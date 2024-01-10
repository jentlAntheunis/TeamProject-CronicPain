using Newtonsoft.Json;

namespace Pebbles.Models;

public class Test
{
    public Test(int id, string data)
    {
        Id = id;
        Data = data;
    }

    public Test() { }

    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("data")]
    public string Data { get; set; }
}
