let selectedProducts = [];
let currentPage = 1; // 현재 페이지
const pageSize = 5; // 한 페이지당 5개씩 표시

function openProductPopup(page = 1) {
    fetch(`/Product/GetProducts?page=${page}`)
        .then(response => response.json())
        .then(data => {
            const productList = document.getElementById("productList");
            productList.innerHTML = "";

            data.products.forEach(product => {
                productList.innerHTML += `
                    <tr>
                        <td>${product.productId}</td>
                        <td>${product.productName}</td>
                        <td>${product.price.toFixed(2)}</td>
                        <td><button class="btn btn-primary" onclick="addProduct(${product.productId}, '${product.productName}', ${product.price})">➕ Add</button></td>
                    </tr>
                `;
            });

            currentPage = data.currentPage;
            renderPagination(data.totalPages);

            document.getElementById("productPopup").style.display = "block";
        })
        .catch(error => {
            toastr.error("Failed to load products.");
            console.error(error);
        });
}

function renderPagination(totalPages) {
    const pagination = document.getElementById("pagination");
    pagination.innerHTML = "";

    const maxPagesToShow = 3;

    let startPage = Math.max(1, currentPage - 1);
    let endPage = Math.min(totalPages, currentPage + 1);

    // ◀ Prev 버튼
    if (currentPage > 1) {
        pagination.innerHTML += `<button class="btn btn-secondary me-1" onclick="openProductPopup(${currentPage - 1})">⬅ Prev</button>`;
    }

    // 첫 페이지로 이동 버튼 (필요한 경우)
    if (startPage > 1) {
        pagination.innerHTML += `<button class="btn btn-light me-1" onclick="openProductPopup(1)">1</button>`;
        if (startPage > 2) {
            pagination.innerHTML += `<span class="me-1">...</span>`;
        }
    }

    // 현재 페이지 기준으로 앞뒤 2개씩 표시
    for (let i = startPage; i <= endPage; i++) {
        pagination.innerHTML += `<button class="btn ${i === currentPage ? 'btn-primary' : 'btn-light'} me-1" onclick="openProductPopup(${i})">${i}</button>`;
    }

    // 마지막 페이지로 이동 버튼 (필요한 경우)
    if (endPage < totalPages) {
        if (endPage < totalPages - 1) {
            pagination.innerHTML += `<span class="me-1">...</span>`;
        }
        pagination.innerHTML += `<button class="btn btn-light me-1" onclick="openProductPopup(${totalPages})">${totalPages}</button>`;
    }

    if (currentPage < totalPages) {
        pagination.innerHTML += `<button class="btn btn-secondary ms-1" onclick="openProductPopup(${currentPage + 1})">Next ➡</button>`;
    }
}


function closeProductPopup() {
    document.getElementById("productPopup").style.display = "none";
}

function addProduct(id, name, price) {
    price = parseFloat(price);

    // 이미 추가된 제품인지 확인
    const existingProduct = selectedProducts.find(p => p.productId === id);
    if (existingProduct) {
        toastr.warning("This product is already added!");
        return;
    }

    // 새로운 제품 추가 (기본 수량: 1)
    selectedProducts.push({
        productId: id,
        productName: name,
        price: price,
        quantity: 1
    });

    // 주문 목록 업데이트
    updateOrderTable();
    closeProductPopup();
}

function updateOrderTable() {
    const orderItems = $("#orderItems");
    orderItems.html("");

    selectedProducts.forEach((product, index) => {
        const total = (product.price * product.quantity).toFixed(2);

        orderItems.append(`
            <tr>
                <td>${product.productId}</td>
                <td>${product.productName}</td>
                <td>${product.price.toFixed(2)}</td> 
                <td style="width: 120px;">
                    <input type="number" min="1" value="${product.quantity}" 
                        class="form-control quantity-input"
                        onchange="updateQuantity(${index}, this.value)">
                </td>
                <td>${total}</td> 
                <td><button class="btn btn-danger" onclick="removeProduct(${index})">❌ Remove</button></td>
            </tr>
        `);
    });

    updateTotalPrice();
}



function updateQuantity(index, value) {
    selectedProducts[index].quantity = parseInt(value) || 1;
    updateOrderTable();
}

function updateTotalPrice() {
    const totalPrice = selectedProducts.reduce((sum, p) => sum + (p.price * p.quantity), 0);
    document.getElementById("totalPrice").innerText = totalPrice.toFixed(2);
}

function removeProduct(index) {
    selectedProducts.splice(index, 1);
    updateOrderTable();
}

function placeOrder() {
    const customerId = $("#customer").val();
    if (!customerId) {
        toastr.error("Please select a customer.");
        return;
    }

    if (selectedProducts.length === 0) {
        toastr.error("No products added to the order.");
        return;
    }

    const orderData = {
        customerId: customerId,
        orderItems: selectedProducts.map(p => ({
            productId: p.productId,
            quantity: p.quantity
        }))
    };

    console.log("----- orderData", orderData);

    $.ajax({
        url: "/Order/PlaceOrder",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(orderData),
        success: function (response) {
            console.log("Order placed:", response);
            toastr.success("Order placed successfully!");

            selectedProducts = [];
            updateOrderTable();
        },
        error: function (xhr, status, error) {
            console.error("Error placing order:", error);
            toastr.error(xhr.responseJSON?.message || "Failed to place order.");
        }
    });
}

