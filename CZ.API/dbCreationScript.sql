USE [master]
GO
/****** Object:  Database [CZ.BallestLane]    Script Date: 30/10/2023 16:20:58 ******/
CREATE DATABASE [CZ.BallestLane]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CZ.BallestLane', FILENAME = N'F:\SQL2022\MSSQL16.MSSQLSERVER\MSSQL\DATA\CZ.BallestLane.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CZ.BallestLane_log', FILENAME = N'F:\SQL2022\MSSQL16.MSSQLSERVER\MSSQL\DATA\CZ.BallestLane_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CZ.BallestLane] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CZ.BallestLane].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CZ.BallestLane] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET ARITHABORT OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CZ.BallestLane] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CZ.BallestLane] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CZ.BallestLane] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CZ.BallestLane] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET RECOVERY FULL 
GO
ALTER DATABASE [CZ.BallestLane] SET  MULTI_USER 
GO
ALTER DATABASE [CZ.BallestLane] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CZ.BallestLane] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CZ.BallestLane] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CZ.BallestLane] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CZ.BallestLane] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CZ.BallestLane] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CZ.BallestLane', N'ON'
GO
ALTER DATABASE [CZ.BallestLane] SET QUERY_STORE = ON
GO
ALTER DATABASE [CZ.BallestLane] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CZ.BallestLane]
GO
/****** Object:  Table [dbo].[Publication]    Script Date: 30/10/2023 16:20:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publication](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Date] [date] NULL,
	[PhotoContent] [nvarchar](max) NULL,
	[Base64Image] [nvarchar](max) NULL,
	[Text] [nvarchar](250) NULL,
 CONSTRAINT [PK_Publication] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 30/10/2023 16:20:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Token] [nvarchar](max) NULL,
	[RefreshTokenGuid] [nvarchar](50) NULL,
	[Expires] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 30/10/2023 16:20:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Password] [nvarchar](200) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([Id], [Key], [Email], [Name], [LastName], [Password]) VALUES (1, N'9AF4FB0C-7200-4071-A664-BC143EC3A1F8', N'Cris@gmail.com', N'Cristian', N'Zappala', N'CH9a7FKGOzZrw60zOvPazFfPNS4/')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Email_Unique]    Script Date: 30/10/2023 16:20:59 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Email_Unique] ON [dbo].[Users]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Publication]  WITH CHECK ADD  CONSTRAINT [FK_Publication_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Publication] CHECK CONSTRAINT [FK_Publication_Users]
GO
ALTER TABLE [dbo].[RefreshTokens]  WITH CHECK ADD  CONSTRAINT [FK_RefreshTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RefreshTokens] CHECK CONSTRAINT [FK_RefreshTokens_Users]
GO
USE [master]
GO
ALTER DATABASE [CZ.BallestLane] SET  READ_WRITE 
GO
