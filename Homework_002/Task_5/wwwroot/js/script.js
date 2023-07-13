$(() => {
    async function getCountries() {
        const response = await fetch("/api/data", {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

        if (response.ok) {
            const countries = await response.json();
            renderCountries(countries);
        }
    }

    let renderCountries = (countries) => {
        let $mainContainer = $("#mainContainer");
        let order = false;

        $mainContainer.append($("<hr>").addClass("featurette-divider"));

        countries.forEach((country) => {
            let $row = $("<div>").addClass("row featurette");

            let $colText = $("<div>").addClass("col-md-7");

            if (!order)
                $colText.addClass("order-md-2");
            else
                $colText.removeClass("order-md-2");
            
            $colText.append($("<h2>").addClass("featurette-heading fw-normal lh-1").text(country.name));
            $colText.append($("<p>").addClass("lead").text(country.description));

            let $colImg = $("<div>").addClass("col-md-5 img-container");
            let $img = $("<img>").addClass("img-fluid w-100");
            $img.attr("src", "data:image/png;base64," + country.imageBase64);
            $colImg.append($img);

            $row.append($colText);
            $row.append($colImg);

            $mainContainer.append($row);

            $mainContainer.append($("<hr>").addClass("featurette-divider"));

            order = !order;
        });
    }

    getCountries();
});