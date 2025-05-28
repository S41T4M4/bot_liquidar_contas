using bot_retorno.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace bot_retorno.Services
{
    // Esse código é responsavél por liquidar a receita na API do VHSYS, onde é passado o id_conta_rec e os dados do titulo a ser liquidado.
    class LiquidarReceita
    {
        public static async Task<bool> LiquidarReceitaAsync(int idContaRec, LiquidarReceitaRequest liquidarReceitaRequest, string accessToken, string secretAcessToken)
        {
            using HttpClient httpClient = new HttpClient();

            string url = $"https://api.vhsys.com/v2/contas-receber/{idContaRec}";
            Console.WriteLine($"[DEBUG] URL da requisição: {url}");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var json = JsonSerializer.Serialize(liquidarReceitaRequest, options);
            Console.WriteLine("[DEBUG] JSON Enviado:");
            Console.WriteLine(json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("access-token", accessToken);
            httpClient.DefaultRequestHeaders.Add("secret-access-token", secretAcessToken);
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("JcDecorBot/1.0");

            try
            {
                var response = await httpClient.PutAsync(url, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"[DEBUG] StatusCode: {response.StatusCode}");
                Console.WriteLine("[DEBUG] Corpo da Resposta:");
                Console.WriteLine(responseBody);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ERRO] Exceção ao fazer requisição:");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }

}
