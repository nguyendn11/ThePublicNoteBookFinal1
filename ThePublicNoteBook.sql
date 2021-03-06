USE [master]
GO
/****** Object:  Database [ThePublicNoteBook]    Script Date: 28-Nov-18 2:22:02 PM ******/
CREATE DATABASE [ThePublicNoteBook]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ThePublicNoteBook', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ThePublicNoteBook.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ThePublicNoteBook_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ThePublicNoteBook_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ThePublicNoteBook] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ThePublicNoteBook].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ThePublicNoteBook] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET ARITHABORT OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ThePublicNoteBook] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ThePublicNoteBook] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ThePublicNoteBook] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ThePublicNoteBook] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ThePublicNoteBook] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET RECOVERY FULL 
GO
ALTER DATABASE [ThePublicNoteBook] SET  MULTI_USER 
GO
ALTER DATABASE [ThePublicNoteBook] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ThePublicNoteBook] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ThePublicNoteBook] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ThePublicNoteBook] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ThePublicNoteBook', N'ON'
GO
USE [ThePublicNoteBook]
GO
/****** Object:  Table [dbo].[ArticleLikes]    Script Date: 28-Nov-18 2:22:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticleLikes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ArticleId] [int] NOT NULL,
 CONSTRAINT [PK_ArticleLikes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Articles]    Script Date: 28-Nov-18 2:22:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Image] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[DateUp] [datetime] NOT NULL,
	[Views] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pictures]    Script Date: 28-Nov-18 2:22:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pictures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](250) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Pictures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLevels]    Script Date: 28-Nov-18 2:22:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLevels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LevelName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserLevels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 28-Nov-18 2:22:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [varchar](100) NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NULL,
	[Phone] [varchar](30) NULL,
	[Address] [nvarchar](250) NULL,
	[Avatar] [nvarchar](250) NULL,
	[LevelId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Index [IX_FK_ArticleLikes_Articles]    Script Date: 28-Nov-18 2:22:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_ArticleLikes_Articles] ON [dbo].[ArticleLikes]
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_ArticleLikes_Users]    Script Date: 28-Nov-18 2:22:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_ArticleLikes_Users] ON [dbo].[ArticleLikes]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Articles_Users]    Script Date: 28-Nov-18 2:22:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Articles_Users] ON [dbo].[Articles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Pictures_Users]    Script Date: 28-Nov-18 2:22:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Pictures_Users] ON [dbo].[Pictures]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_Users_UserLevels]    Script Date: 28-Nov-18 2:22:02 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_Users_UserLevels] ON [dbo].[Users]
(
	[LevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ArticleLikes]  WITH CHECK ADD  CONSTRAINT [FK_ArticleLikes_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([Id])
GO
ALTER TABLE [dbo].[ArticleLikes] CHECK CONSTRAINT [FK_ArticleLikes_Articles]
GO
ALTER TABLE [dbo].[ArticleLikes]  WITH CHECK ADD  CONSTRAINT [FK_ArticleLikes_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ArticleLikes] CHECK CONSTRAINT [FK_ArticleLikes_Users]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Users]
GO
ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD  CONSTRAINT [FK_Pictures_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_Pictures_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserLevels] FOREIGN KEY([LevelId])
REFERENCES [dbo].[UserLevels] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserLevels]
GO
USE [master]
GO
ALTER DATABASE [ThePublicNoteBook] SET  READ_WRITE 
GO
