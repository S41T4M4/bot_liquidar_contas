using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using bot_retorno.Models;
using Microsoft.Identity.Client;
using System.Text;
using bot_retorno.Services;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using System.Globalization;




class Program
{
    // Esse programa é responsavél por ler um arquivo HTML, extrair os números de boletos e consultar a API do VHSYS para liquidar as contas a receber.

    static async Task Main(string[] args)
    {
        List<string> boletosNaoEncontrados = new List<string>();
        List<string> boletosJaLiquidados = new List<string>();
        List<string> boletosLiquidadosSucesso = new List<string>();
        // Caminho do arquivo HTML que contém os boletos.
        string htmlFilePath =@"C:\Users\ibrai\OneDrive\Imagens\TitulosBaixadosLiquidados.xls";
        if (!Path.Exists(htmlFilePath))
        {
            Console.WriteLine($"Arquivo HTML não encontrado: {htmlFilePath}");
        }
        
        var config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();

        string connectionString = config["ConnectionStrings:Default"]!;
        string accessToken = config["VHSYS:AccessToken"]!;
        string secretAccessToken = config["VHSYS:SecretToken"]!;

        // Extrai os números dos boletos com desconto do arquivo HTML.

        var numerosFiltrados = ExtracaoIdBoleto.ExtrairNumerosComDesconto(htmlFilePath);
        Console.WriteLine($"Total de títulos extraídos: {numerosFiltrados.Count}");
        foreach (var titulo in numerosFiltrados)
        {
                
            Console.WriteLine($"Número extraído: {titulo.SeuNumero}, Valor: {titulo.Valor} ");
            var numero = titulo.SeuNumero;
            var valorArquivo = titulo.Valor;

            var contaRecInfo = await ConsultarIdContaRec.ConsultarIdContaRecAsync(connectionString, numero);
            if (contaRecInfo == null)
            {
            
                boletosNaoEncontrados.Add(numero);
                continue; 
            }

            if (contaRecInfo != null && int.TryParse(contaRecInfo.IdContaRec, out int idConta))
            {
                Console.WriteLine($"Número: {numero} -> id_conta_rec: {contaRecInfo.IdContaRec} -> id_banco: {contaRecInfo.IdBanco} -> Valor da Conta : {valorArquivo}");

                // Aqui é consultado o id_conta_rec na api do vhsys, e retornado dados da receita
                var receitaDados = await ContasAReceber.ContasAReceberAsync(idConta, accessToken, secretAccessToken);
                if (receitaDados != null && receitaDados.Situacao != "Conta Liquidada" && receitaDados.LiquidadoRec != "Sim")
                {
                    Console.WriteLine($"id_conta_rec: {receitaDados.IdContaRec}, Nome: {receitaDados.NomeCliente}, Valor: {receitaDados.ValorRec}, Id do banco: {contaRecInfo.IdBanco}");
                    decimal valorDecimal = decimal.Parse(valorArquivo, new CultureInfo("pt-BR"));
                    var liquidarrequest = new LiquidarReceitaRequest
                    {
                        LiquidadoRec = "Sim",
                        ValorPago = valorDecimal.ToString("0.00", CultureInfo.InvariantCulture),
                        DataPagamento = DateTime.Now.ToString("yyyy-MM-dd"),
                        ObservacaoPagamento = "Conta liquida com desconto de recebíveis",
                        IdBanco = contaRecInfo.IdBanco

                    };
                    // Aqui é liquidado a receita
                    var sucess = await LiquidarReceita.LiquidarReceitaAsync(idConta, liquidarrequest, accessToken, secretAccessToken);
                    if (sucess)
                    {
                        boletosLiquidadosSucesso.Add(numero);
                        Console.WriteLine($"Conta {contaRecInfo.IdContaRec} liquidada com sucesso. {contaRecInfo.IdBanco}");
                        Console.WriteLine("------------------------------------------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine($"Falha ao liquidar a conta {contaRecInfo.IdContaRec}.");
                        Console.WriteLine("------------------------------------------------------------------------------");
                    }
                }
                else
                {
                    boletosJaLiquidados.Add(numero);
                    Console.WriteLine($"A conta: {contaRecInfo.IdContaRec} já foi liquidada !");
                    Console.WriteLine("------------------------------------------------------------------------------");
                }
                
            }
           


        }
        Console.WriteLine($"Boletos não encontrados: {boletosNaoEncontrados.Count}");
        foreach (var boletoNaoEncontrado in boletosNaoEncontrados)
        {
            Console.WriteLine($"[{boletoNaoEncontrado}]");
        }
        Console.WriteLine("------------------------------------------------------------------------------");
        Console.WriteLine($"Boletos já liquidados: {boletosJaLiquidados.Count}");
        foreach (var boletoLiquidados in boletosJaLiquidados)
        {
            Console.WriteLine($"[{boletoLiquidados}]");
        }
        Console.WriteLine("------------------------------------------------------------------------------");
        Console.WriteLine($"Boletos liquidados com sucesso: {boletosLiquidadosSucesso.Count}");
        foreach (var boletoLiquidados in boletosLiquidadosSucesso)
        {
            Console.WriteLine($"[{boletoLiquidados}]");
        }

    }
}
    