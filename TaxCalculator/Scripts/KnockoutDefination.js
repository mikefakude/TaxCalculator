function formatCurrently(value) {
    return "R " + value.toFixed(2);
}

function CalculatorViewModel() {

    //Make koDefine as this reference
    var koDefine = this;

    koDefine.Id = ko.observable("");
    koDefine.DateTime = ko.observable("");
    koDefine.PostalCode = ko.observable("");
    koDefine.TaxIncome = ko.observable("");
    ko.CalculatedValue = ko.observable("");

    var Calculator = {
        Id: koDefine.Id,
        DateTime: koDefine.DateTime,
        PostalCode: koDefine.PostalCode,
        TaxIncome: koDefine.TaxIncome,
        CalculatedValue: koDefine.CalculatedValue
    };

    koDefine.Calculator = ko.observable();
    koDefine.Index = ko.observableArray();

    //Get All Added Values
    $.ajax({
        url: 'Calculator/GetAllAddedValues',
        cache: false,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function (data) {
            koDefine.Index(data); 
        }
    });

    //Add Income for Tax
    koDefine.create = function () {
        if (Calculator.PostalCode() != "" && Calculator.TaxIncome() != "") {
            $.ajax({
                url: 'Calculator/AddValues',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(Calculator),
                success: function (data) {
                    koDefine.Index.push(data);
                    koDefine.PostalCode("");
                    koDefine.TaxIncome("");
                }
            }).fail(
                function (xhr, textStatus, err) {
                    alert(err);
                });
        }
        else {
            alert('Please Enter All the Values!');
        }
    }

    // Update Values
    koDefine.update = function () {
        var calculator = koDefine.Calculator();

        $.ajax({
            url: 'Calculator/EditValues',
            cache: false,
            type: 'PUT',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(calculator),
            success: function (data) {
                koDefine.Index.removeAll();
                koDefine.Index(data);
                koDefine.Calculator(null);
                alert("Record Updated Successfully");
            }
        }).fail(
          function (xhr, textStatus, err) {
               alert(err);
         });
    }

    // Delete Values
    koDefine.delete = function (Calculator) {
        if (confirm('Are you sure to Delete "' + Calculator.PostalCode + '" Postal Code ??')) {
            var id = Calculator.Id;

            $.ajax({
                url: 'Calculator/DeleteValuesFromTax/' + id,
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: id,
                success: function (data) {
                    koDefine.Index.remove(Calculator);
                }
            }).fail(
                function (xhr, textStatus, err) {
                    koDefine.status(err);
             });
        }
    }

    // Edit values
    koDefine.edit = function (calculator) {
        koDefine.Calculator(calculator);
    }

    // Reset the form
    koDefine.reset = function () {
        koDefine.PostalCode("");
        koDefine.TaxIncome("");
    }

    koDefine.cancel = function () {
        koDefine.Calculator(null);
    }
}
var viewModel = new CalculatorViewModel();
ko.applyBindings(viewModel);


