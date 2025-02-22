USE [TestOnlineDB]
GO
/****** Object:  Table [dbo].[ChoiceQuestion]    Script Date: 2025/1/7 21:43:32 ******/
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
/****** Object:  Table [dbo].[Class]    Script Date: 2025/1/7 21:43:33 ******/
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
/****** Object:  Table [dbo].[ClassCourseRelation]    Script Date: 2025/1/7 21:43:33 ******/
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
/****** Object:  Table [dbo].[CompletionQuestion]    Script Date: 2025/1/7 21:43:33 ******/
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
/****** Object:  Table [dbo].[Course]    Script Date: 2025/1/7 21:43:33 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Course__737584F69E03464A] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exam]    Script Date: 2025/1/7 21:43:33 ******/
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
/****** Object:  Table [dbo].[Faculty]    Script Date: 2025/1/7 21:43:33 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Faculty__737584F6D824EC2C] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 2025/1/7 21:43:33 ******/
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
/****** Object:  Table [dbo].[StudentExamRelation]    Script Date: 2025/1/7 21:43:33 ******/
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
/****** Object:  Table [dbo].[Teacher]    Script Date: 2025/1/7 21:43:33 ******/
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
