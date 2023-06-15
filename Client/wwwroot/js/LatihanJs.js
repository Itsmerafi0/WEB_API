/*const animals = [
    { name: "nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "gary", species: "mouse", class: { name: "mamalia" } },
    { name: "dory", species: "fish", class: { name: "invertebrata" } },
    { name: "tom", species: "mouse", class: { name: "mamalia" } },
    { name: "aji", species: "wibu", class: { name: "mamalia" } }
];

console.log(animals);


//soal 1
*//*for (let i = 0; i < animals.length; i++) {
    if (animals[i].species !== "mouse") {
        animals[i].class.name = "Non-Mamalia";
    }
}
console.log(animals);
*//*
//soal 2

const onlyMouse = animals.filter(animal => animal.species === "mouse");
console.log(onlyMouse);

//soal 2

*//*const Mouse = [];

animals.forEach(animal => {
    if (animal.species === "mouse") {
        Mouse.push(animal);
    }
});

console.log(Mouse);
*//*
//soal 1

*//*const onlyMouse = [];
*//*
*//*animals.forEach(animal => {
    if (animal.species !== "mouse") {
        onlyMouse.push(animal);
    } else {
        animal.class.name = "non-mamalia";
    }
});

console.log(onlyMouse);
*//*
const ubah = animals.map(x => {
    return {
        name: x.name,
        species: x.species,
        isMouse: x.species == "mouse" ? true:false
    }
})

console.log(ubah);*/

/*$.ajax({
    url: "https://swapi.dev/api/people/",
}).done((result) => {
    let table = "<tr><th>No.</th><th>Name</th><th>Height</th><th>Mass</th><th>Hair Color</th><th>Skin Color</th></tr>";
    $.each(result.results, (index, person) => {
        index += 1
        table += "<tr>";
        table += "<td>" + index + "</td>";
        table += "<td>" + person.name + "</td>";
        table += "<td>" + person.height + "</td>";
        table += "<td>" + person.mass + "</td>";
        table += "<td>" + person.hair_color + "</td>";
        table += "<td>" + person.skin_color + "</td>";
        table += "</tr>";
    });
    console.log(table);
    $("#starwar").html(table);
}).fail((error) => {
    console.log(error);
});
*/


$(document).ready(function () {
    var genderString = "";
    $('#Employee').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        ajax: {
            url: "https://localhost:7076/api/Employee",
            method: "GET",
            dataSrc: "data",
            dataType: "JSON"
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "guid" },
            { data: "nik" },
            { data: "firstName" },
            { data: "lastName" },
            { data: "birthDate" },
            {
                data: "gender",
                render: function (data) {
                    return data === 0 ? "Female" : "Male";
                }
            },
            { data: "hiringDate" },
            { data: "email" },
            { data: "phoneNumber" },
            {
                render: function () {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#employeeModal" onclick="openAddEmpModal()">Add</button>`;
                }
            },
            {
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#employeeUpdateModal" onclick="openUpdateEmployeeModal('${row?.guid}', '${row?.nik}', '${row?.firstName}', '${row?.lastName}', '${row.birthDate}', '${row.gender}', '${row.hiringDate}', '${row.email}', '${row.phoneNumber}')">Update</button>`;
                }
            },            {
                render: function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-danger" onclick="deleteEmployee('${row.guid}')">Delete</button>`;
                }
            }
        ]
    });
});


/*$(document).ready(function () { 
    let table = $("#tablePoke").DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ],
        ajax: {
            url: "https://localhost:7076/api/Employee/GetAllMasterEmployee",
            dataSrc: "data",
            dataType: "JSON"
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { data: "guid" },
            { data: "nik" },
            { data: "fullName" },
            { data: "birthDate" },
            { data: "gender" },
            { data: "hiringDate" },
            { data: "email" },
            { data: "phoneNumber" },
            { data: "major" },
            { data: "degree" },
            { data: "gpa" },
            { data: "universityName" }
            {
                data: "url",
                render: function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#pokeModal" onclick="openModal('${data}')">Detail</button>`;
                }
            },
            {
                render: function () {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#employeeModal" onclick="openAddEmpModal()">Employee</button>`;
                }
            },
            {
                render: function () {
                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#registerModal" onclick="openRegModal()">Register</button>`;
                }
            }
        ]
    });
});*/


function openAddEmpModal() {
    $("#employeeModal").modal("show");
    
} function openRegisEmpModal1() {
    $("#employeeModal1").modal("show");
} 
function openRegModal() {
    $("#registerModal").modal("show");
} 


