﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
    'use strict' var
        forms =
            document.querySelectorAll('.needs-validation');

    // Function to validate individual input fields
    function
        validateField(input) {
        var errorMessage
            = input.title
            || input.validationMessage; // Use title attribute if present
        var errorElement
            = input.nextElementSibling; // Assuming .invalid-feedback is the next sibling

        if (!input.checkValidity()) {
            input.classList.add('is-invalid'); input.classList.remove('is-valid');
            if (errorElement
                && errorElement.classList.contains('invalid-feedback')) {
                errorElement.innerText =
                    errorMessage;
            }
            else {
                // Create a new element if not found
                errorElement =
                    document.createElement('div'); errorElement.classList.add('invalid-feedback',
                        'd-block'); errorElement.innerText
                            = errorMessage;
                input.parentNode.insertBefore(errorElement,
                    input.nextSibling);
            }
        }
        else {
            input.classList.add('is-valid'); input.classList.remove('is-invalid');
            if (errorElement
                && errorElement.classList.contains('invalid-feedback')) {
                errorElement.innerText =
                    '';
            }
        }
    }

    // Add event listeners to all forms with the needs-validation class
    Array.prototype.slice.call(forms).forEach(function (form) {
        form.addEventListener('submit',
            function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault(); event.stopPropagation();
                }
                form.classList.add('was-validated');
            },
            false);

        // Add blur event listener to each form control
        form.querySelectorAll('input, select, textarea').forEach(function
            (input) {
            input.addEventListener('blur',
                function () {
                    validateField(input);
                }
            );
        }
        );
    }
    );
})();
