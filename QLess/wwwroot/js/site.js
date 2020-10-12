
$(document).ready(function () {

    function cardIdFpcus() {
        $('#cardIdKey').show();
        $('#amountKey').hide();
        $('#cashAmountKey').hide();
    };

    function CardIdFpcus() {
        $('#CardId').focus();
        $('#CardIdKey').show();
        $('#controlNumberKey').hide();
    };

    function controlNumberFocus() {
        $('#CardIdKey').hide();
        $('#controlNumberKey').show();
    };

    function amountFocus() {
        $('#cardIdKey').hide();
        $('#amountKey').show();
        $('#cashAmountKey').hide();
    };

    function cashAmountFocus() {
        $('#cardIdKey').hide();
        $('#amountKey').hide();
        $('#cashAmountKey').show();
    };

    function cardOnload() {
        $('#amountKey').hide();
        $('#cashAmountKey').hide();

        $('#CardIdKey').show();
        $('#controlNumberKey').hide();
    }

    function cardKeysHide() {
        $('#cardIdKey').hide();
        $('#amountKey').hide();
        $('#cashAmountKey').hide();
    }

    function clearModal() {
        $('input[type="text"]').val("");
        $('#change').text(0);
        cardIdFpcus();
    }

    cardOnload();

    $('.closeModal').click(function () {
        clearModal();
    });

    $('#cardIdEnter').click(function () {
        $('#amount').focus();
        amountFocus();
    });

    $('#amountEnter').click(function () {
        $('#cashAmount').focus();
        cashAmountFocus();
    });

    $('#cashAmountEnter').click(function () {
        var amount = parseFloat($('#amount').val());
        var cashAmount = parseFloat($('#cashAmount').val());

        var change = (cashAmount - amount);

        $('#change').text(change);

        $('#submitLoad').focus();
    });

    $('#portalDiscount').click(function () {
        CardIdFpcus();
    });

    $('#CardIdEnter').click(function () {
        $('#controlNumber').focus();
        controlNumberFocus();
    });

    $('#controlNumberEnter').click(function () {
        $('#submitDiscount').focus();
        $('#CardIdKey').hide();
        $('#controlNumberKey').hide();
    });


    //$('#controlNumber').click(function () {
    //    $('#CardIdKey').hide();
    //    $('#controlNumberKey').show();
    //});

    $('#cashAmount').on('input', function (e) {

        var amount = parseFloat($('#amount').val());
        var cashAmount = parseFloat($('#cashAmount').val());

        $('#cashAmount').focus();

        var change = (cashAmount - amount);

        $('#change').text(change);

    });


    $("body").on("click", "#submitDiscount", function () {

        var cardId = $('#_CardId').val();
        var cardProvided = $("input[name='optradio']:checked").val();
        var controlNumber = $('#controlNumber').val().replace(/-/gi, "");

        var cardIdcount = $('#_CardId').val().replace(/-/gi, "");

        if (cardIdcount.length == 32) {
            if (cardId != "" && cardProvided != "" && controlNumber != "") {
                $.ajax({
                    method: "POST",
                    url: link = location.origin + "/api/Card/SetCardToDiscounted",
                    data: { cardId: cardId, cardProvided: cardProvided, controlNumber: controlNumber },
                    datatype: "json"
                }).done(function (msg) {
                    CardIdFpcus();
                    alert(msg);
                }).fail(function (msg) {
                    alert(msg);
                });
            }
            else {
                alert("Kindly Fill out the form.");
                CardIdFpcus();
            }
        }
        else {
            alert("Card Id is invalid.");
        }
        
    });

    $("body").on("click", "#submitLoad", function () {

        var cardId = $('#cardId').val();
        var amount = $('#amount').val();

        var cardIdcount = cardId.replace(/-/gi, "");

        if (cardIdcount.length == 32) {
            if (cardId != "" && amount != "") {
                $.ajax({
                    method: "POST",
                    url: link = location.origin + "/api/Card/Load",
                    data: { cardId: cardId, amount: amount },
                    datatype: "json"
                }).done(function (msg) {
                    cardIdFpcus();
                    alert(msg);
                }).fail(function (msg) {
                    cardIdFpcus();
                    alert(msg);
                });
            }
        }
        else {
            cardIdFpcus();
            alert("Card Id is invalid.");
        }

    });

});



