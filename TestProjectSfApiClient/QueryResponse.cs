using System.Text.Json.Serialization;

namespace TestProjectSfApiClient;

public class QueryResponse<T>
{
    [JsonPropertyName("totalSize")]
    public int TotalSize { get; set; }

    [JsonPropertyName("done")]
    public bool Done { get; set; }

    [JsonPropertyName("nextRecordsUrl")]
    public string? NextRecordsUrl { get; set; }

    [JsonPropertyName("records")]
    public List<T> Records { get; set; } = new List<T>();
}
