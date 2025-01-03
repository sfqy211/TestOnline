document.addEventListener('DOMContentLoaded', function () {
    let remainingTime = examData.remainingTime;
    const questions = examData.questions;

    const timerElement = document.getElementById('exam-timer');
    const questionButtons = document.querySelectorAll('.question-btn');
    const questionArea = document.getElementById('question-area');

    function updateTimer() {
        const minutes = Math.floor(remainingTime / 60);
        const seconds = remainingTime % 60;

        timerElement.textContent = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;

        if (remainingTime <= 0) {
            clearInterval(timerInterval);
            submitExam();
        } else {
            remainingTime -= 1;
        }
    }

    const timerInterval = setInterval(updateTimer, 1000);

    questionButtons.forEach(button => {
        button.addEventListener('click', function () {
            const questionId = this.getAttribute('data-question-id');
            displayQuestion(questionId);
        });
    });

    function displayQuestion(questionId) {
        questionButtons.forEach(btn => btn.classList.remove('active'));
        document.querySelector(`.question-btn[data-question-id="${questionId}"]`).classList.add('active');

        questionArea.innerHTML = '';
        const questionElement = document.createElement('div');
        questionElement.className = 'question';
        questionElement.innerHTML = `
            <h6>问题 ${parseInt(questionId) + 1}</h6>
            <p>${questions[questionId].Text}</p>
        `;
        questionArea.appendChild(questionElement);
    }

    document.getElementById('save-exam').addEventListener('click', function () {
        saveExam();
    });

    document.getElementById('submit-exam').addEventListener('click', function () {
        submitExam();
    });

    function saveExam() {
        // Implement save logic here
        alert('Exam saved!');

    }

    function submitExam() {
        // Implement submit logic here
        alert('Exam submitted!');
        clearInterval(timerInterval);
        remainingTime = 0;
    }

    // Initial display of the first question
    if (questionButtons.length > 0) {
        displayQuestion(0);
    }
});