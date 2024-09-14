// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let currentStep = 1;

function showStep(step) {
    document.querySelectorAll('.step').forEach((element) => {
        element.style.display = 'none';
    });
    document.getElementById(`step-${step}`).style.display = 'block';
}

function nextStep(step) {
    currentStep = step + 1;
    showStep(currentStep);
}

function prevStep(step) {
    currentStep = step - 1;
    showStep(currentStep);
}

function submitWizard() {
    alert("Wizard completed!");
}

// Initially display the first step
showStep(currentStep);
