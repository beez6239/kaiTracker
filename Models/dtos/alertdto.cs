using System.ComponentModel.DataAnnotations; 
namespace KaiCryptoTracker.AlertModel;

public class AlertAction
{
    [Required(ErrorMessage = "Please select a token.")]
    public string Coin { get; set; } = string.Empty;

    [Required(ErrorMessage = "Alert type is not specified.")]
    public string AlertType { get; set; } = string.Empty;

   
    [Range(1, 100, ErrorMessage = "RSI value must be between 1 and 100.")]
    public int? RSIValue { get; set; }

    
    [Range(0.00001, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public decimal? AlertPrice { get; set; }

    
    [MinLength(2, ErrorMessage = "Two moving average values are required.")]
    public int[]? MovingAverage { get; set; }
    
    public int Interval { get; set; }

    public string? Condition { get; set; }

    [Required(ErrorMessage = "Notification method is required.")]
    public string NotificationMethod { get; set; } = string.Empty;

}