using Library.Entity;
using Newtonsoft.Json;
using System;

namespace Library.DTOModels.DTOMappers.Converters
{
    public class EditorialJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var editorial = value as Editorial;
            writer.WriteValue(editorial != null ? editorial.Id : "");
        }

        public override bool CanConvert(Type objectType) => typeof(Editorial).IsAssignableFrom(objectType);

        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Should never get here.");
        }
    }
}