function AddEmployee() {
    var eNik = $('#employee-nik').val();
    var eFirst = $('#employee-fname').val();
    var eLast = $('#employee-lname').val();
    var eBDate = $('#employee-bdate').val();
    var eGender = document.querySelector('input[name="employee-gender"]:checked').id.includes('m') ? 1 : 0;
    var eHDate = $('#employee-hdate').val();
    var eEmail = $('#employee-email').val();
    var ePhone = $('#employee-pnumber').val();

    $.ajax({
        async: true, // Async by default is set to “true” load the script asynchronously  
        // URL to post data into sharepoint list  
        url: "https://localhost:7076/api/Employee",
        method: "POST", //Specifies the operation to create the list item  
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem' // it defines the ListEnitityTypeName  
            },
            //Pass the parameters
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'hiringDate': eHDate,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose", //It defines the Data format   
            "content-type": "application/json;odata=verbose", //It defines the content type as JSON  
/*            "X-RequestDigest": $("#__REQUESTDIGEST").val() //It gets the digest value   
*/        },
        success: function (data) {
            console.log(data);
        },
        error: function (error) {
            console.log(JSON.stringify(error));

        }

    })

}

function deleteEmployee(guid) {
    console.log(guid)
    if (confirm("Are you sure you want to delete this employee?")) {
        $.ajax({
            url: `https://localhost:7076/api/Employee/${guid}`,
            type: 'DELETE',
            success: function (result) {
                console.log(result);
                window.location.reload();
            }, error: function (xhr, status, error) {
                console.error('error occured: ', error)
            }
        });
    }
}

/*$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon?limit=100000&offset=0",
}).done((result) => {
    let trHTML = "";
    $.each(result.results, (key, val) => {
        trHTML += `<tr>
            <td>${key + 1}</td>
            <td>${val.name}</td>
            <td>
                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#pokeModal" onclick="openModal('${val.url}')">
                    Action</button></td>
        </tr>`;
    })
    console.log(trHTML);
    $("#wrapper-sw").append(trHTML);

}).fail((error) => {
    console.log(error);
})
*/
function removeProgressBars() {
    $(".container-progress").empty();
}
function openModal(url) {
    $.ajax({
        url: url,
        method: 'GET',
        success: function (data) {
            // Handle the data and open the modal
            console.log("data", data);
            $("#img-Pokemon").attr("src", data.sprites.other['official-artwork'].front_default);
            $("#exampleModalLabel").text(data.name);
            $("#height").text("Height : " + data.height).css({
                "text-transform": "uppercase",
                "color": "black",
                "font-weight" : "bold"

            });

            $("#weight").text("Weight : " + data.weight).css({
                "text-transform": "uppercase",
                "color": "black",
                "font-style": "italic"
            });


            $("#statsRadarChart").remove();
            $(".radar-chart-container").append('<canvas id="statsRadarChart"></canvas>');
            // Create a radar chart for the Pokémon's stats
            createRadarChart(data.stats);

            let typesHtml = '';
            data.types.forEach(function (type) {
                typesHtml += `<div class="type ${type.type.name}">${type.type.name}</div>`;

            });
            $("#pokemonTypes").html(typesHtml);


            // create variable for get api stats
            const baseStats = data.stats;
            const loadedStats = 0;

            console.log(baseStats);

            const hpStat = baseStats[0].base_stat;
            const attackStat = baseStats[1].base_stat;
            const deffStat = baseStats[2].base_stat;
            const specialAttackStat = baseStats[3].base_stat;
            const specialDeffStat = baseStats[4].base_stat;
            const speedStat = baseStats[5].base_stat;

            console.log("Hp:", hpStat);
            console.log("Attack:", attackStat);
            console.log("Deffend:", deffStat);
            console.log("Special Attack:", specialAttackStat);
            console.log("Special Deffend:", specialDeffStat);
            console.log("Speed:", speedStat);
            const progressBars = [
                { name: "Hp", stat: baseStats[0].base_stat},
                { name: "Attack", stat: baseStats[1].base_stat },
                { name: "Deffend", stat: baseStats[2].base_stat },
                { name: "Special Attack", stat: baseStats[3].base_stat },
                { name: "Special Deffend", stat: baseStats[4].base_stat },
                { name: "Speed", stat: baseStats[5].base_stat }
            ];

            $.each(progressBars, function (index, progressBar) {
                var progress = $("<div>").addClass("progress mx-auto");
                var progressBarInner = $("<div>")
                    .addClass("progress-bar")
                    .attr({
                        role: "progressbar",
                        style: "width: " + progressBar.stat + "% ;",
                        "aria-valuenow": progressBar.stat,
                        "aria-valuemin": "0",
                        "aria-valuemax": "100"
                    })
                    .text(progressBar.name + ": " + progressBar.stat);



                // Append the inner progress bar to the progress bar container
                progress.append(progressBarInner);

                // Append the progress bar to the wrapper element
                $(".container-progress").append(progress);

                
            });

            // Automatically change the color of the image
            var hueRotation = 1;
            setInterval(function () {
                hueRotation += 40; // Change this value to control the speed of color change
                $("#img-Pokemon").css("filter", "hue-rotate(" + hueRotation + "deg)");
            }, 4000); // Change this value to control the interval between color changes
        },



        error: function (error) {
            console.log(error);
        }
    });

    removeProgressBars();

    createRadarChart(baseStats);


    // Mengatur event click untuk menghapus dan menambahkan progress bar
    $(".button").click(function () {
        // Menghapus progress bar
        removeProgressBars();

    });
}


