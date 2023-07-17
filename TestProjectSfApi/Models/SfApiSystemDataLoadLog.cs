using System.Text.Json.Serialization;
using TestProjectSfApiLib.Enums;

namespace TestProjectSfApiLib.Models;

public class SfApiSystemDataLoadLog : SfApiEntity
{
    [JsonPropertyName("FIN_Process__c")]
    public DataGroup? DataGroup { get; set; }

    [JsonPropertyName("FIN_ProcessDate__c")]
    public DateOnly? ProcessDate { get; set; }

    [JsonPropertyName("AWS_ActualRawTotalRecordCount__c")]
    public double? ActualRawTotalRecordCount { get; set; }

    [JsonPropertyName("AWS_ActualRawTotalAmount__c")]
    public decimal? ActualRawTotalAmount { get; set; }
}