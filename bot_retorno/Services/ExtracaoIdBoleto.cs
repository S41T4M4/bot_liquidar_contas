using bot_retorno.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot_retorno.Services
{

    // Nesse código é feito a extração do arquivo HTML recebido pelo financeiro, onde é localizado na tabela o "Seu Número" e o "Valor" do título, que são utilizados para liquidar a receita.
    class ExtracaoIdBoleto
    {
       public static List<TituloComValor> ExtrairNumerosComDesconto(string filePath)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.Load(filePath);

            var table = htmlDoc.DocumentNode.SelectSingleNode("//table[@border='1']");
            if (table == null)
            {
                Console.WriteLine("Nenhuma tabela encontrada.");
                return new List<TituloComValor>();
            }

            List<TituloComValor> titulos = new List<TituloComValor>();

            foreach (var row in table.SelectNodes(".//tr[position() > 1]"))
            {
                var columns = row.SelectNodes(".//td");

                if (columns != null && columns.Count >= 9)
                {
                    string valor = columns[3].InnerText.Trim();
                    string seuNumero = columns[6].InnerText.Trim();
                    string tipoCobranca = columns[8].InnerText.Trim();

                    if (tipoCobranca.Contains("Desconto", StringComparison.OrdinalIgnoreCase))
                    {
                        titulos.Add(new TituloComValor
                        {
                            SeuNumero = seuNumero,
                            Valor = valor
                        });
                    }
                }
            }

            return titulos;
        }
    }
}
