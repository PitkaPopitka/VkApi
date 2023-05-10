using Newtonsoft.Json;
using WebApplication4.Models;

namespace WebApplication4
{
    public class GroupConverter : JsonConverter<Group>
    {
        public override Group? ReadJson(JsonReader reader, Type objectType, Group? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string value = reader.Value as string;
            return new Group {Name = value};
        }

        public override void WriteJson(JsonWriter writer, Group? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
