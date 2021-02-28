using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Genealogy.Data
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PurchaseStatus
    {
        Pending = 0,
        WaitingForCapture = 1,
        Succeeded = 2,
        Canceled = 3,
        Processed = 4
    }

}