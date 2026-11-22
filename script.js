
const toggle = document.getElementById("theme-toggle");

// Load saved theme
if (localStorage.getItem("theme") === "dark") {
    document.body.classList.add("dark-mode");
    toggle.textContent = "☀️ Light Mode";
} else {
    toggle.textContent = "🌙 Dark Mode";
}

// Toggle theme
toggle.addEventListener("click", function() {

    document.body.classList.toggle("dark-mode");

    const isDark = document.body.classList.contains("dark-mode");

    localStorage.setItem("theme", isDark ? "dark" : "light");

    toggle.textContent = isDark ? "☀️ Light Mode" : "🌙 Dark Mode";

});
