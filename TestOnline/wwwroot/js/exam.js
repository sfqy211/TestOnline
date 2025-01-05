document.addEventListener('DOMContentLoaded', function () {
    let remainingTime = examData.remainingTime;
    const choiceQuestions = examData.choiceQuestions;
    const completionQuestions = examData.completionQuestions;

    const timerElement = document.getElementById('exam-timer');
    const questionButtons = document.querySelectorAll('.question-btn');
    const questionArea = document.getElementById('question-area');

    // 用于保存用户的选择
    const userAnswers = {
        choice: {},
        completion: {}
    };

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
            const questionType = this.getAttribute('data-question-type');
            displayQuestion(questionId, questionType);
        });
    });

    function displayQuestion(questionId, questionType) {
        questionButtons.forEach(btn => btn.classList.remove('active'));
        document.querySelector(`.question-btn[data-question-id="${questionId}"][data-question-type="${questionType}"]`).classList.add('active');

        questionArea.innerHTML = '';
        const questionElement = document.createElement('div');
        questionElement.className = 'question';

        if (questionType === 'choice') {
            const question = choiceQuestions.find(q => q.QuestionId === questionId);
            if (question) {
                const questionNumber = choiceQuestions.indexOf(question) + 1;
                let optionsHtml = '';

                if (question.OptionA) {
                    optionsHtml += `
                        <input type="radio" id="optionA-${questionId}" name="question-${questionId}" value="A" ${userAnswers.choice[questionId] === 'A' ? 'checked' : ''}>
                        <label for="optionA-${questionId}">A. ${question.OptionA}</label><br>
                    `;
                }

                if (question.OptionB) {
                    optionsHtml += `
                        <input type="radio" id="optionB-${questionId}" name="question-${questionId}" value="B" ${userAnswers.choice[questionId] === 'B' ? 'checked' : ''}>
                        <label for="optionB-${questionId}">B. ${question.OptionB}</label><br>
                    `;
                }

                if (question.OptionC) {
                    optionsHtml += `
                        <input type="radio" id="optionC-${questionId}" name="question-${questionId}" value="C" ${userAnswers.choice[questionId] === 'C' ? 'checked' : ''}>
                        <label for="optionC-${questionId}">C. ${question.OptionC}</label><br>
                    `;
                }

                if (question.OptionD) {
                    optionsHtml += `
                        <input type="radio" id="optionD-${questionId}" name="question-${questionId}" value="D" ${userAnswers.choice[questionId] === 'D' ? 'checked' : ''}>
                        <label for="optionD-${questionId}">D. ${question.OptionD}</label><br>
                    `;
                }

                questionElement.innerHTML = `
                    <h6>问题 ${questionNumber}</h6>
                    <p>${question.Text}</p>
                    <div>
                        ${optionsHtml}
                    </div>
                `;
            } else {
                questionElement.innerHTML = '<p>题目不存在</p>';
            }
        } else if (questionType === 'completion') {
            const question = completionQuestions.find(q => q.QuestionId === questionId);
            if (question) {
                const questionNumber = completionQuestions.indexOf(question) + 1;
                questionElement.innerHTML = `
                    <h6>问题 ${questionNumber}</h6>
                    <p>${question.Text}</p>
                    <input type="text" id="completion-${questionId}" name="completion-${questionId}" placeholder="请输入答案" value="${userAnswers.completion[questionId] || ''}">
                `;
            } else {
                questionElement.innerHTML = '<p>题目不存在</p>';
            }
        }

        questionArea.appendChild(questionElement);

        // 监听选择题的变化
        document.querySelectorAll(`input[name="question-${questionId}"]`).forEach(input => {
            input.addEventListener('change', function () {
                userAnswers.choice[questionId] = this.value;
            });
        });

        // 监听填空题的变化
        const completionInput = document.getElementById(`completion-${questionId}`);
        if (completionInput) {
            completionInput.addEventListener('input', function () {
                userAnswers.completion[questionId] = this.value;
            });
        }
    }

    document.getElementById('save-exam').addEventListener('click', function () {
        saveExam();
    });

    document.getElementById('submit-exam').addEventListener('click', function () {
        submitExam();
    });

    function saveExam() {
        // Implement save logic here
        alert('保存试卷');
    }

    function submitExam() {
        const form = document.createElement('form');
        form.method = 'POST';
        form.action = '/Exam/SubmitExam';

        // 添加考试ID和学生ID
        const examIdInput = document.createElement('input');
        examIdInput.type = 'hidden';
        examIdInput.name = 'ExamId';
        examIdInput.value = examData.ExamId;
        form.appendChild(examIdInput);

        const studentIdInput = document.createElement('input');
        studentIdInput.type = 'hidden';
        studentIdInput.name = 'StudentId';
        studentIdInput.value = examData.StudentId;
        form.appendChild(studentIdInput);

        // 收集选择题答案
        for (const [questionId, selectedOption] of Object.entries(userAnswers.choice)) {
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = `ChoiceAnswers[${questionId}]`;
            input.value = selectedOption;
            form.appendChild(input);
        }

        // 收集填空题答案
        for (const [questionId, answerText] of Object.entries(userAnswers.completion)) {
            const input = document.createElement('input');
            input.type = 'hidden';
            input.name = `CompletionAnswers[${questionId}]`;
            input.value = answerText;
            form.appendChild(input);
        }

        document.body.appendChild(form);
        form.submit();
    }

    // Initial display of the first question
    if (questionButtons.length > 0) {
        questionButtons[0].click();
    }
});

