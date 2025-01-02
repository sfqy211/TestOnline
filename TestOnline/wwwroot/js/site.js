function loadContent(event, viewName) {
    fetch('/Admin/LoadView?viewName=' + viewName)
        .then(response => response.text())
        .then(html => {
            document.getElementById('content').innerHTML = html;
        });
}

function searchCourses() {
    var courseName = document.getElementById('courseSearchInput').value;
    fetch('/Admin/SearchCourses?courseName=' + encodeURIComponent(courseName))
        .then(response => response.text())
        .then(html => {
            document.getElementById('searchResultsCourse').innerHTML = html;
        })
        .catch(error => {
            console.error('course search error:', error);
        });
}


function searchStudents() {
    var studentName = document.getElementById('studentSearchInput').value;
    fetch('/Admin/SearchStudents?studentName=' + encodeURIComponent(studentName))
        .then(response => response.text())
        .then(html => {
            document.getElementById('searchResultsStudent').innerHTML = html;
        })
        .catch(error => {
            console.error('student search error:', error);
        });
}

function searchTeachers() {
    var teacherName = document.getElementById('teacherSearchInput').value;
    fetch('/Admin/SearchTeachers?teacherName=' + encodeURIComponent(teacherName))
        .then(response => response.text())
        .then(html => {
            document.getElementById('searchResultsTeacher').innerHTML = html;
        })
        .catch(error => {
            console.error('teacher search error:', error);
        });
}