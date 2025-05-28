using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bot_retorno.Models;
using Microsoft.Data.SqlClient;


namespace bot_retorno.Services
{
    // Esse código é responsável por receber o número do id_boleto, onde é feito a consulta no banco de dados, retornando o id_conta_rec.
    class ConsultarIdContaRec
    {
        public static async Task<ContaRecInfo?> ConsultarIdContaRecAsync(string connectionString, string numero)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                

                string query = "SELECT id_conta_rec, id_banco FROM CR1 WHERE id_boleto = @id_boleto";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id_boleto", numero);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string idContaRec = reader["id_conta_rec"].ToString()!;
                            string idBanco = reader["id_banco"].ToString()!;
                            return new ContaRecInfo(idContaRec, idBanco);
                        }
                    }
                }
            }

            return null;
        }
    }

}
