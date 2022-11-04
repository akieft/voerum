$(document).ready(function () {
    // Create two variable with the names of the months and days in an array
    var monthNames = ["Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "October", "November", "December"];
    var dayNames = ["Zondag", "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag"]

    // Create a newDate() object
    var newDate = new Date();
    // Extract the current date from Date object
    newDate.setDate(newDate.getDate());
    // Output the day, date, month and year
    $('#Date').html(dayNames[newDate.getDay()] + " " + newDate.getDate() + ' ' + monthNames[newDate.getMonth()] + ' ' + newDate.getFullYear());

    setInterval(function () {
        // Create a newDate() object and extract the seconds of the current time on the visitor's
        var seconds = new Date().getSeconds();
        // Add a leading zero to seconds value
        $("#sec").html((seconds < 10 ? "0" : "") + seconds);
    }, 1000);

    setInterval(function () {
        // Create a newDate() object and extract the minutes of the current time on the visitor's
        var minutes = new Date().getMinutes();
        // Add a leading zero to the minutes value
        $("#min").html((minutes < 10 ? "0" : "") + minutes);
    }, 1000);

    setInterval(function () {
        // Create a newDate() object and extract the hours of the current time on the visitor's
        var hours = new Date().getHours();
        // Add a leading zero to the hours value
        $("#hours").html((hours < 10 ? "0" : "") + hours);
    }, 1000);
});

initializePage = (obj) => {
    obj.querySelectorAll(".search-field").forEach((obj) => {
        obj.addEventListener("keyup", (event) => {
            if (event.keyCode == 13) {
                event.preventDefault();
                let results = $("#search").serializeArray();
                console.info("Detected enter, running search:");
                console.info(results);

                console.log('search = ' + results[0].value);

                let response = $.ajax({
                    xhrFields: { withCredentials: true },
                    type: "GET",
                    url: "/Recipe/List?search=" + results[0].value
                }).done(() => $("#search-results").html(response.responseText));
            }
        });
    });

    obj.querySelectorAll(".repeat-list").forEach((obj) => {
        let list = obj;
        let element = obj.querySelector(".repeat-element");
        list.querySelectorAll(".repeat-list-add-button").forEach((obj) => {
            obj.addEventListener("click", (event) => {
                let newElement = list.appendChild(element.cloneNode(true));
                $(newElement).slideDown();
            });
        });
    });

    removeMeFromList = (obj) => {
        let ele = obj.closest(".repeat-element");
        $(ele).slideUp(400, () => {
            ele.remove();
        })
    }

    submitNewRecipe = () => {
        let data = $("#new-recipe-form").serializeArray();
        let recipe = { Ingredients: [], Steps: [] };
        let step = 1;
        for (obj in data) {
            item = data[obj];
            key = item["name"];
            value = item["value"];
            switch (key) {
                case "name":
                    recipe.Name = value;
                    break;
                case "ingredient":
                    if (value != "") recipe.Ingredients.push({ Text: value });
                    break;
                case "step":
                    if (value != "") {
                        recipe.Steps.push({ Text: value, Order: step });
                        step++;
                    }
                    break;
            }
        }
        console.info(recipe);
        let response = $.ajax({
            xhrFields: { withCredentials: true },
            type: "POST",
            url: "/api/Recipes",
            data: recipe
        }).done(function () {
            alert("Recipe saved successfully!");
            $('#new-recipe-foldout').slideToggle();
            location.reload();
        });
    }

    obj.querySelectorAll(".section-close-button").forEach((obj) => {
        obj.addEventListener("click", (event) => {
            $(obj.closest(".section")).slideToggle("section-hidden");
        });
    });

    obj.querySelectorAll(".section").forEach((obj) => {
        obj.querySelectorAll("a").forEach((link) => {
            link.addEventListener("click", (event) => {
                event.preventDefault();
                let page = $.get(link.getAttribute("href")).done(() => {
                    obj.innerHTML = page.responseText;
                    initializePage(obj);
                });
            });
        });
    });

    obj.querySelector(".logout-button").addEventListener("click", (event) => {
        location.replace("/Account/LogOff");
    });
}

console.info("Initializing voerum.");
initializePage(document);