


$("#addCustomerForm button").click(function () {

    $.ajax({
        url: '/api/Customers/',
        method: 'POST',
        data: {
            "FirstName": $("#addCustomerForm [name=FirstName]").val(),
            "LastName": $("#addCustomerForm [name=LastName]").val(),
            "Age": $("#addCustomerForm [name=Age]").val(),
            "Email": $("#addCustomerForm [name=Email]").val(),
            "Gender": $("#addCustomerForm [name=Gender]").val(),

        }

    })
        .done(function (result) {

            alert(`Success! Result = ${result}`)
            console.log("Success!", result)

        })

        .fail(function (xhr, status, error) {

            alert( "fail");
            console.log("Error", xhr, status, error);

        })

});


$("#showAllCustomers button").click(function () {

    $.ajax({
        url: '/api/Customers/',
        method: 'GET',
        data: {

        }

    })
        .done(function (result) {

            var list = ''
            for (i = 0; i < result.length; i++) {
                list += "förnamn: " + result[i].firstName + " efternamn: " + result[i].lastName + " ålder: " + result[i].age + " email: " + result[i].email + " kön: " + result[i].gender + "<button class='deleteButton' data-id='" + result[i].id + "'>tabort</button>" + "<button class='editButton' data-id='" + result[i].id + "'>ändra</button>" + "<br>";
            };
            $('#showAllCustomersId').html(list);

            console.log(status);

        })

        .fail(function (xhr, status, error) {

            alert(`Fail!`)
            console.log("Error", xhr, status, error);

        })
});

$("body").on("click", ".deleteButton", function () {

    let clickedId = $(this).data("id")
    console.log(clickedId)
    $.ajax({
        url: '/api/Customers/',
        method: 'DELETE',
        data: {
            clickedId
        }

    })
        .done(function (result) {
            alert(result)
            console.log(status);

        })

        .fail(function (xhr, status, error) {

            alert(`Fail!`)
            console.log("Error", xhr, status, error);

        })
});

$("body").on("click", ".editButton", function () {

    let clickedId = $(this).data("id")
    console.log(clickedId)
    $.ajax({
        url: '/api/Customers/',
        method: 'PUT',
        data: {
            clickedId
        }

    })
        .done(function (result) {

            //"<input name='FirstName' value='+result+' /><input name='LastName' value='Svensson' /><input name='Age' value='99' /><input name='Email' value='Sven@svensson.com' /><input name='Gender' value='other' /><button>spara ändringar</button>"
            
            $('#editCustomerForm').html("<input name='FirstName' value=" + result.firstName + " />" + "<input name='LastName' value=" + result.lastName + " />" + "<input name='Age' value=" + result.age + " />" + "<input name='Email' value=" + result.email + " />" + "<input name='Gender' value=" + result.gender + " />" + "<input name='Id' type='hidden' value=" + result.id + " readonly/>" + "<button class='saveEditButton' data-id='" + result.id + "'>spara ändringar</button>");

            //$("#addCustomerForm [name=FirstName]").val(result.firstName),
            //    $("#addCustomerForm [name=LastName]").val(result.lastName),
            //    $("#addCustomerForm [name=Age]").val(result.age),
            //    $("#addCustomerForm [name=Email]").val(result.email),
            //    $("#addCustomerForm [name=Gender]").val(result.gender)
            //                    alert()
            console.log(status);

        })

        .fail(function (xhr, status, error) {

            alert(`Fail!`)
            console.log("Error", xhr, status, error);

        })




});

$("body").on("click", ".saveEditButton", function () {

 
    
    $.ajax({
        url: '/api/Customers/SaveEdit',
        method: 'POST',
        data: {
            "FirstName": $("#editCustomerForm [name=FirstName]").val(),
            "LastName": $("#editCustomerForm [name=LastName]").val(),
            "Age": $("#editCustomerForm [name=Age]").val(),
            "Email": $("#editCustomerForm [name=Email]").val(),
            "Gender": $("#editCustomerForm [name=Gender]").val(),
            "Id": $("#editCustomerForm [name=Id]").val(),
        }

    })
        .done(function (result) {

            alert('success')
            $("#editCustomerForm").html('');
            console.log(status);

        })

        .fail(function (xhr, status, error) {

            alert(`Fail!`)
            console.log("Error", xhr, status, error);

        })



});

$("body").on("click", "#mailServerButton", function () {

    $.ajax({
        url: '/api/Customers/mailServerInfo',
        method: 'GET',
        data: {
        }

    })
        .done(function (result) {
            $("#showMailServerId").html("mailserver: "+result.mailServerHostName+" sendmail: "+result.sendMail+" log sent mail: "+result.logEverySentMail+" blind copy adress: "+result.blindCopyAddresses);
           
            console.log(status);

        })

        .fail(function (xhr, status, error) {

            alert(`Fail!`)
            console.log("Error", xhr, status, error);

        })



});