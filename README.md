# 🛍️ Smart POS .NET
## A lightweight Sales & Inventory management system <br> built with ASP.NET Core for small businesses

| **Product Management** |
| :---------------------: |
| <img src ="https://github.com/user-attachments/assets/0643a475-11a0-4e19-abec-bf5958ba8443" width="800"/> |
| **Inventory Management** |
| <img src ="https://github.com/user-attachments/assets/ff550fc0-a6dc-4e18-b2f5-bbe81281c34c" width="800"/> |
| **Order Management** |
| <img src ="https://github.com/user-attachments/assets/39e4bea2-6944-44f0-8e76-1e17f949de3a" width="800"/> |
| **Sales Management** |
| <img src ="https://github.com/user-attachments/assets/aaa24ea2-a88f-4065-a284-b1c138b51554" width="800"/> |


# 🧭 [Index](#index) <a name = "index"></a>

- [Introduction](#introduction)
- [Tech Skills](#tech)
- [ERD](#erd)
- [Features](#features)
- [Pain Point](#point)
- [Retrospective](#retrospective)  

<br>

## 🚩Introduction <a name = "introduction"></a>
- Smart POS Web is a lightweight and efficient sales and inventory management system designed for small businesses.
- Product Management: Create, update, and delete master products.
- Inventory Management: Automatically deduct stock when an order is placed.
- Order Management: Process and track customer orders efficiently.
- Sales Management: Analyze sales trends using charts.
- User-friendly UI: Provides real-time data updates for seamless experience.

<br>

## 🤖 Languages & Technologies <a name = "tech"></a>
### `Back-end`
* C# .NET 8 (ASP.NET Core)
* Entity Framework Core
* MySQL
* RESTful API
* Visual Studio, DBeaver

### `Front-end`
* HTML
* CSS
* JavaScript
* JQuery
* BootStrap

### `External API`
* Google chart API

<br>

<br>

## 📝 ERD <a name = "erd"></a>
<img src="https://github.com/user-attachments/assets/5f1e7012-e136-46a7-b0cd-aa8a529b4652" width="800"/>

## 🧠 Core Features & Implementation <a name = "features"></a>
<img src="https://github.com/user-attachments/assets/5087c0a6-6f39-4593-b871-4d2c6041a97d">



<br>
<!-- <img src ="https://github.com/user-attachments/assets/be377622-c7ed-4127-821f-f555d1d8a16d" width="850"/> -->
<img src ="https://github.com/user-attachments/assets/5f01b878-446b-4a84-a1e3-075d045f5fbb" width="850"/>
<img src ="https://github.com/user-attachments/assets/eb9ac8c9-5019-4406-9af7-82fc85f8d1ed" width="850"/>

## 🎯 Pain Point <a name = "point"></a>
## ✅ How to efficiently manage real-time inventory and order processing?
- **Seamless real-time inventory tracking**: The system ensures that inventory is automatically deducted as orders are placed, preventing stock inconsistencies.
- **Asynchronous order processing**: Ensures smooth order fulfillment and inventory updates without system lag, improving operational efficiency.
- **Smart replenishment calculations**: The system analyzes stock levels and automatically suggests restocking quantities based on order trends and sales performance.
- **Separation of Inventory and Product Data**: Inventory is managed independently from the product catalog, enabling a stock-focused system that prioritizes supply chain accuracy.
- **One-to-Many Order Detail Structure**: Orders are split into detailed line items, allowing multiple products to be processed in a single transaction while supporting FIFO (First-In, First-Out) stock handling.

<br>


## 📒 Retrospective <a name = "retrospective"></a>
#### Lessons Learned During the Project
🚀 **Transitioning from PHP to ASP.NET**

Coming from a PHP background, building an order management web application using ASP.NET was a new and exciting challenge. Thanks to my Java experience, I was able to quickly structure the architecture using the MVC pattern.

📦 **Future Enhancements for Inventory Management**

One key feature that enhances revenue and profitability tracking is the cost price column in the product master table. By storing cost prices, the system can provide detailed profit margin analysis, allowing businesses to make informed financial decisions.
<br><br>
To further improve the system, I see potential in incorporating expiration dates and storage locations into the inventory model. This would make the system even more practical for real-world small businesses, allowing them to track perishable goods and optimize warehouse organization. For now, I focused on creating a lightweight system centered around total sales revenue.

📊 **Crafting a Data-Driven Sales Dashboard**

I designed three key charts that provide small business owners with essential sales insights. While many systems on the market focus on basic calculations, what truly differentiates a solution is whether it provides a dashboard that users find intuitive and actionable. Moving forward, I plan to dive deeper into data visualization and user experience design to refine this aspect.

