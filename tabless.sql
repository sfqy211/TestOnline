CREATE TABLE Faculty (
    FacultyId INT NOT NULL,
    Name NVARCHAR(255) NOT NULL UNIQUE,
    PRIMARY KEY(FacultyId)
);

CREATE TABLE Class (
    ClassId INT NOT NULL,
    FacultyId INT NOT NULL,
    PRIMARY KEY(ClassId),
    FOREIGN KEY (FacultyId) REFERENCES Faculty(FacultyId)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE Teacher (
    TeacherId INT NOT NULL,
    FacultyId INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    PRIMARY KEY(TeacherId),
    FOREIGN KEY (FacultyId) REFERENCES Faculty(FacultyId)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE Student (
    StudentId INT NOT NULL,
    ClassId INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    PRIMARY KEY(StudentId),
    FOREIGN KEY (ClassId) REFERENCES Class(ClassId)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE INDEX Student_index_0
ON Student (ClassId);

CREATE TABLE Course (
    CourseId INT NOT NULL,
    FacultyId INT NOT NULL,
    IsExam BIT NOT NULL DEFAULT 0,
    Name NVARCHAR(255) NOT NULL UNIQUE,
    Credit FLOAT NOT NULL,
    PRIMARY KEY(CourseId),
    FOREIGN KEY (FacultyId) REFERENCES Faculty(FacultyId)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE TABLE Exam (
    ExamId INT NOT NULL,
    CourseId INT NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Data NVARCHAR(MAX) NOT NULL,
    RightAnswer NVARCHAR(MAX),
    StudentAnswer NVARCHAR(MAX),
    Description NVARCHAR(255),
    StartTime DATETIME,
    EndTime DATETIME,
    Score FLOAT NOT NULL,
    PRIMARY KEY(ExamId),
    FOREIGN KEY (CourseId) REFERENCES Course(CourseId)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE INDEX Exam_index_0
ON Exam (CourseId);

CREATE TABLE TeacherCourseRelation (
    TeacherId INT NOT NULL,
    CourseId INT NOT NULL,
    PRIMARY KEY(TeacherId, CourseId),
    FOREIGN KEY (TeacherId) REFERENCES Teacher(TeacherId)
    ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (CourseId) REFERENCES Course(CourseId)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE INDEX TeacherCourseRelation_index_0
ON TeacherCourseRelation (TeacherId);

CREATE INDEX TeacherCourseRelation_index_1
ON TeacherCourseRelation (CourseId);

CREATE TABLE ClassCourseRelation (
    ClassId INT NOT NULL,
    CourseId INT NOT NULL,
    PRIMARY KEY(ClassId, CourseId),
    FOREIGN KEY (ClassId) REFERENCES Class(ClassId)
    ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY (CourseId) REFERENCES Course(CourseId)
    ON UPDATE NO ACTION ON DELETE NO ACTION
);

CREATE INDEX ClassCourseRelation_index_0
ON ClassCourseRelation (ClassId);

CREATE INDEX ClassCourseRelation_index_1
ON ClassCourseRelation (CourseId);