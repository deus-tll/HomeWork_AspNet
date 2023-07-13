$(() => {
  async function getRestaurants() {
    const response = await fetch("/api/restaurants", {
      method: "GET",
      headers: { "Accept": "application/json"}
    });

    if(response.ok){
      const restaurants = await response.json();
      renderListOfRestaurants(restaurants);
    }
  }

  let renderListOfRestaurants = (restaurants) => {
      let $list = $(".list-group");

      restaurants.forEach((rest) => {
          let $rest = $("<a>").attr("href", "/api/restaurant?id=" + rest.id).text(rest.name);
          $rest.addClass("list-group-item list-group-item-action px-3 border-0");
          $list.append($rest);
      });
  }

    getRestaurants();
});