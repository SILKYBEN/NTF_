/// <reference path="App.js" />
/// <reference path="../Services/CaseService.js" />

application.controller('indexController', function ($scope, $window, $location) {
    $(document).ready(function () {
        var playerOrNot;
        var type_ln_ln;
        getNews();
        $("#agediv").hide();
        $("#cluboraffildiv").hide();
        
        $("#pleasewaitReg").hide();
        $("#pleasewaitLog").hide();

        
        $(".getStarted").click(function () {
            $("#agediv").removeClass('hiddenDiv');
            $("#categoryDiv__").removeClass('hiddenDiv');
            $("#coachOrUmpire").addClass('hiddenDiv');
            playerOrNot = '';
            if ($(this).hasClass('separate_')) {
                $("#coachOrUmpire").removeClass('hiddenDiv');
                $("#categoryDiv__").addClass('hiddenDiv');
                $("#agediv").addClass('hiddenDiv');
                playerOrNot = 'not';
                if ($(this).hasClass('umpire'))
                    type_ln_ln = 'Umpire';
                else
                    type_ln_ln = 'Coach';
            }
        });
        $("#addUserButton").click(function () {
            $("#confErr").html('');
            $("#cluboraffildivvalErr").html("");
            if ($("#pass").val() != $("#conf").val()) {
                $("#confErr").html('Password Mismatch');
                return;
            }
            var d = new Date($("#dob").val());
            var dateMainStart = d.getDay() + "/" + d.getMonth() + "/" + d.getFullYear();

            var catgegoryName = $("#Rank").val();
            var insName = '';
            var genderMain = $("#gender").val();
            var ageGroup = $("#age").val();

            //if (!(catgegoryName === 'professional' || catgegoryName === 'Wheelchair' || catgegoryName === 'Affiliation' || catgegoryName === 'Club')) {
            //    genderMain = $("#gender").val();
            //    ageGroup =  $("#age").val();
            //}

            if (catgegoryName === 'affiliation' || catgegoryName === 'club') {
                genderMain = '';
                ageGroup = '';
                if ($("#cluboraffildivval").val() === '') {
                    $("#cluboraffildivvalErr").html("Please fill form completely");
                    return;
                }
                insName = $("#cluboraffildivval").val();
            }
            
            $("#addUserButton").prop('disabled', true);
            $("#addUserButton").html('Please wait');
            if (playerOrNot.trim()!='') {
                $.post("AppCode/NTFService.asmx/AddCoachOrUmpire",
                {
                    sname: $("#surname").val(),
                    othername: $("#othername").val(),
                    username_ln: $("#username").val(),
                    email_ln: $("#email").val(),
                    phone_ln: $("#phone").val(),
                    gender_ln: genderMain,
                    dob_ln: dateMainStart,
                    pass_ln: $("#conf").val(),

                    cert_ln: $("#certLevel").val(),
                    type_ln: type_ln_ln
                },
                function (data, status) {
                    if (data.trim() === '1') {
                        swal({
                            title: "Congrats!",
                            text: "Please Check your email for your N-Pin",
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
                    $("#addUserButton").prop('disabled', false);
                    $("#addUserButton").html('Sign Up');
                });
            }
            else{

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
                            title: "Congrats!",
                            text: "Please Check your email for your N-Pin",
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
                    $("#addUserButton").prop('disabled', false);
                    $("#addUserButton").html('Sign Up');
                });
             }
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

        function getNews() {
            $.post("AppCode/NTFService.asmx/GetNews",
      {
          type: 'limited'

      },
      function (data, status) {
          console.log(data);
          divRepeater(JSON.parse(data));
      });
        }


        function divRepeater(domObj) {
            var html = "";
            $.map(domObj, function (data) {
                var elm2 = "<li>" +
                    "<article class=entry-item>" +
                                        "<div class=entry-thumb>" +
                                            "<a href=#><img src=Admin/NewsImages/"+data.NewsImage+" alt=></a>" +
                                        "</div>" +
                                        "<div class=entry-content>" +
                                            "<div class=content-top>" +
                                                "<h4 class=entry-title itemscope=><a id=viewMoreButton itemprop=name href=#>" + data.NewsTitle + "</a></h4>" +

                                            "</div>" +
                                                 "<p>" + data.NewsIntro + " </p>" +
                                            "<footer>" +
                                                "<p class=entry-author>by <a href=#>" + data.NewsAuthor + "</a></p>" +
                                            "</footer>" +
                                        "</div>" +
                                        "<div class=post-share-link style-bg-color>" +
                                            "<span><i class=fa fa-share-alt></i></span>" +
                                            "<ul>" +
                                                "<li><a href=# class=fa fa-facebook></a></li>" +
                                                "<li><a href=# class=fa fa-twitter></a></li>" +
                                                "<li><a href=# class=fa fa-google-plus></a></li>" +
                                            "</ul>" +
                                        "</div>" +
                                    "</article>" +
                                "</li>";
                html = html + elm2;
            });
            $("#divnews").append(html);
        }

        $(document).on("click", "#viewMoreButton", function () {
            var id = $(this).closest("h4").children("a:eq(0)").text();
            window.location = "newdetails.html?title=" + id;
        })
    });
});