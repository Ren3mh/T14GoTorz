using System.Text.Json;
using System.Text.Json.Serialization;
using Gotorz14.Model;
using static Gotorz14.Model.Fligth;

namespace Gotorz14.Services

{
    public class jsonToObject
    {
        public static FareData JsonDeserialize1(string jsonText)
        {
            FareData fareData = JsonSerializer.Deserialize<FareData>(jsonText);
            return fareData;
        }

    }
}
