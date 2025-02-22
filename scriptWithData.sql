USE [TestOnlineDB]
GO
/****** Object:  Table [dbo].[ChoiceQuestion]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChoiceQuestion](
	[QuestionId] [nvarchar](50) NOT NULL,
	[ExamId] [nvarchar](50) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[OptionA] [nvarchar](max) NULL,
	[OptionB] [nvarchar](max) NULL,
	[OptionC] [nvarchar](max) NULL,
	[OptionD] [nvarchar](max) NULL,
	[Correct] [char](1) NOT NULL,
	[Score] [float] NOT NULL,
 CONSTRAINT [PK_ChoiceQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[ClassId] [nvarchar](50) NOT NULL,
	[FacultyId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Class__CB1927C05AC0DB95] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassCourseRelation]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassCourseRelation](
	[ClassId] [nvarchar](50) NOT NULL,
	[CourseId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__ClassCou__A78BF0DA06D5A411] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompletionQuestion]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompletionQuestion](
	[QuestionId] [nvarchar](50) NOT NULL,
	[ExamId] [nvarchar](50) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[Correct] [nvarchar](max) NOT NULL,
	[Score] [float] NOT NULL,
 CONSTRAINT [PK_CompletionQuestion] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseId] [nvarchar](50) NOT NULL,
	[TeacherId] [nvarchar](50) NOT NULL,
	[IsExam] [bit] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Credit] [float] NOT NULL,
	[IsCompleted] [bit] NOT NULL,
	[CreditHours] [int] NULL,
 CONSTRAINT [PK__Course__C92D71A7BB8CCA7A] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exam]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exam](
	[ExamId] [nvarchar](50) NOT NULL,
	[CourseId] [nvarchar](50) NOT NULL,
	[ExamName] [nvarchar](255) NOT NULL,
	[Time] [int] NOT NULL,
	[Proportion] [float] NOT NULL,
 CONSTRAINT [PK__Exam__297521C7A6F5FA42] PRIMARY KEY CLUSTERED 
(
	[ExamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Faculty]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faculty](
	[FacultyId] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Faculty__306F630E95575872] PRIMARY KEY CLUSTERED 
(
	[FacultyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentId] [nvarchar](50) NOT NULL,
	[ClassId] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Student__32C52B99ED63EE82] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentExamRelation]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentExamRelation](
	[StudentId] [nvarchar](50) NOT NULL,
	[ExamId] [nvarchar](50) NOT NULL,
	[StudentScore] [float] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 2025/1/7 22:14:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[TeacherId] [nvarchar](50) NOT NULL,
	[FacultyId] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK__Teacher__EDF259644678BC29] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ChoiceQuestion] ([QuestionId], [ExamId], [Text], [OptionA], [OptionB], [OptionC], [OptionD], [Correct], [Score]) VALUES (N'07e2d73d-6ade-4fc7-a4bb-5838395b8efd', N'20191060030302', N'一个无向图有 6 个顶点和 7 条边，该图的度数之和是多少？', N'7', N'14', N'28', N'42', N'A', 90)
GO
INSERT [dbo].[Class] ([ClassId], [FacultyId]) VALUES (N'20220411', N'4')
INSERT [dbo].[Class] ([ClassId], [FacultyId]) VALUES (N'20220611', N'6')
INSERT [dbo].[Class] ([ClassId], [FacultyId]) VALUES (N'20220612', N'6')
INSERT [dbo].[Class] ([ClassId], [FacultyId]) VALUES (N'20220613', N'6')
INSERT [dbo].[Class] ([ClassId], [FacultyId]) VALUES (N'20220614', N'6')
INSERT [dbo].[Class] ([ClassId], [FacultyId]) VALUES (N'20220615', N'6')
INSERT [dbo].[Class] ([ClassId], [FacultyId]) VALUES (N'20220616', N'6')
INSERT [dbo].[Class] ([ClassId], [FacultyId]) VALUES (N'20222011', N'20')
INSERT [dbo].[Class] ([ClassId], [FacultyId]) VALUES (N'20222012', N'20')
GO
INSERT [dbo].[ClassCourseRelation] ([ClassId], [CourseId]) VALUES (N'20220411', N'201910410801')
INSERT [dbo].[ClassCourseRelation] ([ClassId], [CourseId]) VALUES (N'20220616', N'201910600303')
INSERT [dbo].[ClassCourseRelation] ([ClassId], [CourseId]) VALUES (N'20220616', N'201910600304')
INSERT [dbo].[ClassCourseRelation] ([ClassId], [CourseId]) VALUES (N'20220616', N'201910600306')
INSERT [dbo].[ClassCourseRelation] ([ClassId], [CourseId]) VALUES (N'20222011', N'201912010802')
GO
INSERT [dbo].[CompletionQuestion] ([QuestionId], [ExamId], [Text], [Correct], [Score]) VALUES (N'a2d32b9b-ff52-4e06-985b-8713dd319474', N'20191060030302', N'函数f存在反函数的条件是什么？', N'f必须是一个双射函数', 10)
GO
INSERT [dbo].[Course] ([CourseId], [TeacherId], [IsExam], [Name], [Credit], [IsCompleted], [CreditHours]) VALUES (N'201910410801', N'1999041101', 1, N'自动控制原理', 4, 0, 64)
INSERT [dbo].[Course] ([CourseId], [TeacherId], [IsExam], [Name], [Credit], [IsCompleted], [CreditHours]) VALUES (N'201910600303', N'1999061101', 1, N'离散数学', 4, 0, 48)
INSERT [dbo].[Course] ([CourseId], [TeacherId], [IsExam], [Name], [Credit], [IsCompleted], [CreditHours]) VALUES (N'201910600304', N'1999061101', 1, N'操作系统', 4.5, 1, 64)
INSERT [dbo].[Course] ([CourseId], [TeacherId], [IsExam], [Name], [Credit], [IsCompleted], [CreditHours]) VALUES (N'201910600306', N'1999061101', 0, N'毕业设计', 2, 1, 98)
INSERT [dbo].[Course] ([CourseId], [TeacherId], [IsExam], [Name], [Credit], [IsCompleted], [CreditHours]) VALUES (N'201912010801', N'1999201101', 1, N'软件导论', 1.5, 1, 32)
INSERT [dbo].[Course] ([CourseId], [TeacherId], [IsExam], [Name], [Credit], [IsCompleted], [CreditHours]) VALUES (N'201912010802', N'1999201101', 1, N'软件工程', 2, 0, 48)
GO
INSERT [dbo].[Exam] ([ExamId], [CourseId], [ExamName], [Time], [Proportion]) VALUES (N'20191060030301', N'201910600303', N'离散数学期末', 150, 0.2)
INSERT [dbo].[Exam] ([ExamId], [CourseId], [ExamName], [Time], [Proportion]) VALUES (N'20191060030302', N'201910600303', N'离散数学期中（演示使用）', 1, 0.800000011920929)
INSERT [dbo].[Exam] ([ExamId], [CourseId], [ExamName], [Time], [Proportion]) VALUES (N'20191060030401', N'201910600304', N'操作系统期末', 120, 1)
GO
INSERT [dbo].[Faculty] ([FacultyId], [Name]) VALUES (N'1', N'船舶工程学院')
INSERT [dbo].[Faculty] ([FacultyId], [Name]) VALUES (N'6', N'计算机学院')
INSERT [dbo].[Faculty] ([FacultyId], [Name]) VALUES (N'20', N'软件学院')
INSERT [dbo].[Faculty] ([FacultyId], [Name]) VALUES (N'8', N'信息与通信工程学院')
INSERT [dbo].[Faculty] ([FacultyId], [Name]) VALUES (N'4', N'自动化学院')
GO
INSERT [dbo].[Student] ([StudentId], [ClassId], [Name], [Password]) VALUES (N'2022041101', N'20220411', N'李四', N'041101')
INSERT [dbo].[Student] ([StudentId], [ClassId], [Name], [Password]) VALUES (N'2022065619', N'20220616', N'测试账户', N'065619')
INSERT [dbo].[Student] ([StudentId], [ClassId], [Name], [Password]) VALUES (N'2022201101', N'20222011', N'张三', N'201101')
GO
INSERT [dbo].[StudentExamRelation] ([StudentId], [ExamId], [StudentScore]) VALUES (N'2022065619', N'20191060030302', 90)
GO
INSERT [dbo].[Teacher] ([TeacherId], [FacultyId], [Name], [Password]) VALUES (N'1999041101', N'4', N'王老师', N'041101')
INSERT [dbo].[Teacher] ([TeacherId], [FacultyId], [Name], [Password]) VALUES (N'1999061101', N'6', N'张老师', N'061101')
INSERT [dbo].[Teacher] ([TeacherId], [FacultyId], [Name], [Password]) VALUES (N'1999201101', N'20', N'李老师', N'201101')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Course__737584F69E03464A]    Script Date: 2025/1/7 22:14:06 ******/
ALTER TABLE [dbo].[Course] ADD  CONSTRAINT [UQ__Course__737584F69E03464A] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Faculty__737584F6D824EC2C]    Script Date: 2025/1/7 22:14:06 ******/
ALTER TABLE [dbo].[Faculty] ADD  CONSTRAINT [UQ__Faculty__737584F6D824EC2C] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Course] ADD  CONSTRAINT [DF__Course__IsExam__4D94879B]  DEFAULT ((0)) FOR [IsExam]
GO
ALTER TABLE [dbo].[ChoiceQuestion]  WITH CHECK ADD  CONSTRAINT [FK_ChoiceQuestion_Exam] FOREIGN KEY([ExamId])
REFERENCES [dbo].[Exam] ([ExamId])
GO
ALTER TABLE [dbo].[ChoiceQuestion] CHECK CONSTRAINT [FK_ChoiceQuestion_Exam]
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK__Class__FacultyId__440B1D61] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK__Class__FacultyId__440B1D61]
GO
ALTER TABLE [dbo].[ClassCourseRelation]  WITH CHECK ADD  CONSTRAINT [FK__ClassCour__Class__5812160E] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([ClassId])
GO
ALTER TABLE [dbo].[ClassCourseRelation] CHECK CONSTRAINT [FK__ClassCour__Class__5812160E]
GO
ALTER TABLE [dbo].[ClassCourseRelation]  WITH CHECK ADD  CONSTRAINT [FK__ClassCour__Cours__59063A47] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[ClassCourseRelation] CHECK CONSTRAINT [FK__ClassCour__Cours__59063A47]
GO
ALTER TABLE [dbo].[CompletionQuestion]  WITH CHECK ADD  CONSTRAINT [FK_CompletionQuestion_Exam] FOREIGN KEY([ExamId])
REFERENCES [dbo].[Exam] ([ExamId])
GO
ALTER TABLE [dbo].[CompletionQuestion] CHECK CONSTRAINT [FK_CompletionQuestion_Exam]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Teacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[Teacher] ([TeacherId])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Teacher]
GO
ALTER TABLE [dbo].[Exam]  WITH CHECK ADD  CONSTRAINT [FK__Exam__CourseId__5165187F] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([CourseId])
GO
ALTER TABLE [dbo].[Exam] CHECK CONSTRAINT [FK__Exam__CourseId__5165187F]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK__Student__ClassId__49C3F6B7] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([ClassId])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK__Student__ClassId__49C3F6B7]
GO
ALTER TABLE [dbo].[StudentExamRelation]  WITH CHECK ADD  CONSTRAINT [FK_StudentAnswer_Student] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([StudentId])
GO
ALTER TABLE [dbo].[StudentExamRelation] CHECK CONSTRAINT [FK_StudentAnswer_Student]
GO
ALTER TABLE [dbo].[StudentExamRelation]  WITH CHECK ADD  CONSTRAINT [FK_StudentQuestion_Exam] FOREIGN KEY([ExamId])
REFERENCES [dbo].[Exam] ([ExamId])
GO
ALTER TABLE [dbo].[StudentExamRelation] CHECK CONSTRAINT [FK_StudentQuestion_Exam]
GO
ALTER TABLE [dbo].[Teacher]  WITH CHECK ADD  CONSTRAINT [FK__Teacher__Faculty__46E78A0C] FOREIGN KEY([FacultyId])
REFERENCES [dbo].[Faculty] ([FacultyId])
GO
ALTER TABLE [dbo].[Teacher] CHECK CONSTRAINT [FK__Teacher__Faculty__46E78A0C]
GO
