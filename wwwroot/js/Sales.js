google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawCharts);

function drawCharts() {
    drawDailyChart();
    drawCustomerChart();
    drawProductChart();
}

function drawDailyChart() {
    $.ajax({
        url: "/Sales/GetSalesData",
        type: "GET",
        dataType: "json",
        success: function (response) {
            if (!response || response.length === 0) {
                console.log("No sales data available.");
                return;
            }

            var data = [['Date', 'Sales', 'Sales2']];
            response.forEach(function (item) {
                if (!item.date || item.totalSales === undefined) {
                    console.warn("Skipping invalid data:", item);
                    return;
                }
                data.push([item.date, item.totalSales, item.totalSales]);
            });

            console.log("Chart Data (Daily Sales):", data);

            var chartData = google.visualization.arrayToDataTable(data);
            var options = {
                title: 'Daily Sales Data (Last 6 Days)',
                vAxis: { title: 'Total Sales' },
                hAxis: { title: 'Date' },
                legend: { position: 'none' },
                seriesType: 'bars',
                series: {
                    0: { color: '#D1E9F6' },
                    1: { type: 'line', color: '#6EACDA' }
                }
            };

            var chart = new google.visualization.ColumnChart(document.getElementById('dailyChart'));
            chart.draw(chartData, options);
        },
        error: function (xhr, status, error) {
            console.error("Error loading sales data:", error);
        }
    });
}

function drawCustomerChart() {
    $.ajax({
        url: "/Sales/GetCustomerSalesData",
        type: "GET",
        dataType: "json",
        success: function (response) {
            if (!response || response.length === 0) {
                console.log("No customer sales data available.");
                return;
            }

            var data = [['Customer', 'Sales']];
            response.forEach(function (item) {
                var customerName = "";
                switch (item.customerId) {
                    case 1: customerName = "New World"; break;
                    case 2: customerName = "PAK'n SAVE"; break;
                    case 3: customerName = "Woolworths"; break;
                    default: customerName = "Other";
                }
                data.push([customerName, item.totalSales]);
            });

            var chartData = google.visualization.arrayToDataTable(data);
            var options = {
                title: 'Sales ratio by customer',
                width: 600,
                height: 400,
                pieHole: 0.4,
                slices: {
                    0: { color: '#D84040' },
                    1: { color: '#FFCF50' },
                    2: { color: '#88C273' }
                }
            };

            var chart = new google.visualization.PieChart(document.getElementById('customerChart'));
            chart.draw(chartData, options);
        },
        error: function (xhr, status, error) {
            console.error("Error loading customer sales data:", error);
        }
    });
}

function drawProductChart() {
    $.ajax({
        url: "/Sales/GetProductSalesData",
        type: "GET",
        dataType: "json",
        success: function (response) {

            if (!response || response.length === 0) {
                console.log("No product sales data available.");
                return;
            }

            var data = [['Product', 'Sales Amount']];
            response.forEach(function (item) {
                data.push([item.productName, item.totalSales]); 
            });

            console.log("Chart Data (Product Sales):", data);

            var chartData = google.visualization.arrayToDataTable(data);
            var options = {
                title: 'Top 10 Selling Products',
                width: 800,
                height: 500,
                legend: { position: 'none' },
                hAxis: { title: 'Sales Amount' }, 
                bars: 'horizontal', 
                colors: ['#0D92F4']
            };

            var chart = new google.visualization.BarChart(document.getElementById('productChart'));
            chart.draw(chartData, options);
        },
        error: function (xhr, status, error) {
            console.error("Error loading product sales data:", error);
        }
    });
}


