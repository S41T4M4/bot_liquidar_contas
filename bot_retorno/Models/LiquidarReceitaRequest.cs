using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace bot_retorno.Models
{
    class LiquidarReceitaRequest
    {
        [JsonPropertyName("liquidado_rec")]
        public string LiquidadoRec { get; set; } = "Sim";

        [JsonPropertyName("valor_pago")]
        public string ValorPago { get; set; }

        [JsonPropertyName("data_pagamento")]
        public string DataPagamento { get; set; }

        [JsonPropertyName("obs_pagamento")]
        public string? ObservacaoPagamento { get; set; }

        [JsonPropertyName("id_banco")]
        public string IdBanco { get; set; }

    }
}
