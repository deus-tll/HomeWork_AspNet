(() => {
    const formName = "person_form";
    const tbody = document.querySelector("tbody");
    const errorsElement = document.getElementById("errors");

    const getPeople = async () => {
        const response = await fetch("api/people", {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

        if (response.ok === true) {
            const people = await response.json();
            people.forEach(person => {
                tbody.append(row(person));
            });
        }
    }


    const getPerson = async (id) => {
        const response = await fetch("api/people/" + id, {
            method: "GET",
            headers: { "Accept": "application/json" }
        });

        if (response.ok === true) {
            const person = await response.json();
            const form = document.forms[formName];
            form.elements["id"].value = person.id;
            form.elements["name"].value = person.name;
            form.elements["age"].value = person.age;
        }
    }


    const createPerson = async (personName, personAge) => {

        const response = await fetch("api/people", {
            method: "POST",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                name: personName,
                age: parseInt(personAge, 10)
            })
        });

        if (response.ok === true) {
            const person = await response.json();
            reset();
            tbody.append(row(person));
        }
        else {
            const errorData = await response.json();
            console.log("errors", errorData);
            if (errorData) {
                if (errorData.errors) {
                    if (errorData.errors["Name"]) {
                        showError(errorData.errors["Name"]);
                    }

                    if (errorData.errors["Age"]) {
                        showError(errorData.errors["Age"]);
                    }
                }

                if (errorData["Name"]) {
                    showError(errorData["Name"]);
                }

                if (errorData["Age"]) {
                    showError(errorData["Age"]);
                }
            }

            errorsElement.style.display = "block";
        }
    }


    const editPerson = async (personId, personName, personAge) => {
        const response = await fetch("api/people", {
            method: "PUT",
            headers: { "Accept": "application/json", "Content-Type": "application/json" },
            body: JSON.stringify({
                id: parseInt(personId, 10),
                name: personName,
                age: parseInt(personAge, 10)
            })
        });
        if (response.ok === true) {
            const person = await response.json();
            reset();
            document.querySelector("tr[data-rowid='" + person.id + "']").replaceWith(row(person));
        }
    }


    const deletePerson = async (id) => {
        const response = await fetch("api/people/" + id, {
            method: "DELETE",
            headers: { "Accept": "application/json" }
        });
        if (response.ok === true) {
            const person = await response.json();
            document.querySelector("tr[data-rowid='" + person.id + "']").remove();
        }
    }


    const reset = () => {
        const form = document.forms[formName];
        form.reset();
        form.elements["id"].value = 0;
    };


    const showError = (errors) => {
        errors.forEach((error) => {
            const p = document.createElement("p");
            p.textContent = error;
            errorsElement.appendChild(p);
        });
    };


    const row = (person) => {

        const tr = document.createElement("tr");
        tr.setAttribute("data-rowid", person.id);

        const idTd = document.createElement("td");
        idTd.append(person.id);
        tr.append(idTd);

        const nameTd = document.createElement("td");
        nameTd.append(person.name);
        tr.append(nameTd);

        const ageTd = document.createElement("td");
        ageTd.append(person.age);
        tr.append(ageTd);

        const linksTd = document.createElement("td");

        const editLink = document.createElement("a");
        editLink.setAttribute("data-id", person.id);
        editLink.setAttribute("style", "cursor:pointer;padding:15px;");
        editLink.append("Edit");
        editLink.addEventListener("click", (e) => {

            e.preventDefault();
            getPerson(person.id);
        });
        linksTd.append(editLink);

        const removeLink = document.createElement("a");
        removeLink.setAttribute("data-id", person.id);
        removeLink.setAttribute("style", "cursor:pointer;padding:15px;");
        removeLink.append("Delete");
        removeLink.addEventListener("click", (e) => {
            e.preventDefault();
            deletePerson(person.id);
        });

        linksTd.append(removeLink);
        tr.appendChild(linksTd);

        return tr;
    }


    document.getElementById("reset").addEventListener("click", (e) => {
        e.preventDefault();
        reset();
    });


    document.forms[formName].addEventListener("submit", async (e) => {
        e.preventDefault();
        errorsElement.innerHTML = "";
        errorsElement.style.display = "none";

        const form = document.forms[formName];
        const id = form.elements["id"].value;
        const name = form.elements["name"].value;
        const age = form.elements["age"].value;

        if (id == 0) {
            createPerson(name, age);
        } else {
            editPerson(id, name, age);
        }
    });


    getPeople();
})();