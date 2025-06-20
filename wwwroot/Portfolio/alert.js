function sendFormData() {
    const selectedCoin = $("#coin-select").val();

    const alertType = $("#alert-type").val();

    // --- price value---
    const alertPrice = $("#alert-price").val();

    // --- ma values ---
    const maShortPeriod = $("#ma-short").val();
    const maLongPeriod = $("#ma-long").val();
    const maCondition = $("#ma-condition").val();
    const interval = $("#interval").val();

    // --- rsi value ---
    const rsiValue = $("#rsi-value").val();
    const rsiCondition = $("#rsi-condition").val();
    const notificationMethod = $("input[name='notificationMethod']:checked").val();


    $.ajax({
        url: "/Alert",
        type: "POST",
        dataType: "json",
        data: {
            Coin: selectedCoin, 
            AlertType : alertType, 
            AlertPrice :  alertPrice,
            MovingAverage : [maShortPeriod , maLongPeriod], 
            Condition : maCondition || rsiCondition,
            RSIValue : rsiValue,
            NotificationMethod : notificationMethod,
            Interval: interval
         
        },
        success: function (response) {
            if (response.success) {
                if (parseInt(response.count) >= 1) {
                    window.location.replace(response.url);
                }
            } else {
                const errors = response.error;
                for (const key in errors) {
                    if (errors.hasOwnProperty(key)) {
                        const errorMessages = errors[key];
                        if (errorMessages.length > 0 && key === "customerror") {
                            errorMessages.forEach(function (message) {
                                $("#error").text(message).show();
                            });
                        }
                    }
                }
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", status, error);
        }
    });
}
