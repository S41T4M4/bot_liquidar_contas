using bot_retorno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace bot_retorno.Services
{
    // Esse código é responsavél por consultar a API do VHSYS, onde é passado o id_conta_rec e retornado os dados da receita.
    class ContasAReceber
    {
        public static async Task<ReceitaDados?> ContasAReceberAsync(int idContaRec)
        {
            string url = $"https://api.vhsys.com/v2/contas-receber/{idContaRec}";
            var accessToken = "TVGHZQGKOFERELGNbSJAYBWOeVECOI";
            var secretAccessToken = "wUX4VQuGHbvbagHyg9n9CPaNZFpOE2";

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("access-token", accessToken);
            httpClient.DefaultRequestHeaders.Add("secret-access-token", secretAccessToken);

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse>(json);

                return result?.Data?.Count > 0 ? result.Data[0] : null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao consultar a API: {e.Message}");
                return null;
            }
        }
    }
}
