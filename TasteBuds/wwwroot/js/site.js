// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// File: wwwroot/js/site.js

$(document).ready(function () {
    $('#searchButton').on('click', function () {
        var query = $('#searchInput').val();
        if (query) {
            window.location.href = '/Search/Results?query=' + encodeURIComponent(query);
        }
    });

    $('#searchInput').on('keypress', function (e) {
        if (e.which === 13) { // Enter key pressed
            $('#searchButton').click();
        }
    });
});
