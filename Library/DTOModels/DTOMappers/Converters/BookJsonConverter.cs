using Library.Entity;
using Newtonsoft.Json;
using System;

namespace Library.DTOModels.DTOMappers.Converters
{
    public class BookJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var book = value as Book;
            writer.WriteValue(book != null ? book.Id : "");
        }

        public override bool CanConvert(Type objectType) => typeof(Book).IsAssignableFrom(objectType);

        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Should never get here.");
        }
    }
}