/// <reference path="App.js" />
/// <reference path="../Services/CaseService.js" />

application.controller('indexController', function ($scope, $window, $location) {
    $(document).ready(function () {
        $("#agediv").hide();
        $("#cluboraffildiv").hide();
        
        $("#pleasewaitReg").hide();
        $("#pleasewaitLog").hide();
        
        $("#addUserButton").click(function () {
            $("#confErr").html('');
            if ($("#pass").val() != $("#conf").val()) {
                $("#confErr").html('Password Mismatch');
                return;
            }
            $("#pleasewaitReg").show();
            var d = new Date($("#dob").val());
            var dateMainStart = d.getDay() + "/" + d.getMonth() + "/" + d.getFullYear();
            
            var catgegoryName = $("#Rank").val();
            var insName = '';
            var genderMain = '';
            var ageGroup = '';
            if (!(catgegoryName === 'professional' || catgegoryName === 'Wheelchair' || catgegoryName === 'affiliation' || catgegoryName === 'club')) {
                genderMain = $("#gender").val();
                ageGroup =  $("#age").val();
            }
            if (catgegoryName === 'affiliation' || catgegoryName === 'club') {
                genderMain = '';
                insName = $("#cluboraffildivval").val();
            }
            
            $("#addUserButton").prop('disabled', true);
            $.post("AppCode/NTFService.asmx/AddUser",
            {
                sname: $("#surname").val(),
                othername: $("#othername").val(),
                username_ln: $("#username").val(),
                email_ln: $("#email").val(),
                phone_ln: $("#phone").val(),
                gender_ln: genderMain,
                dob_ln: dateMainStart,
                pass_ln: $("#conf").val(),
                
                cat_ln: catgegoryName,
                age_ln: ageGroup,
                ins_ln: insName
            },
            function (data, status) {
                if (data.trim() === '1') {
                    swal({
                        title: "Raah!",
                        text: "User Added, Check your email for your NTF Pin",
                        icon: "success",
                    });
                }
                else {
                    swal({
                        title: "Ooops!",
                        text: "Please Try Again!",
                        icon: "error",
                    });
                }
                $("#surname").val('')
                $("#othername").val('')
                $("#username").val('')
                $("#email").val('')
                $("#phone").val('')
                $("#pleasewaitReg").hide();
                $("#addUserButton").prop('disabled', false);
            });
        });


        $("#loginB").click(function () {
            $("#pleasewaitLog").show();
            $("#loginB").prop('disabled', true);
            var email = $("#emailLogin").val();
            $.post("AppCode/NTFService.asmx/VerifyLogin",
            {
                email_ln: email,
                pass_ln: $("#passwordLogin").val()

            },
            function (data, status) {
                if (data === '1') {
                    localStorage["mail"] = email;
                    swal({
                        title: "Raah!",
                        text: "Correct Credentials!",
                        icon: "success",
                    });
                }
                else {

                    $("#div_alert").html('');
                    jQuery("#div_alert").fadeIn('fast');
                    $("#div_alert").html(data);
                    setTimeout(function () {
                        jQuery("#div_alert").fadeOut('slow');
                    }, 5000);
                    $("#pleasewaitLog").hide();
                    $("#loginB").prop('disabled', false);
                }
            });
        });

        $scope.validatePass = function () {
            $("#confErr").html('');
            if ($("#pass").val() != $("#conf").val()) {
                $("#confErr").html('Password Mismatch');
                return;
            }
        }
        
            $("#Rank").change(function(){
                var correspondingID = $(this).find(":selected").val().trim();
                $(".genderDiv").show();
                $('#agediv').hide();
                $("#cluboraffildiv").hide();
                if(correspondingID === 'senior'){
                    document.getElementById("age").options.length = 0;
                    $('#age').append('<option value="40-45">40-45</option>');
                    $('#age').append('<option value="51-55">51-55</option>');
                    $('#age').append('<option value="56-60">56-60</option>');
                    $('#age').append('<option value="61-65">61-65</option>');
                    $('#age').append('<option value="66-70">66-70</option>');
                    $('#age').append('<option value="71-75">71-75</option>');
                    $('#age').append('<option value="76-80">76-80</option>');
                    $('#agediv').show();
                }
                else if(correspondingID === 'junior'){
                    document.getElementById("age").options.length = 0;
                    $('#age').append('<option value="<10"><10</option>');
                    $('#age').append('<option value="<12"><12</option>');
                    $('#age').append('<option value="<14"><14</option>');
                    $('#age').append('<option value="<16"><16</option>');
                    $('#age').append('<option value="<18"><18</option>');
                    $('#agediv').show();
                }
                else if(correspondingID === 'affiliation'){
                    $(".genderDiv").hide();
                    $("#cluboraffildiv").show();
                    $("#pofsomething").html('Enter Name of Instituition');
                }
                else if (correspondingID === 'club'){
                     $(".genderDiv").hide();
                     $("#cluboraffildiv").show();
                     $("#pofsomething").html('Enter Club Name');
                }
                
//                $(".style-sub-1").hide();
//                $("#" + correspondingID).show();

})
    });
});