using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace bot_retorno.Converters
{
    public class DecimalJsonConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var str = reader.GetString()?.Trim();
                    if (decimal.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
                        return result;
                }
                else if (reader.TokenType == JsonTokenType.Number)
                {
                    return reader.GetDecimal();
                }
            }
            catch { }

            return 0m; 
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
    public class NullableDateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string value = reader.GetString();

                if (string.IsNullOrWhiteSpace(value))
                    return null;

                if (DateTime.TryParse(value, out DateTime date))
                    return date;

                return null;
            }

            if (reader.TokenType == JsonTokenType.Null)
                return null;

            throw new JsonException($"Unexpected token parsing date. Token: {reader.TokenType}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString("yyyy-MM-ddTHH:mm:ss"));
            else
                writer.WriteNullValue();
        }
    }

}