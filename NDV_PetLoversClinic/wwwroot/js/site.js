// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code




//Breed Row
$(document).ready(function () {
    let breedIndex = 1; // Initialize index for new breed rows

    // Add breed row
    $('#addBreedRow').on('click', function () {
        const rowCount = $('#breedRow .form-group').length;

        // Allow adding row only if there are less than 5 rows
        if (rowCount < 5) {
            const newBreedRow = `
                            <div class="form-group d-flex align-items-center mb-2">
                            <div class="form-floating flex-grow-1 mb-2">
                                <input class="form-control" id="Breed[${breedIndex}]" name="Breeds[${breedIndex}].breed_Name" placeholder="Breed Name" required />
                                <label class="text-secondary" for="Breed[${breedIndex}]"><span> * </span>Breed Name</label>
                            </div>
                               
                                <button type="button" class="btn btn-outline-danger border-0 removeRow ms-2">
                                    <i class="bi bi-trash-fill"></i>
                                </button>
                            </div>
                        `;
            $('#breedRow').append(newBreedRow);
            breedIndex++; // Increment index for next breed row
        } else {
            alert('You can only add up to 5 rows.');
        }
    });

    // Remove breed row
    $('#breedRow').on('click', '.removeRow', function () {
        const rowCount = $('#breedRow .form-group').length;

        // Allow removal only if there are more than 1 row
        if (rowCount > 1) {
            $(this).closest('.form-group').remove();

            // Re-adjust the indexes after removing a row
            $('#breedRow .form-group').each(function (index) {
                $(this).find('input').attr('name', `Breeds[${index}].breed_Name`);
            });
            breedIndex--; // Decrement index after removing a row
        } else {
            alert('At least one breed row must remain.');
        }
    });
});

//for adding new number
$(document).ready(function () {
    const updateButtonState = () => $('#addContact').prop('disabled', $('.contact input').length >= 2);

    $('#addContact').click(function () {
        if ($('.contact input').length < 2) {
            const nextIndex = $('.contact input').length;
            const newInput = $(`
                        <div class="d-flex align-items-center mb-3 mt-2">
                         <div class="form-floating flex-grow-1 me-2">
                            <input 
                                type="text" 
                                class="form-control"
                                name="IContact[${nextIndex}].contactNo" 
                                id="contactNo" 
                                maxlength="11"
                                placeholder="Enter contact number" 
                                required 
                            />
                            <label for="contactNo"><span> * </span>Contact No.</label>
                        </div>
                            <button type="button" class="btn btn-outline-danger removeContact border-0">
                                <i class="bi bi-trash"></i>
                            </button>
                        </div>`);

            newInput.find('.removeContact').click(function () {
                if ($('.contact input').length > 1) {
                    $(this).closest('.d-flex').remove();
                    updateButtonState();
                } else {
                    alert('At least one contact field must remain.');
                }
            });

            $('#contactContainer').append(newInput);
            updateButtonState();
        } else {
            alert('You can only add a maximum of 2 contacts.');
        }
    });

    updateButtonState();
});

//contact Number
$(document).ready(function () {
    // Restrict input to numbers only
    $(document).on('input', 'input[name^="IContact"]', function () {
        // Allow only numeric characters
        this.value = this.value.replace(/[^0-9]/g, '');
    });
});

//max date for input type date
$(document).ready(function () {
    // Get the current date
    var today = new Date().toISOString().split('T')[0];

    // Set max attribute to today's date
    $('.maxDate').attr('max', today);
});

//minimum date - not include past dates
$(document).ready(function () {
    // Get the current date in YYYY-MM-DD format
    var today = new Date().toISOString().split('T')[0];

    // Set min attribute to today's date to prevent selecting past dates
    $('.minDate').attr('min', today);
});


// Auto-fill age field based on birthdate // owner create pets
$(document).on('change', '.birthdate', function () {
    var birthdate = $(this).val();
    var age = calculateAge(birthdate);
    $(this).closest('tr').find('.age').val(age);
});

function setMaxBirthdate() {
    var today = new Date().toISOString().split('T')[0];
    $('.birthdate').attr('max', today);
}

// Auto-fill age field based on birthdate // owner create pets
$(document).on('change', '.birthdate', function () {
    var birthdate = $(this).val();
    var age = calculateAge(birthdate);
    $(this).closest('tr').find('.age').val(age);
});


//pets
/*
// Auto-fill age field based on birthdate
$(document).on('change', '.birthdate', function () {
    var birthdate = $(this).val();
    var age = calculateAge(birthdate);
    $(this).closest('.petrow').find('.age').val(age);
});


// Function to calculate age
function calculateAge(birthdate) {
    var today = new Date();
    var birthDate = new Date(birthdate);
    var age = today.getFullYear() - birthDate.getFullYear();
    var month = today.getMonth() - birthDate.getMonth();
    if (month < 0 || (month === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}*/

setMaxBirthdate(); // Set max date on initial load