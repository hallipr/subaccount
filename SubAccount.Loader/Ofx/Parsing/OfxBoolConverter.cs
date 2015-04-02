namespace SubAccount.Loader.Ofx.Parsing
{
    using System;
    using Newtonsoft.Json;

    public class OfxBoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string text;

            if (value is bool)
            {
                var boolValue = (bool)value;
                text = boolValue ? "Y" : "N";
            }
            else
            {
                throw new JsonSerializationException(string.Format("Unexpected value when converting bool. Expected bool, got {0}.", value.GetType().Name));
            }

            writer.WriteValue(text);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var nullable = Nullable.GetUnderlyingType(objectType) != null;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!nullable)
                    throw new JsonSerializationException(string.Format("Cannot convert null value to {0}.", objectType));

                return null;
            }

            if (reader.TokenType == JsonToken.Boolean)
            {
                return reader.Value;
            }

            if (reader.TokenType != JsonToken.String)
                throw new JsonSerializationException(string.Format("Unexpected token parsing bool. Expected String, got {0}.", reader.TokenType));

            var valueText = reader.Value.ToString();

            if (string.IsNullOrEmpty(valueText) && nullable)
                return null;

            if (valueText == "Y")
                return true;

            if (valueText == "N")
                return false;

            throw new JsonSerializationException(string.Format("Unexpected value parsing bool. Expected Y or N, got {0}.", valueText));
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool) || objectType == typeof(bool?);
        }
    }
}