using bot_retorno.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ReceitaDados
{
    [JsonPropertyName("id_conta_rec")]
    public int IdContaRec { get; set; }

    [JsonPropertyName("id_empresa")]
    public int IdEmpresa { get; set; }

    [JsonPropertyName("id_registro")]
    public int IdRegistro { get; set; }

    [JsonPropertyName("identificacao")]
    public string Identificacao { get; set; }

    [JsonPropertyName("nome_conta")]
    public string NomeConta { get; set; }

    [JsonPropertyName("id_categoria")]
    public int IdCategoria { get; set; }

    [JsonPropertyName("categoria_rec")]
    public string CategoriaRec { get; set; }

    [JsonPropertyName("id_banco")]
    public int IdBanco { get; set; }

    [JsonPropertyName("id_cliente")]
    public int IdCliente { get; set; }

    [JsonPropertyName("id_boleto")]
    public int IdBoleto { get; set; }

    [JsonPropertyName("nome_cliente")]
    public string NomeCliente { get; set; }

    [JsonPropertyName("vencimento_rec")]
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public DateTime? VencimentoRec { get; set; }

    [JsonPropertyName("valor_rec")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal ValorRec { get; set; }

    [JsonPropertyName("valor_pago")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal ValorPago { get; set; }

    [JsonPropertyName("data_emissao")]
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public DateTime? DataEmissao { get; set; }

    [JsonPropertyName("vencimento_original")]
    public DateTime? VencimentoOriginal { get; set; }

    [JsonPropertyName("n_documento_rec")]
    public string NDocumentoRec { get; set; }

    [JsonPropertyName("observacoes_rec")]
    public string ObservacoesRec { get; set; }

    [JsonPropertyName("id_centro_custos")]
    public int IdCentroCustos { get; set; }

    [JsonPropertyName("centro_custos_rec")]
    public string CentroCustosRec { get; set; }

    [JsonPropertyName("praca_pagamento")]
    public string PracaPagamento { get; set; }

    [JsonPropertyName("liquidado_rec")]
    public string LiquidadoRec { get; set; }

    [JsonPropertyName("data_pagamento")]
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public DateTime? DataPagamento { get; set; }

    [JsonPropertyName("obs_pagamento")]
    public string ObsPagamento { get; set; }

    [JsonPropertyName("forma_pagamento")]
    public string FormaPagamento { get; set; }

    [JsonPropertyName("valor_juros")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal ValorJuros { get; set; }

    [JsonPropertyName("valor_desconto")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal? ValorDesconto { get; set; }

    [JsonPropertyName("valor_acrescimo")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal? ValorAcrescimo { get; set; }

    [JsonPropertyName("valor_taxa")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal ValorTaxa { get; set; }

    [JsonPropertyName("retorno_pagamento")]
    public int RetornoPagamento { get; set; }

    [JsonPropertyName("tipo_conta")]
    public string TipoConta { get; set; }

    [JsonPropertyName("data_cad_rec")]
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public DateTime? DataCadRec { get; set; }

    [JsonPropertyName("data_mod_rec")]
    [JsonConverter(typeof(NullableDateTimeConverter))]
    public DateTime? DataModRec { get; set; }

    [JsonPropertyName("boleto_enviado")]
    public int BoletoEnviado { get; set; }

    [JsonPropertyName("boleto_original")]
    public int BoletoOriginal { get; set; }

    [JsonPropertyName("duplicata_enviado")]
    public int DuplicataEnviado { get; set; }

    [JsonPropertyName("remetido")]
    public int Remetido { get; set; }

    [JsonPropertyName("registrado")]
    public int Registrado { get; set; }

    [JsonPropertyName("protestar")]
    public int Protestar { get; set; }

    [JsonPropertyName("dias_protestar")]
    public int DiasProtestar { get; set; }

    [JsonPropertyName("NossoNumero")]
    public string NossoNumero { get; set; }

    [JsonPropertyName("agrupado")]
    public int Agrupado { get; set; }

    [JsonPropertyName("agrupado_data")]
    public int? AgrupadoData { get; set; }

    [JsonPropertyName("agrupado_user")]
    public int? AgrupadoUser { get; set; }

    [JsonPropertyName("agrupamento")]
    public int Agrupamento { get; set; }

    [JsonPropertyName("fluxo")]
    public int Fluxo { get; set; }

    [JsonPropertyName("lixeira")]
    public string Lixeira { get; set; }

    [JsonPropertyName("id_pagamento_ob")]
    public int? IdPagamentoOb { get; set; }

    [JsonPropertyName("situacao")]
    public string Situacao { get; set; }

  

    [JsonPropertyName("valor_baixa")]
    [JsonConverter(typeof(DecimalJsonConverter))]
    public decimal ValorBaixa { get; set; }

    [JsonPropertyName("link_boleto")]
    public string LinkBoleto { get; set; }
}