function createRadarChart(stats) {
    let statsData = {
        labels: ['HP', 'Attack', 'Defense', 'Speed', 'Special Attack', 'Special Defense'],
        datasets: [{
            label: 'Stats',
            data: [
                stats[0].base_stat,
                stats[1].base_stat,
                stats[2].base_stat,
                stats[5].base_stat,
                stats[3].base_stat,
                stats[4].base_stat
            ],
            backgroundColor: 'rgba(0, 123, 255, 0.5)',
            borderColor: 'rgba(0, 123, 255, 1)',
            pointBackgroundColor: 'rgba(0, 123, 255, 1)',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: 'rgba(0, 123, 255, 1)'
        }]
    };

    let radarChartOptions = {
        scale: {
            ticks: {
                beginAtZero: true,
                max: 200
            }
        }
    };

    let radarChart = new Chart($("#statsRadarChart"), {
        type: 'radar',
        data: statsData,
        options: radarChartOptions
    });

    // CSS animation
    $("#statsRadarChart").addClass("radar-chart-animation");
}

function validatePassword() {
        var passwordInput = document.getElementById("passwordInput");
    var confirmPasswordInput = document.getElementById("confirmPasswordInput");
    var errorMessage = document.getElementById("passwordError");

    if (passwordInput.value !== confirmPasswordInput.value) {
        errorMessage.style.display = "block";
        } else {
        errorMessage.style.display = "none";
        }
    
}


function openUpdateEmployeeModal(guid, nik, firstName, lastName, birthDate, gender, hiringDate, email, phoneNumber) {
    // Set the values of the input fields in the modal
    /*console.log('woi ini dari update: ', nik)*/
    document.getElementById('u-employee-nik').value = nik;
    document.getElementById('u-employee-fname').value = firstName;
    document.getElementById('u-employee-lname').value = lastName;
    document.getElementById('u-employee-bdate').value = birthDate;
    document.getElementById('u-employee-hdate').value = hiringDate;
    document.getElementById('u-employee-email').value = email;
    document.getElementById('u-employee-pnumber').value = phoneNumber;

    // Set the gender radio button based on the gender value
    if (gender == 0) {
        document.getElementById('u-employee-gender-f').checked = true;
    } else {
        document.getElementById('u-employee-gender-m').checked = true;
    }

    // Change the modal title
    document.getElementById('employeeUpdateModalTitle').innerText = 'Update Employee';

    // Show the modal
    $('#employeeUpdateModal').modal('show');

    // Add an event listener to the form submit button for updating the employee
    document.getElementById('employeeUpdateModalBody').addEventListener('submit', function (event) {
        event.preventDefault(); // Prevent form submission

        // Call the updateEmployee function with the GUID parameter
        updateEmployee(guid);
    });
}

function updateEmployee(guid) {
    // Retrieve the updated values from the input fields
    var eNik = document.getElementById('u-employee-nik').value;
    var eFirst = document.getElementById('u-employee-fname').value;
    var eLast = document.getElementById('u-employee-lname').value;
    var eBDate = document.getElementById('u-employee-bdate').value;
    var eGender = document.querySelector('input[name="u-employee-gender"]:checked').id.includes('f') ? 0 : 1;
    var eHDate = document.getElementById('u-employee-hdate').value;
    var eEmail = document.getElementById('u-employee-email').value;
    var ePhone = document.getElementById('u-employee-pnumber').value;

    $.ajax({
        url: `https://localhost:7076/api/Employee`,
        method: "PUT",
        data: JSON.stringify({
            '__metadata': {
                'type': 'SP.Data.EmployeeListItem'
            },
            'guid': guid,
            'nik': eNik,
            'firstName': eFirst,
            'lastName': eLast,
            'birthDate': eBDate,
            'gender': eGender,
            'hiringDate': eHDate,
            'email': eEmail,
            'phoneNumber': ePhone
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
            "X-HTTP-Method": "MERGE",
            "IF-MATCH": "*"
        },
        success: function (data) {
            console.log(data);
            window.location.reload()
            // Get the updated row data as an array
            var updatedRowData = [
                eNik,
                eFirst,
                eLast,
                eBDate,
                eGender === 1 ? 'Male' : 'Female',
                eHDate,
                eEmail,
                ePhone
                // Add other columns as needed
            ];

            // Loop through each row in the DataTable
            dataTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var rowData = this.data();

                // Check if the GUID in the row matches the guid parameter
                if (rowData[0] === guid) {
                    // Update the row data
                    this.data(updatedRowData);

                    // Redraw the updated row
                    this.invalidate();

                    // Exit the loop
                    return false;
                }
            });

            // Close the modal
            $('#employeeUpdateModal').modal('hide');
        },
        error: function (error) {
            console.log("Error: " + JSON.stringify(error));
        }
    });
}


/*(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()
*/