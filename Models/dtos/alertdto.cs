using System.ComponentModel.DataAnnotations; 
namespace KaiCryptoTracker.AlertModel;

public class AlertAction
{
    [Required(ErrorMessage = "Please select a Token ")]
    public string Coin { get; set; } = string.Empty;

    [Required(ErrorMessage = "Alert type not specified ")]
    public string AlertType { get; set; } = string.Empty;
    public int? RSIValue { get; set; }
    public int? Price { get; set; }
    public int[]? MovingAverage { get; set; } = Array.Empty<int>();
    public Dictionary<string, int> condition { get; set; } = new();


}

public static class AlertTypes
{
    public static string[] AlertTypeValues()
    {
        return ["Pricealert", "Movingaveragealert", "Rsialert"]; 
    }
}