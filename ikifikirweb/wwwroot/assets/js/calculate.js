var mainCalculate = function (total) {

    let buttons = document.querySelectorAll('button');
    let countPrice = document.getElementById("count-price");
    let countPricePre = document.getElementById("count-price-pretotal");
    let btnNext = document.getElementById("btnNext");
    let totalPrice = 0;
    let sumTotal = 0;
    let Id = 0;
    let IdRemove = 0;

    if (total > 0) {
        btnNext.style.cursor = "pointer";
        btnNext.style.backgroundColor = "#0040d8";
        countPricePre.innerHTML = total;
    }

    else {
        btnNext.style.cursor = "not-allowed";
        btnNext.style.backgroundColor = "#84a8ff";
        btnNext.style.disabled = "disabled";
    }

    buttons.forEach(button => {
        button.addEventListener('click', function () {

            btnNext.style.cursor = "pointer";
            btnNext.style.backgroundColor = "#0040d8";
            btnNext.style.disabled = "false";

            if (this.innerHTML == "+") {

                var id = $(this).val();
                var priceThis = $("#price_" + id).val();
                var titleThis = $("#title_" + id).val();
                Id = $(this).val();
                selected = true;
                this.classList.add('active');
                this.innerHTML = "-";
                totalPrice = Number.parseInt(priceThis);

                if (total > 0) {
                    total = increment(totalPrice);
                    //Bu satırda toplamı yazıyor 
                    countPricePre.innerHTML = total;
                }

                else {
                    countPrice.innerHTML = increment(totalPrice);
                }

                insertToCalculate();
                insertDynamicListData(titleThis, priceThis);

            }
            else if (this.innerHTML == "-") {

                var id = $(this).val();
                IdRemove = $(this).val();
                var priceThis = $("#price_" + id).val();
                selected = false;
                this.classList.remove('active');
                this.innerHTML = "+";
                totalPrice = Number.parseInt($("#price_" + id).val());

                if (total > 0) {
                    total = deIncrement(totalPrice);
                     //Bu satırda toplamı yazıyor
                    countPricePre.innerHTML = total;
                }
                else {
                    countPrice.innerHTML = deIncrement(totalPrice);
                }

                removeToCalculate();
                removeDynamicListData(priceThis);

            }
        });
    });

    var insertToCalculate = function () {

        try {

            $.ajax({

                type: "POST",
                url: "/anasayfa/sepeteekle/",
                data: { Id: Id },
                success: function (response) {
                },
                error: function (response) {
                }

            });
        }

        catch (err) {
            alert(err);
        }

        finally {

        }

    }

    var removeToCalculate = function () {

        try {

            $.ajax({

                type: "POST",
                url: "/anasayfa/sepetcikar/",
                data: { IdRemove: IdRemove },
                success: function (response) {
                },
                error: function (response) {
                }

            });
        }

        catch (err) {
            alert(err);
        }

        finally {

        }

    }

    function insertDynamicListData(titleThis, priceThis) {

        var listEmpty = document.getElementById("empty_list");

        if (listEmpty != null) {
            listEmpty.remove();
        }

        var li = document.createElement('li');
        li.innerHTML = dynamicelementdata(titleThis, priceThis);
        document.getElementById("uniqeListPrice").appendChild(li);

    }

    function removeDynamicListData(priceThis) {
        var priceConvert = Number.parseInt(priceThis);
        var li = document.getElementById('list_' + priceConvert);
        li.remove();
    }

    function dynamicelementdata(titleThis, priceThis) {
        var priceConvert = Number.parseInt(priceThis);
        return "<li style='font-size: 15px; line-height: 22px;' id='list_" + priceConvert + "'>" +
            "<div>" + titleThis + " <br /> <strong>" + priceThis + " ₺</strong>" +
            "<hr style='margin-top: 10px; margin-bottom: 10px;' /> </div></li>"
    }

    function increment(totalPriceValue) {
        sumTotal += totalPriceValue;
        return Number.parseInt(sumTotal);
    }

    function deIncrement(totalPriceDeValue) {
        sumTotal -= totalPriceDeValue;
        return Number.parseInt(sumTotal);
    }

}

var viewCartList = function () {

    var btn = document.getElementById("btn_view");
    var cartList = document.getElementById("cartList");
    var totalList = document.getElementsByClassName("total-title");

    if (btn.innerHTML == "Sepeti Kapat") {
        btn.innerHTML = "Sepeti Görüntüle";
        cartList.classList.remove('showCart');
        cartList.classList.add('hideCart');
    }
    else if (btn.innerHTML == "Sepeti Görüntüle") {
        btn.innerHTML = "Sepeti Kapat";
        cartList.classList.remove('hideCart');
        cartList.classList.add('showCart');
    }
};

var sepetiOnayla = function () {
    var memberForm = document.getElementById("member_form");
    var services = document.getElementById("services_price");

    services.style.transform = "rotateY(180deg)";
    memberForm.style.transform = "rotateY(0deg)";

}

var teklifedon = function () {

    var memberForm = document.getElementById("member_form");
    var services = document.getElementById("services_price");

    services.style.transform = "rotateY(0deg)";
    memberForm.style.transform = "rotateY(180deg)";
}

$("#form_price").submit(function (event) {
    event.preventDefault();
    var form = $(this);
    var actionUrl = form.attr('action');

    $.ajax({

        type: "POST",
        url: actionUrl,
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: form.serialize(),
        success: function (data) {

            var memberForm = document.getElementById("member_form");
            var doneform = document.getElementById("done_form");
            memberForm.style.transform = "rotateY(180deg)";
            doneform.style.transform = "rotateY(0deg)";
        },
        error: function (data) {
        }

    });

})

var removeFromTable = function (id) {

    var count = document.getElementById("count-price");
    var price = document.getElementById("price_" + id).value;
    var getRow = document.getElementById("tableRow_" + id);

    getRow.remove();
    count.innerHTML = decrementTotalBox(price);

    $.ajax({
        type: "POST",
        url: "/anasayfa/sepetcikar/",
        data: { IdRemove: id },
        success: function (data) {
        }
    });

    function decrementTotalBox(price) {
        currentTotal = currentTotal - price;
        console.log("Yeni Fiyat: " + currentTotal);
        return currentTotal;
    }
}