using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MAUIMobile.Models;


public partial class Astroid : ObservableObject
{

    [ObservableProperty]
    [property: JsonPropertyName("id")]
    string id;

    [ObservableProperty]
    [property: JsonPropertyName("name")]
    string name;

    [ObservableProperty]
    [property: JsonPropertyName("is_potentially_hazardous_asteroid")]
    bool isPotentiallyHazardous;

    [ObservableProperty]
    string closeApproachDate;  

    [ObservableProperty]
    string missDistance;      


    // This handles nested JSON and fills the above observable properties
    [JsonPropertyName("close_approach_data")]
    public JsonElement CloseApproachData
    {
        set
        {
            
            if (value.ValueKind != JsonValueKind.Array || value.GetArrayLength() == 0)
                return;

            var first = value[0];

            
            CloseApproachDate =
                first.GetProperty("close_approach_date_full").GetString();

            MissDistance =
                first.GetProperty("miss_distance")
                     .GetProperty("kilometers")
                     .GetString();
        }
    }
}
