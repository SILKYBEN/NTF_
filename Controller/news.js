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
      
    });
});