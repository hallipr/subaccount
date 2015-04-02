namespace SubAccount.Loader.Ofx.Parsing
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;

    public class OfxDateTimeConverter : JsonConverter
    {
        private const string DateTimeFormat = "yyyyMMddHHmmss.fff";
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
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

            if (reader.TokenType == JsonToken.Date)
            {
                return reader.Value;
            }

            if (reader.TokenType != JsonToken.String)
                throw new JsonSerializationException(string.Format("Unexpected token parsing date. Expected String, got {0}.", reader.TokenType));

            var dateText = reader.Value.ToString();

            if (string.IsNullOrEmpty(dateText) && nullable)
                return null;

            var format = DateTimeFormat;
            if (dateText.Contains('['))
            {
                dateText = dateText.Split(':')[0] + "]";
                format += "[z]";
            }

            return DateTime.ParseExact(dateText, format, CultureInfo.CurrentCulture);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
        }
    }
}