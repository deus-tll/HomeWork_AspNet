$(() => {
    async function getRestaurant() {
        const response = await fetch("/api/get_restaurant", {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

        if (response.ok) {
            const restaurant = await response.json();

            $("#restaurantName").text(restaurant.name);
            $("#restaurantRating").text(restaurant.rating);
            $("#restaurantAddress").text(restaurant.address);
            $("#restaurantDescription").text(restaurant.description);
        }
        else {
            const error = await response.json();
            console.log(error.message);
        }
    }

    getRestaurant();
});