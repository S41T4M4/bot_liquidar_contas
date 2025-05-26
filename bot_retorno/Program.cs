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




class Program
{
    // Esse programa é responsavél por ler um arquivo HTML, extrair os números de boletos e consultar a API do VHSYS para liquidar as contas a receber.

    static async Task Main(string[]args)
    {
        string htmlFilePath = "C:\\Users\\ibrai\\OneDrive\\Imagens\\teste.xls";
        var config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory()) 
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) 
          .Build();

        string connectionString = config["ConnectionStrings:Default"]!;
        string accessToken = config["VHSYS:AccessToken"]!;
        string secretAccessToken = config["VHSYS:SecretToken"]!;

        var numerosFiltrados = ExtracaoIdBoleto.ExtrairNumerosComDesconto(htmlFilePath);
        Console.WriteLine($"Total de títulos extraídos: {numerosFiltrados.Count}");
        foreach (var titulo in numerosFiltrados) 
        {
            Console.WriteLine($"Número extraído: {titulo.SeuNumero}, Valor: {titulo.Valor}");
            var numero = titulo.SeuNumero;
            var valorArquivo = titulo.Valor;

            var contaRecInfo = await ConsultarIdContaRec.ConsultarIdContaRecAsync(connectionString, numero);
            if (contaRecInfo != null && int.TryParse(contaRecInfo.IdContaRec, out int idConta))
            {
                Console.WriteLine($"Número: {numero} -> id_conta_rec: {contaRecInfo.IdContaRec} -> id_banco: {contaRecInfo.IdBanco} -> Valor da Conta : {valorArquivo}");


                var receitaDados = await ContasAReceber.ContasAReceberAsync(idConta);
                if(receitaDados != null && receitaDados.Situacao != "Conta Liquidada" && receitaDados.LiquidadoRec != "Sim")
                {
                    Console.WriteLine($"id_conta_rec: {receitaDados.IdContaRec}, Nome: {receitaDados.NomeCliente}, Valor: {receitaDados.ValorRec}, Id do banco: {contaRecInfo.IdBanco}");
                
                    var liquidarrequest = new LiquidarReceitaRequest
                    {
                        LiquidadoRec = "Sim",
                        ValorPago = valorArquivo.Replace(",", "."),
                        DataPagamento = DateTime.Now.ToString("yyyy-MM-dd"),
                        ObservacaoPagamento = "Conta liquida com desconto de recebíveis",
                        IdBanco = contaRecInfo.IdBanco

                    };
                 
                    var sucess = await LiquidarReceita.LiquidarReceitaAsync(idConta, liquidarrequest, accessToken, secretAccessToken);
                    if (sucess)
                    {
                        Console.WriteLine($"Conta {contaRecInfo.IdContaRec} liquidada com sucesso. {contaRecInfo.IdBanco}");
                    }
                    else
                    {
                        Console.WriteLine($"Falha ao liquidar a conta {contaRecInfo.IdContaRec}.");
                    }
                }
                else
                {
                    Console.WriteLine($"A conta: {contaRecInfo.IdContaRec} já foi liquidada !");
                }
            }

        }
    }
}
