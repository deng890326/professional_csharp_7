/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [Books]    Script Date: 10/3/2017 10:00:05 PM ******/
CREATE DATABASE [Books]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Books', FILENAME = N'C:\Users\wei\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\Books.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Books_log', FILENAME = N'C:\Users\wei\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\Books.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Books] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Books].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Books] SET ANSI_NULL_DEFAULT ON 
GO
ALTER DATABASE [Books] SET ANSI_NULLS ON 
GO
ALTER DATABASE [Books] SET ANSI_PADDING ON 
GO
ALTER DATABASE [Books] SET ANSI_WARNINGS ON 
GO
ALTER DATABASE [Books] SET ARITHABORT ON 
GO
ALTER DATABASE [Books] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Books] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Books] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Books] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Books] SET CURSOR_DEFAULT  LOCAL 
GO
ALTER DATABASE [Books] SET CONCAT_NULL_YIELDS_NULL ON 
GO
ALTER DATABASE [Books] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Books] SET QUOTED_IDENTIFIER ON 
GO
ALTER DATABASE [Books] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Books] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Books] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Books] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Books] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Books] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Books] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Books] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Books] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Books] SET RECOVERY FULL 
GO
ALTER DATABASE [Books] SET  MULTI_USER 
GO
ALTER DATABASE [Books] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Books] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Books] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Books] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Books] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Books] SET QUERY_STORE = OFF
GO
USE [Books]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Books]
GO
/****** Object:  Schema [ProCSharp]    Script Date: 10/3/2017 10:00:05 PM ******/
CREATE SCHEMA [ProCSharp]
GO
/****** Object:  Table [ProCSharp].[Books]    Script Date: 10/3/2017 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [ProCSharp].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Publisher] [nvarchar](50) NULL,
	[Isbn] [nvarchar](20) NOT NULL,
	[ReleaseDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [ProCSharp].[Books] ON 

INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (1, N'Professional C# 6 and .NET Core 1.0', N'Wrox Press', N'978-1-119-09660-3', CAST(N'2016-04-11' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (2, N'Professional C# 5.0 and .NET 4.5.1', N'Wrox Press', N'978-1-118-83303-2', CAST(N'2014-02-09' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (3, N'Enterprisee Services with the .NET Framework', N'Addison Wesley', N'978-0321246738', CAST(N'2005-06-03' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (4, N'Beginning Visual C# 2012 Programming', N'Wrox Press', N'978-1118314418', CAST(N'2012-12-04' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (5, N'Real World .NET, C#, and Silverlight', N'Wrox Press', N'978-1118021965 ', CAST(N'2011-11-22' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (6, N'Professional C# 3rd Edition', N'Wrox Press', N'978-0764557590', CAST(N'2004-06-02' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (7, N'Beginning Visual C# 2010', N'Wrox Press', N'978-0470502266', CAST(N'2010-04-05' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (8, N'Professional C# 2008', N'Wrox Press', N'978-0470191378', CAST(N'2008-05-24' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (9, N'Professional C# 4 and .NET 4', N'Wrox Press', N'978-0470502259', CAST(N'2010-03-08' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (10, N'Professional C# 2nd Edition', N'Wrox Press', N'978-1861007049', CAST(N'2002-03-28' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (11, N'Professional C# 2012 and .NET 4.5', N'Wrox Press', N'978-1118314425', CAST(N'2012-10-18' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (12, N'Professional C# 2005', N'Wrox Press', N'978-0764575341', CAST(N'2005-11-07' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (13, N'Beginning Visual C# 2005', N'Wrox Press', N'978-0764578472', CAST(N'2005-11-07' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (14, N'Pro .NET 1.1 Network Programming', N'APress', N'978-1590593455', CAST(N'2004-09-30' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (15, N'Beginning Visual C# 2008', N'Wrox Press', N'978-0470191354', CAST(N'2008-05-05' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (16, N'Beginning C#', N'Wrox Press', N'978-1861004987', CAST(N'2001-09-15' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (17, N'Beginning Visual C#', N'Wrox Press', N'978-0764543821', CAST(N'2002-08-20' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (18, N'Professional C# 2005 with .NET 3.0', N'Wrox Press', N'978-0470124727', CAST(N'2007-06-12' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (19, N'ASP to ASP.NET Migration Handbook', N'Wrox Press', N'978-1861008466', CAST(N'2003-02-01' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (1010, N'Professional C# Web Services', N'Wrox Press', N'978-1861004390', CAST(N'2001-12-01' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (1012, N'Professional C# (Beta 2 Edition)', N'Wrox Press', N'978-1861004994', CAST(N'2001-06-01' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (1014, N'Professional .NET Network Programming', N'Wrox Press', N'978-1861007353', CAST(N'2002-10-01' AS Date))
INSERT [ProCSharp].[Books] ([Id], [Title], [Publisher], [Isbn], [ReleaseDate]) VALUES (1015, N'Professional C# 7 and .NET Core 2.0', N'Wrox Press', N'978-1119449270', CAST(N'2018-04-02' AS Date))
SET IDENTITY_INSERT [ProCSharp].[Books] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Books_Isbn]    Script Date: 10/3/2017 10:00:05 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Books_Isbn] ON [ProCSharp].[Books]
(
	[Isbn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [ProCSharp].[GetBooksByPublisher]    Script Date: 10/3/2017 10:00:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [ProCSharp].[GetBooksByPublisher]
	@publisher nvarchar(50)
AS
	SELECT [Id], [Title], [Publisher], [ReleaseDate] FROM [ProCSharp].[Books] WHERE [Publisher] = @publisher ORDER BY [ReleaseDate]
GO
USE [master]
GO
ALTER DATABASE [Books] SET  READ_WRITE 
GO
