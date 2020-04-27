// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Tooltips Initialization
$(function () {
  $('[data-toggle="tooltip"]').tooltip()
})

(function() {
'use strict';
window.addEventListener('load', function() {
// Fetch all the forms we want to apply custom Bootstrap validation styles to
var forms = document.getElementsByClassName('needs-validation');
// Loop over them and prevent submission
var validation = Array.prototype.filter.call(forms, function(form) {
form.addEventListener('submit', function(event) {
if (form.checkValidity() === false) {
event.preventDefault();
event.stopPropagation();
}
form.classList.add('was-validated');
}, false);
});
}, false);
})();

var subjects = [
"Where's My Stuff?",
"Cancel Items or Orders",
"Returns & Refunds",
"Shipping Rates & Information",
"Change Your Payment Method",
"Manage Your Account Information",
"About Two-Step Verification",
"Cancel Items or Orders",
"Change Your Order Information",
"Contact Third-Party Sellers",
"More in Managing Your Orders"
];

$('#form-autocomplete-2').mdbAutocomplete({
data: subjects
});

