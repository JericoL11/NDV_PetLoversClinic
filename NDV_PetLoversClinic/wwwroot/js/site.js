// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code

//for adding new number
$(document).ready(function () {
    const updateButtonState = () => $('#addContact').prop('disabled', $('.contact input').length >= 2);

    $('#addContact').click(function () {
        if ($('.contact input').length < 2) {
            const nextIndex = $('.contact input').length;
            const newInput = $(`
                        <div class="d-flex align-items-center mb-3 mt-2">
                            <div class="contact-item flex-grow-1">
                                <input type="text" class="form-control" name="IContact[${nextIndex}].contactNo" placeholder="Enter contact number" required>
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

/*form-add-for-pet*/
$(document).ready(function () {
    // Function to update the name attributes of the rows
    function updateRowNames() {
        $('#PetTable tbody tr').each(function (index) {
            $(this).find('input, select').each(function () {
                var name = $(this).attr('name');
                if (name) {
                    var newName = name.replace(/\d+/, index);
                    $(this).attr('name', newName);
                }
            });
        });
    }

    // Add row
    $('#addRow').click(function () {
        var rowCount = $('#PetTable tbody tr').length;
        var newRow = $('.detailRow').first().clone();

        newRow.find('input, select').each(function () {
            var name = $(this).attr('name');
            if (name) {
                var newName = name.replace(/\d+/, rowCount);
                $(this).attr('name', newName);
            }
            if ($(this).attr('id') !== 'dateInput') {
                $(this).val(''); // Clear the value of the input/select except dateInput
            }
        });

        newRow.appendTo($('#PetTable tbody'));
    });

    // Remove row
    $(document).on('click', '.removeRow', function () {
        if ($('#PetTable tbody tr').length > 1) {
            $(this).closest('tr').remove();
            updateRowNames(); // Update the name attributes of the remaining rows
        }
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