const apiUrl = "http://localhost:5043/api/Tasks";

const taskList = document.getElementById("taskList");
const taskForm = document.getElementById("taskForm");
const message = document.getElementById("message");

async function loadTasks() {
    try {
        const response = await fetch(apiUrl);

        if (!response.ok) {
            throw new Error("Failed to load tasks");
        }

        const tasks = await response.json();
        renderTasks(tasks);
    } catch (error) {
        console.error("GET error:", error);
        showMessage("Failed to load tasks", true);
    }
}

function renderTasks(tasks) {
    taskList.innerHTML = "";

    if (!tasks || tasks.length === 0) {
        taskList.innerHTML = "<p>No tasks yet.</p>";
        return;
    }

    tasks.forEach(task => {
        const div = document.createElement("div");
        div.classList.add("task");

        if (task.isDone) {
            div.classList.add("done");
        }

        div.innerHTML = `
            <h3>${task.title}</h3>
            <p>${task.description}</p>
            <p>Status: ${task.isDone ? "Done" : "Not done"}</p>
            <button class="toggle-btn">Mark as ${task.isDone ? "not done" : "done"}</button>
            <button class="delete-btn">Delete</button>
        `;

        const toggleBtn = div.querySelector(".toggle-btn");
        const deleteBtn = div.querySelector(".delete-btn");

        toggleBtn.addEventListener("click", () => {
            toggleTask(task.id, task.title, task.description, task.isDone);
        });

        deleteBtn.addEventListener("click", () => {
            deleteTask(task.id);
        });

        taskList.appendChild(div);
    });
}

taskForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const title = document.getElementById("title").value.trim();
    const description = document.getElementById("description").value.trim();

    if (!title || !description) {
        showMessage("Please fill in all fields", true);
        return;
    }

    const newTask = {
        title: title,
        description: description
    };

    try {
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newTask)
        });

        if (!response.ok) {
            throw new Error("Failed to create task");
        }

        taskForm.reset();
        showMessage("Task created", false);
        loadTasks();
    } catch (error) {
        console.error("POST error:", error);
        showMessage("Failed to create task", true);
    }
});

async function toggleTask(id, title, description, isDone) {
    const updatedTask = {
        title: title,
        description: description,
        isDone: !isDone
    };

    try {
        const response = await fetch(`${apiUrl}/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(updatedTask)
        });

        if (!response.ok) {
            throw new Error("Failed to update task");
        }

        showMessage("Task updated", false);
        loadTasks();
    } catch (error) {
        console.error("PUT error:", error);
        showMessage("Failed to update task", true);
    }
}

async function deleteTask(id) {
    try {
        const response = await fetch(`${apiUrl}/${id}`, {
            method: "DELETE"
        });

        if (!response.ok) {
            throw new Error("Failed to delete task");
        }

        showMessage("Task deleted", false);
        loadTasks();
    } catch (error) {
        console.error("DELETE error:", error);
        showMessage("Failed to delete task", true);
    }
}

function showMessage(text, isError = false) {
    message.textContent = text;
    message.style.color = isError ? "red" : "green";
}

loadTasks();