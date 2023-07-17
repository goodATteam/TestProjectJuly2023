using System.Text.Json.Serialization;

namespace TestProjectSfApiLib.Models;

public abstract class SfApiEntity
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }
}