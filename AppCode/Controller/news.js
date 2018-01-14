/// <reference path="App.js" />
/// <reference path="../Services/CaseService.js" />

application.controller('news', function ($scope, $window, $location) {
    $(document).ready(function () {
        var correspondingID ;
        $("ul.clearfix li").click(function (e) {
            correspondingID  = $(this).attr('id');
            localStorage['ID'] = correspondingID;          
        })
        $("#" + localStorage['ID']).removeClass('hidden');
      
            //                $(".style-sub-1").hide();
            //              
            //$(".genderDiv").show();
            //$('#agediv').hide();
            //$("#cluboraffildiv").hide();
            //if (correspondingID === 'senior') {
            //    document.getElementById("age").options.length = 0;
            //    $('#age').append('<option value="40-45">40-45</option>');
            //    $('#age').append('<option value="51-55">51-55</option>');
            //    $('#age').append('<option value="56-60">56-60</option>');
            //    $('#age').append('<option value="61-65">61-65</option>');
            //    $('#age').append('<option value="66-70">66-70</option>');
            //    $('#age').append('<option value="71-75">71-75</option>');
            //    $('#age').append('<option value="76-80">76-80</option>');
            //    $('#agediv').show();
            //}
            //else if (correspondingID === 'junior') {
            //    document.getElementById("age").options.length = 0;
            //    $('#age').append('<option value="<10"><10</option>');
            //    $('#age').append('<option value="<12"><12</option>');
            //    $('#age').append('<option value="<14"><14</option>');
            //    $('#age').append('<option value="<16"><16</option>');
            //    $('#age').append('<option value="<18"><18</option>');
            //    $('#agediv').show();
            //}
            //else if (correspondingID === 'affiliation') {
            //    $(".genderDiv").hide();
            //    $("#cluboraffildiv").show();
            //    $("#pofsomething").html('Enter Name of Instituition');
            //}
            //else if (correspondingID === 'club') {
            //    $(".genderDiv").hide();
            //    $("#cluboraffildiv").show();
            //    $("#pofsomething").html('Enter Club Name');
            //}

            //                $(".style-sub-1").hide();
            //                $("#" + correspondingID).show();

    });
});