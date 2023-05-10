using Newtonsoft.Json;
using WebApplication4.Models;

namespace WebApplication4
{
    public class StateConverter : JsonConverter<State>
    {
        public override State ReadJson(JsonReader reader, Type objectType, State? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return new State { Name = value };
        }

        public override void WriteJson(JsonWriter writer, State? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
