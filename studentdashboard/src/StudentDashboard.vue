<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-md-3 bg-light sidebar">
        <div class="student-info p-3">
          <h5>{{ student.name }}</h5>
          <p>{{ student.studentId }}</p>
        </div>
        <nav class="nav flex-column">
          <a class="nav-link" @click="showComponent('CourseManagement')">课程管理</a>
          <a class="nav-link" @click="showComponent('ExamManagement')">考试管理</a>
          <a class="nav-link" @click="showComponent('GradeManagement')">成绩管理</a>
          <a class="nav-link" @click="showComponent('PersonalInfo')">个人信息</a>
        </nav>
      </div>
      <div class="col-md-9">
        <component :is="currentComponent"></component>
      </div>
    </div>
  </div>
</template>

<script>import CourseManagement from './CourseManagement.vue';
import ExamManagement from './ExamManagement.vue';
import GradeManagement from './GradeManagement.vue';
import PersonalInfo from './PersonalInfo.vue';

export default {
  data() {
    return {
      student: {
        name: '学生姓名',
        studentId: '123456'
      },
      currentComponent: 'CourseManagement'
    };
  },
  components: {
    CourseManagement,
    ExamManagement,
    GradeManagement,
    PersonalInfo
  },
  methods: {
    showComponent(componentName) {
      this.currentComponent = componentName;
    }
  }
};</script>

<style>
  .container-fluid {
    padding: 0;
  }

  .sidebar {
    height: 100vh;
  }

  .student-info {
    border-bottom: 1px solid #ddd;
  }
</style>
