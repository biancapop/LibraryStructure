using Library.Entity;
using Newtonsoft.Json;
using System;

namespace Library.DTOModels.DTOMappers.Converters
{
    public class CategoryJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var category = value as Category;
            writer.WriteValue(category != null ? category.Id : "");
        }

        public override bool CanConvert(Type objectType) => typeof(Category).IsAssignableFrom(objectType);

        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Should never get here.");
        }
    }
}