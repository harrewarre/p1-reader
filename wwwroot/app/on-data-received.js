export default function onDataReceived(connection) {
    connection.on("ReportConsumption", onConsumptionReported);
}

function onConsumptionReported(consumptionData) {
    document.getElementById("totalConsumption").innerText = (consumptionData.totalKw * 1000) + "W";
    document.getElementById("l1Consumption").innerText = (consumptionData.line1Kw * 1000) + "W";
    document.getElementById("l2Consumption").innerText = (consumptionData.line2Kw * 1000) + "W";
    document.getElementById("l3Consumption").innerText = (consumptionData.line3Kw * 1000) + "W";
}