$(() => {
  async function getUsers() {
      const response = await fetch("/api/data", {
          method: "GET",
          headers: { "Accept": "application/json" }
      });

      if (response.ok) {
          const users = await response.json();
          const $rows = $("tbody");
          users.forEach(user => $rows.append(makeRow(user)));
          console.log(users);
      }
  }

    async function getUser(id) {
        console.log(id);
      const response = await fetch("/api/data/" + id, {
          method: "GET",
          headers: { "Accept": "application/json" }
      });

      if (response.ok) {
          const user = await response.json();
          const $form = $("form[name='userForm']");
          $form.find("input[name='id']").val(user.id);
          $form.find("input[name='name']").val(user.name);
          $form.find("input[name='age']").val(user.age);
      }
      else {
          error(response);
      }
  }

  async function createUser(userName, userAge) {
      const response = await fetch("/api/data", {
          method: "POST",
          headers: { "Accept": "application/json", "Content-Type": "application/json" },
          body: JSON.stringify({
              name: userName,
              age: parseInt(userAge, 10)
          })
      });

      if (response.ok) {
          const user = await response.json();
          reset();
          $("tbody").append(makeRow(user));
      }
      else {
          error(response);
      }
  }

  async function editUser(userId, userName, userAge) {
      const response = await fetch("/api/data", {
          method: "PUT",
          headers: { "Accept": "application/json", "Content-Type": "application/json" },
          body: JSON.stringify({
              id: userId,
              name: userName,
              age: parseInt(userAge, 10)
          })
      });

      if (response.ok) {
          const user = await response.json();
          reset();
          $("tr[data-rowid=" + user.id + "]").replaceWith(makeRow(user));
      }
      else {
          error(response);
      }
  }

  async function deleteUser(id) {
      const response = await fetch("/api/data/" + id, {
          method: "DELETE",
          headers: { "Accept": "application/json" }
      });

      if (response.ok) {
          const user = await response.json();
          $("tr[data-rowid=" + user.id + "]").remove();
      }
      else {
          error(response);
      }
  }

  function reset() {
      var form = $("form[name='userForm']");
      form[0].reset();
      form.find("input[name='id']").val(0);
  }

   async function error(response) {
        const error = await response.json();
        console.log(error.message);
    }

  function makeRow(user) {
      const $tr = $("<tr>").attr("data-rowid", user.id);
      $tr.append($("<td>").append(user.name));
      $tr.append($("<td>").append(user.age));

      const $linksTd = $("<td>");
      const $editLink = $("<a>").attr("style", "cursor:pointer;padding:15px;").append("Change").on("click", (e) => {
          e.preventDefault();
          getUser(user.id);
      });

      const $removeLink = $("<a>").attr("style", "cursor:pointer;padding:15px;").append("Delete").on("click", (e) => {
          e.preventDefault();
          deleteUser(user.id);
      });

      $linksTd.append($editLink).append($removeLink);
      $tr.append($linksTd);

      return $tr;
  }

  $("#reset").on("click", (e) => {
      e.preventDefault();
      reset();
  });

    const $form = $("form[name='userForm']");

    $form.on("submit", (e) => {
        e.preventDefault();
        const id = $form.find("input[name='id']").val();
        const name = $form.find("input[name='name']").val();
        const age = $form.find("input[name='age']").val();

        if (id == 0)
            createUser(name, age);
        else
            editUser(id, name, age);
    });

  getUsers();
});