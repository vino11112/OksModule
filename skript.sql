USE [master]
GO
/****** Object:  Database [OksModuleDB]    Script Date: 30.06.2025 22:01:46 ******/
CREATE DATABASE [OksModuleDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OksModuleDB', FILENAME = N'D:\Новая папка (2)\MSSQL15.SQLEXPRESS\MSSQL\DATA\OksModuleDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OksModuleDB_log', FILENAME = N'D:\Новая папка (2)\MSSQL15.SQLEXPRESS\MSSQL\DATA\OksModuleDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OksModuleDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OksModuleDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OksModuleDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OksModuleDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OksModuleDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OksModuleDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OksModuleDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [OksModuleDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [OksModuleDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OksModuleDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OksModuleDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OksModuleDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OksModuleDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OksModuleDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OksModuleDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OksModuleDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OksModuleDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OksModuleDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OksModuleDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OksModuleDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OksModuleDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OksModuleDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OksModuleDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OksModuleDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OksModuleDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OksModuleDB] SET  MULTI_USER 
GO
ALTER DATABASE [OksModuleDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OksModuleDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OksModuleDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OksModuleDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OksModuleDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OksModuleDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OksModuleDB] SET QUERY_STORE = OFF
GO
USE [OksModuleDB]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 30.06.2025 22:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 30.06.2025 22:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[DocumentType] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Content] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Deadline] [datetime] NULL,
	[AuthorId] [int] NOT NULL,
	[RecipientDepartmentId] [int] NULL,
	[FilePath] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 30.06.2025 22:01:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([DepartmentId], [Name], [Code]) VALUES (1, N'ПТО', N'PTO')
INSERT [dbo].[Departments] ([DepartmentId], [Name], [Code]) VALUES (2, N'Юридический отдел', N'LEGAL')
INSERT [dbo].[Departments] ([DepartmentId], [Name], [Code]) VALUES (3, N'Проектно-сметный отдел', N'PSO')
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[Documents] ON 

INSERT [dbo].[Documents] ([DocumentId], [DocumentType], [Title], [Content], [Status], [CreatedDate], [Deadline], [AuthorId], [RecipientDepartmentId], [FilePath]) VALUES (1, N'Технические условия', N'fsdfds', N'sdfasdfasdf', N'Отправлен', CAST(N'2025-06-30T21:02:31.163' AS DateTime), CAST(N'2025-06-12T00:00:00.000' AS DateTime), 0, 1, NULL)
SET IDENTITY_INSERT [dbo].[Documents] OFF
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('Черновик') FOR [Status]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD FOREIGN KEY([RecipientDepartmentId])
REFERENCES [dbo].[Departments] ([DepartmentId])
GO
USE [master]
GO
ALTER DATABASE [OksModuleDB] SET  READ_WRITE 
GO
