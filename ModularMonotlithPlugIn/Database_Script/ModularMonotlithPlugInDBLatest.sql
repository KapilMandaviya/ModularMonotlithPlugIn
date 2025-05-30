USE [ModularMonolithPlugin]
GO
/****** Object:  Table [dbo].[City]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseMaster]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](150) NULL,
 CONSTRAINT [PK_CourseMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormFields]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormFields](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Form_Id] [int] NOT NULL,
	[FieldName] [varchar](250) NOT NULL,
	[Label] [nvarchar](255) NOT NULL,
	[DataType] [nvarchar](255) NOT NULL,
	[FieldType] [nvarchar](255) NOT NULL,
	[LengthValue] [int] NULL,
	[DefaultValue] [nvarchar](max) NULL,
	[Required] [bit] NOT NULL,
	[Duplicate] [bit] NULL,
	[Tooltip] [nvarchar](255) NULL,
	[CssClass] [nvarchar](255) NULL,
	[Position] [int] NULL,
	[OptionTableName] [nvarchar](100) NULL,
	[OptionValueField] [nvarchar](100) NULL,
	[OptionTextField] [nvarchar](100) NULL,
	[OptionsJson] [nvarchar](max) NULL,
 CONSTRAINT [PK_FormFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Forms]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Forms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FormName] [nvarchar](255) NULL,
	[MenuId] [int] NULL,
	[SubMenuId] [int] NULL,
	[TableName] [nvarchar](255) NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedId] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedId] [int] NULL,
 CONSTRAINT [PK_Forms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormUserControl]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormUserControl](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserControl] [varchar](250) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_FormUserControl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menus]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[SortOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modules]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[DllPath] [nvarchar](255) NULL,
	[IsEnabled] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SqlDatatypeList]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SqlDatatypeList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Datatype] [varchar](250) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_SqlDatatypeList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[States]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubMenus]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubMenus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[SortOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 5/29/2025 5:10:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NULL,
 CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[City] ON 

INSERT [dbo].[City] ([Id], [Name]) VALUES (1, N'Jamnagar')
INSERT [dbo].[City] ([Id], [Name]) VALUES (2, N'Rajkot')
INSERT [dbo].[City] ([Id], [Name]) VALUES (3, N'Surat')
SET IDENTITY_INSERT [dbo].[City] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseMaster] ON 

INSERT [dbo].[CourseMaster] ([Id], [Title]) VALUES (1, N'MCA')
INSERT [dbo].[CourseMaster] ([Id], [Title]) VALUES (2, N'BCA')
INSERT [dbo].[CourseMaster] ([Id], [Title]) VALUES (3, N'B.Tech')
INSERT [dbo].[CourseMaster] ([Id], [Title]) VALUES (4, N'M.Tech')
INSERT [dbo].[CourseMaster] ([Id], [Title]) VALUES (5, N'Computer Engineer')
SET IDENTITY_INSERT [dbo].[CourseMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[FormFields] ON 

INSERT [dbo].[FormFields] ([Id], [Form_Id], [FieldName], [Label], [DataType], [FieldType], [LengthValue], [DefaultValue], [Required], [Duplicate], [Tooltip], [CssClass], [Position], [OptionTableName], [OptionValueField], [OptionTextField], [OptionsJson]) VALUES (3031, 6016, N'Discipline', N'Discipline Name', N'VARCHAR', N'TextBox', 100, N'', 1, 0, N'', N'', 1, N'', N'', N'', N'')
INSERT [dbo].[FormFields] ([Id], [Form_Id], [FieldName], [Label], [DataType], [FieldType], [LengthValue], [DefaultValue], [Required], [Duplicate], [Tooltip], [CssClass], [Position], [OptionTableName], [OptionValueField], [OptionTextField], [OptionsJson]) VALUES (3032, 6017, N'College_Code', N'College Code', N'VARCHAR', N'TextBox', 150, N'', 0, 0, N'', N'', 1, N'', N'', N'', N'')
INSERT [dbo].[FormFields] ([Id], [Form_Id], [FieldName], [Label], [DataType], [FieldType], [LengthValue], [DefaultValue], [Required], [Duplicate], [Tooltip], [CssClass], [Position], [OptionTableName], [OptionValueField], [OptionTextField], [OptionsJson]) VALUES (3033, 6017, N'CollegeName', N'College_Name', N'VARCHAR', N'TextBox', 150, N'', 0, 0, N'', N'', 2, N'', N'', N'', N'')
SET IDENTITY_INSERT [dbo].[FormFields] OFF
GO
SET IDENTITY_INSERT [dbo].[Forms] ON 

INSERT [dbo].[Forms] ([Id], [FormName], [MenuId], [SubMenuId], [TableName], [CreatedAt], [CreatedId], [UpdatedAt], [UpdatedId]) VALUES (6016, N'Discipline Master', 2016, 2016, N'Discipline', CAST(N'2025-05-29T16:50:57.103' AS DateTime), 0, CAST(N'2025-05-29T16:51:42.630' AS DateTime), 0)
INSERT [dbo].[Forms] ([Id], [FormName], [MenuId], [SubMenuId], [TableName], [CreatedAt], [CreatedId], [UpdatedAt], [UpdatedId]) VALUES (6017, N'College Master', 2017, 2017, N'College_master', CAST(N'2025-05-29T16:53:12.330' AS DateTime), 0, NULL, 0)
SET IDENTITY_INSERT [dbo].[Forms] OFF
GO
SET IDENTITY_INSERT [dbo].[FormUserControl] ON 

INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (1, N'TextBox', 1)
INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (2, N'DropDown', 1)
INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (3, N'CheckBox', 1)
INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (4, N'RadioButton', 1)
INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (5, N'TextArea', 1)
INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (6, N'DatePicker', 1)
INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (7, N'FileUpload', 1)
INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (8, N'Password', 1)
INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (9, N'Email', 1)
INSERT [dbo].[FormUserControl] ([Id], [UserControl], [IsActive]) VALUES (10, N'Number', 1)
SET IDENTITY_INSERT [dbo].[FormUserControl] OFF
GO
SET IDENTITY_INSERT [dbo].[Menus] ON 

INSERT [dbo].[Menus] ([Id], [Name], [SortOrder]) VALUES (2016, N'Discipline', NULL)
INSERT [dbo].[Menus] ([Id], [Name], [SortOrder]) VALUES (2017, N'College', NULL)
SET IDENTITY_INSERT [dbo].[Menus] OFF
GO
SET IDENTITY_INSERT [dbo].[Modules] ON 

INSERT [dbo].[Modules] ([Id], [Name], [DllPath], [IsEnabled]) VALUES (1, N'UserModule', N'Modules/UserModule.dll', 1)
INSERT [dbo].[Modules] ([Id], [Name], [DllPath], [IsEnabled]) VALUES (2, N'CourseModule', N'Modules/CourseModule.dll', 1)
INSERT [dbo].[Modules] ([Id], [Name], [DllPath], [IsEnabled]) VALUES (2002, N'DynamicFormBuilder', N'Modules/DynamicFormBuilder.dll', 1)
SET IDENTITY_INSERT [dbo].[Modules] OFF
GO
SET IDENTITY_INSERT [dbo].[SqlDatatypeList] ON 

INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (1, N'INT', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (2, N'BIGINT', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (3, N'SMALLINT', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (4, N'TINYINT', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (5, N'BIT', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (6, N'DECIMAL', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (7, N'NUMERIC', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (8, N'FLOAT', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (9, N'REAL', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (10, N'MONEY', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (11, N'SMALLMONEY', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (12, N'CHAR', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (13, N'VARCHAR', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (14, N'NCHAR', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (15, N'NVARCHAR', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (16, N'TEXT', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (17, N'NTEXT', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (18, N'VARCHAR', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (19, N'NVARCHAR', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (20, N'DATE', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (21, N'TIME', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (22, N'DATETIME', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (23, N'DATETIME2', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (24, N'SMALLDATETIME', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (25, N'DATETIMEOFFSET', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (26, N'BINARY', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (27, N'VARBINARY', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (28, N'VARBINARY', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (29, N'UNIQUEIDENTIFIER', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (30, N'XML', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (31, N'SQL_VARIANT', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (32, N'IMAGE', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (33, N'GEOGRAPHY', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (34, N'GEOMETRY', 1)
INSERT [dbo].[SqlDatatypeList] ([Id], [Datatype], [IsActive]) VALUES (35, N'HIERARCHYID', 1)
SET IDENTITY_INSERT [dbo].[SqlDatatypeList] OFF
GO
SET IDENTITY_INSERT [dbo].[States] ON 

INSERT [dbo].[States] ([Id], [Name]) VALUES (1, N'Gujarat')
INSERT [dbo].[States] ([Id], [Name]) VALUES (2, N'Maharastra')
SET IDENTITY_INSERT [dbo].[States] OFF
GO
SET IDENTITY_INSERT [dbo].[SubMenus] ON 

INSERT [dbo].[SubMenus] ([Id], [MenuId], [Name], [SortOrder]) VALUES (2016, 2016, N'View', NULL)
INSERT [dbo].[SubMenus] ([Id], [MenuId], [Name], [SortOrder]) VALUES (2017, 2017, N'View', NULL)
SET IDENTITY_INSERT [dbo].[SubMenus] OFF
GO
