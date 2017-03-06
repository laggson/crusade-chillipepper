USE [master]
GO
/****** Object:  Database [FWA]    Script Date: 06.03.2017 18:30:44 ******/
CREATE DATABASE [FWA]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FWA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\FWA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FWA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\FWA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [FWA] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FWA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FWA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FWA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FWA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FWA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FWA] SET ARITHABORT OFF 
GO
ALTER DATABASE [FWA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FWA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FWA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FWA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FWA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FWA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FWA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FWA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FWA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FWA] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FWA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FWA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FWA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FWA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FWA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FWA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FWA] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FWA] SET RECOVERY FULL 
GO
ALTER DATABASE [FWA] SET  MULTI_USER 
GO
ALTER DATABASE [FWA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FWA] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FWA] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FWA] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FWA] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'FWA', N'ON'
GO
ALTER DATABASE [FWA] SET QUERY_STORE = OFF
GO
USE [FWA]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [FWA]
GO
/****** Object:  Table [dbo].[Gegenstand]    Script Date: 06.03.2017 18:30:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gegenstand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[InvNummer] [nvarchar](255) NULL,
	[Pruefkarte] [bit] NULL,
	[Art] [nvarchar](255) NULL,
	[Kommentar] [nvarchar](255) NULL,
	[Zeitraum_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pruefung]    Script Date: 06.03.2017 18:30:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pruefung](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Datum] [datetime] NULL,
	[Kommentar] [nvarchar](255) NULL,
	[Mangel] [nvarchar](255) NULL,
	[Zustand] [nvarchar](255) NULL,
	[Gegenstand_id] [int] NULL,
	[Tester_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 06.03.2017 18:30:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[PwHash] [nvarchar](255) NULL,
	[PwSalt] [nvarchar](255) NULL,
	[Type] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Zeitraum]    Script Date: 06.03.2017 18:30:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zeitraum](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Januar] [bit] NULL,
	[Februar] [bit] NULL,
	[Maerz] [bit] NULL,
	[April] [bit] NULL,
	[Mai] [bit] NULL,
	[Juni] [bit] NULL,
	[Juli] [bit] NULL,
	[August] [bit] NULL,
	[September] [bit] NULL,
	[Oktober] [bit] NULL,
	[November] [bit] NULL,
	[Dezember] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Gegenstand] ON 

INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (1, N'MTF', N'', 0, N'TÜV / Stadt', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (2, N'MTF', N'', 0, N'Ölstand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (3, N'MTF', N'', 0, N'Flüssigkeiten', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (4, N'MTF', N'', 0, N'Beleuchtung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (5, N'TLF', N'', 0, N'TÜV bzw SP jährlich', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (6, N'TLF', N'', 0, N'Feuerlöscher', N'Prüffrist April 2016', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (7, N'TLF', N'', 0, N'Flüssigkeiten (Kühler/Scheiben)', N'Kühlwasser fast minimum', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (8, N'TLF', N'', 0, N'Ölstand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (9, N'TLF', N'', 0, N'Beleuchtung', N'Blinker vorne beide Seiten ern.', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (10, N'TLF', N'', 0, N'Fangleinen', N'Prüfung durch den Bauhof', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (11, N'LF', N'', 0, N'TÜV / Stadt', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (12, N'LF', N'', 0, N'Feuerlöscher', N'Prüffrist April 2016', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (13, N'LF', N'', 0, N'Ölstand', N'Sollte aufgefüllt werden', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (14, N'LF', N'', 0, N'Flüssigkeiten (Kühler/Scheiben)', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (15, N'LF', N'', 0, N'Beleuchtung', N'Positionsleuchte links mitte defekt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (16, N'LF', N'', 0, N'Fangleinen', N'Prüfung durch den Bauhof', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (17, N'LF', N'', 0, N'Steckleiter Dach', N'Prüfung durch den Bauhof', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (18, N'Halle', N'', 0, N'Feuerlöscher', N'Prüffrist April 2016', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (19, N'Kompressor', N'', 0, N'Ölstand', N'Sollte aufgefüllt werden', 1)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (20, N'Spinde', N'', 0, N'Feuerwehrgurte', N'Prüfung durch den Bauhof', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (21, N'Scheinwerfer', N'', 0, N'Jährlicher E-Check', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (22, N'Kabeltrommel', N'', 0, N'Jährlicher E-Check', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (23, N'Tauchpumpe', N'', 0, N'Jährlicher E-Check', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (24, N'Generator', N'', 0, N'Jährlicher E-Check', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (25, N'Steckdose G4', N'', 0, N'Jährlicher E-Check', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (26, N'Lichtmast DA', N'', 0, N'Jährlicher E-Check', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (27, N'TS 8/8', N'08LF10 G1 07 001', 0, N'Trockensaugp. Schließdruck', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (28, N'Pressluftatmer', N'08LF10 G1 01 001', 1, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (29, N'Pressluftatmer', N'08LF10 G1 01 002', 1, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (30, N'PA-Reserveflasche', N'08LF10 G1 01 005', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (31, N'PA-Reserveflasche', N'08LF10 G1 01 006', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (32, N'PA-Reserveflasche', N'08LF10 G1 01 007', 0, N'', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (33, N'PA-Reserveflasche', N'08LF10 G1 01 008', 0, N'', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (34, N'Motorkettensäge', N'08LF10 G1 09 001', 1, N'Nach Prüfordnung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (35, N'Kombikanister', N'08LF10 G1 09 004', 1, N'Ablaufjahr', N'2017', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (36, N'Wathose 1', N'08LF10 G1 01 011', 0, N'', N'Löcher undicht', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (37, N'Wathose 2', N'08LF10 G1 01 012', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (38, N'Brandfluchthaube', N'08LF10 G1 01 013', 1, N'Vollständigkeit/Ablaufdatum', N'Mai 19', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (39, N'Brandfluchthaube', N'08LF10 G1 01 014', 1, N'Vollständigkeit/Ablaufdatum', N'Mai 19', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (40, N'Übergangsstück C-D', N'08LF10 G2 03 001', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (41, N'Strahlrohr D', N'08LF10 G3 02 002', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (42, N'Strahlrohr C 1', N'08LF10 G2 02 003', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (43, N'Strahlrohr C 2', N'08LF10 G3 02 004', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (44, N'Strahlrohr C 3', N'08LF10 G3 02 005', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (45, N'Strahlrohr B', N'08LF10 G3 02 006', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (46, N'Stützkrümmer B', N'08LF10 G3 02 007', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (47, N'Standrohr', N'08LF10 G3 02 008', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (48, N'Übergangsstück B-C', N'08LF10 G2 03 018', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (49, N'Stromaggregat', N'08LF10 G2 07 003', 0, N'Funktion / Schutzleiter Prüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (50, N'Leitungsroller 240V', N'08LF10 G2 06 001', 0, N'Funktion / Beschädigungen', N'Nase der Kappe am Stecker', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (51, N'Leitungsroller 240V', N'08LF10 G2 06 002', 0, N'Funktion / Beschädigungen', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (52, N'Tauchpumpe', N'08LF10 G2 07 004', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (53, N'Hitzeschutzkleidung', N'08LF10 G2 01 015', 0, N'Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (54, N'Hitzeschutzkleidung', N'08LF10 G2 01 016', 0, N'Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (55, N'Absturzsicherung', N'08LF10 G2 04 003', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (56, N'Verteiler', N'08LF10 G3 03 024', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (57, N'Verteiler', N'08LF10 G3 03 025', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (58, N'Druckbegrenzungsventil', N'08LF10 G3 03 026', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (59, N'Übergangsstück B-C', N'08LF10 G3 03 027', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (60, N'Übergangsstück B-C', N'08LF10 G3 03 028', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (61, N'Druckschl. S DN 25/50', N'08LF10 G4 03 029', 1, N'Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (62, N'Pistolenstrahlrohr', N'08LF10 G4 03 030', 0, N'Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (63, N'Lüftungsgerät', N'08LF10 G4 07 008', 0, N'Funktion/Öl/Kraftstoff', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (64, N'Stativ', N'08LF10 G4 06 004', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (65, N'Zubehör zu Stativ', N'08LF10 G4 06 005', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (66, N'Flutlichtscheinwerfer', N'08LF10 G4 06 006', 0, N'', N'Kappe des Steckers fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (67, N'Flutlichtscheinwerfer', N'08LF10 G4 06 007', 0, N'', N'Kappe des Steckers fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (68, N'Verteiler 240V', N'08LF10 G4 06 008', 1, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (69, N'Hydrantenanschluss', N'08LF10 G4 03 032', 0, N'Funktion/Belastbarkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (70, N'Saugkorb', N'08LF10 GR 03 036', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (71, N'Saugschutzkorb', N'08LF10 GR 03 037', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (72, N'Druckschlauch 5m', N'08LF10 GR 03 038', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (73, N'Übergangsstück A-B', N'08LF10 GR 03 039', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (74, N'Sammelstück', N'08LF10 GR 03 040', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (75, N'Saugschlauch A', N'08LF10 GR 03 042', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (76, N'Saugschlauch A', N'08LF10 GR 03 043', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (77, N'Saugschlauch A', N'08LF10 GR 03 044', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (78, N'Saugschlauch A', N'08LF10 GR 03 045', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (79, N'Übergangsstück B-C', N'08LF10 GR 03 049', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (80, N'Mehrzweckleine', N'08LF10 GR 07 009', 0, N'Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (81, N'Mehrzweckleine', N'08LF10 GR 07 010', 0, N'Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (82, N'Saugschlauch', N'08LF10 GR 03 042', 0, N'Nach Prüfordnung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (83, N'Saugschlauch', N'08LF10 GR 03 043', 0, N'Nach Prüfordnung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (84, N'Saugschlauch', N'08LF10 GR 03 044', 0, N'Nach Prüfordnung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (85, N'Saugschlauch', N'08LF10 GR 03 045', 0, N'Nach Prüfordnung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (86, N'Krankentrage', N'08LF10 GR 05 001', 0, N'Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (87, N'Pumpe FP 8/8', N'08LF10 GR 07 011', 1, N'Trockensaugp. Schließdruck', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (88, N'Schaumrohr M2', N'08LF10 DA 02 001', 0, N'Funktion/Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (89, N'Flutlichtstrahler', N'08LF10 DA 06 015', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (90, N'Flutlichtstrahler', N'08LF10 DA 06 016', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (91, N'Pressluftatmer', N'08LF10 MR 01 017', 1, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (92, N'Pressluftatmer', N'08LF10 MR 01 018', 1, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (93, N'Handscheinwerfer', N'08LF10 MR 06 016', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (94, N'Handscheinwerfer', N'08LF10 MR 06 017', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (95, N'Handscheinwerfer', N'08LF10 MR 06 018', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (96, N'Handscheinwerfer', N'08LF10 MR 06 019', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (97, N'Kübelspritze', N'08LF10 MR 02 003', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (98, N'Handfunkgerät Digital', N'08LF10 MR 06 020', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (99, N'Handfunkgerät Digital', N'08LF10 MR 06 021', 0, N'', N'', NULL)
GO
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (100, N'Handfunkgerät Digital', N'08LF10 MR 06 022', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (101, N'Handfunkgerät Digital', N'08LF10 MR 06 023', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (102, N'Atemschutztafel', N'08LF10 MR 01 025', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (103, N'Brantfluchthaube', N'08LF10 MR 01 033', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (104, N'Brantfluchthaube', N'08LF10 MR 01 034', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (105, N'Werkzeughasten E', N'08LF10 S1 08 003', 0, N'Funktion/Vollständigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (106, N'Schnittschutz Jacke', N'08LF10 S1 010 35', 0, N'Zustand/Ablaufdatum', N'Entspricht nicht UVV Muß Form c', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (107, N'Schnittschutz Jacke', N'08LF10 S1 010 36', 0, N'Zustand/Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (108, N'Schnittschutz Hose', N'08LF10 S1 010 37', 0, N'Zustand/Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (109, N'Schnittschutz Helm', N'08LF10 S1 010 38', 0, N'Zustand/Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (110, N'Verbandskasten Feuerwehr', N'08LF10 S2 05 002', 0, N'Vollständigkeit/Ablaufdatum', N'Jun 20', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (111, N'Bindestrang', N'08LF10 S2 07 019', 0, N'Vollständigkeit', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (112, N'Bindestrang', N'08LF10 S2 07 020', 0, N'Vollständigkeit', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (113, N'Bindestrang', N'08LF10 S2 07 021', 0, N'Vollständigkeit', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (114, N'Bindestrang', N'08LF10 S2 07 022', 0, N'Vollständigkeit', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (115, N'Bindestrang', N'08LF10 S2 07 023', 0, N'Vollständigkeit', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (116, N'Bindestrang', N'08LF10 S2 07 024', 0, N'Vollständigkeit', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (117, N'Schaumrohr S2', N'08LF10 S2 02 005', 0, N'Funktion/Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (118, N'Zumischer Z2', N'08LF10 S2 02 006', 0, N'Funktion/Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (119, N'Ansaugschlauch D', N'08LF10 S2 02 007', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (120, N'Schaummittelbehälter', N'08LF10 S2 02 008', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (121, N'Schaummittelbehälter', N'08LF10 S2 02 009', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (122, N'Schaummittelbehälter', N'08LF10 S2 02 010', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (123, N'Werkzeugkasten Makita', N'08LF10 S2 08 002', 0, N'Zustand/Vollständigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (124, N'Handfunkgerät Digital', N'08LF10 FR 06 026', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (125, N'Handfunkgerät Digital', N'08LF10 FR 06 027', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (126, N'Handfunkgerät Digital', N'08LF10 FR 06 028', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (127, N'Handfunkgerät Digital', N'08LF10 FR 06 029', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (128, N'Warnlampe LKW', N'08LF10 FR 06 030', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (129, N'Warnlampe LKW', N'08LF10 FR 06 031', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (130, N'Winkerkelle', N'08LF10 FR 06 032', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (131, N'Winkerkelle', N'08LF10 FR 06 033', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (132, N'Metabo Handlampe', N'08LF10 FR 06 034', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (133, N'Metabo Handlampe', N'08LF10 FR 06 035', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (134, N'Handlampe LNW', N'08LF10 FR 06 039', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (135, N'Handlampe LNW', N'08LF10 FR 06 040', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (136, N'Handlampe LNW', N'08LF10 FR 06 041', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (137, N'Suchscheinwerfer', N'08LF10 FR 06 042', 0, N'Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (138, N'Funkgerät MRT', N'08LF10 FR 06 043', 0, N'Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (139, N'Funkgerät 4m analog', N'08LF10 FR 06 044', 0, N'Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (140, N'Mikro für Außenlautspr.', N'08LF10 FR -- ---', 0, N'Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (141, N'Vollschutzanzug', N'08TF30 G1 01 043', 1, N'Zubehör', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (142, N'Vollschutzanzug', N'08TF30 G1 01 044', 1, N'Zubehör', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (143, N'Vollschutzanzug', N'08TF30 G1 01 045', 1, N'Zubehör', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (144, N'Vollschutzanzug', N'08TF30 G1 01 046', 1, N'Zubehör', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (145, N'Ablegeplane', N'08TF30 G1 01 021', 0, N'erneuern?', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (146, N'Helmsprechgarnitur', N'08TF30 G1 06 045', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (147, N'Helmsprechgarnitur', N'08TF30 G1 06 045', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (148, N'Zubehör CSA', N'08TF30 G1 09 022', 0, N'Inhalt?', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (149, N'Hitzeschutzkleidung', N'08TF30 G2 01 047', 0, N'Typ 3', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (150, N'Hitzeschutzkleidung', N'08TF30 G2 01 048', 0, N'Typ 3', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (151, N'Trainingsanzug', N'08TF30 G2 01 049', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (152, N'Trainingsanzug', N'08TF30 G2 01 050', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (153, N'Trainingsanzug', N'08TF30 G2 01 051', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (154, N'Trainingsanzug', N'08TF30 G2 01 052', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (155, N'Mehrzweckschutzanzug', N'08TF30 G2 01 053', 0, N'Tychern F', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (156, N'Mehrzweckschutzanzug', N'08TF30 G2 01 054', 0, N'Tychern F', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (157, N'Mehrzweckschutzanzug', N'08TF30 G2 01 055', 0, N'Tychern F', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (158, N'Mehrzweckschutzanzug', N'08TF30 G2 01 056', 0, N'Tychern F', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (159, N'Mehrzweckschutzanzug', N'08TF30 G2 01 057', 0, N'Tychern C', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (160, N'Mehrzweckschutzanzug', N'08TF30 G2 01 058', 0, N'Tychern C', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (161, N'Mehrzweckschutzanzug', N'08TF30 G2 01 059', 0, N'Tychern C', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (162, N'Mehrzweckschutzanzug', N'08TF30 G2 01 060', 0, N'Tychern C', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (163, N'Schutzhandschuhe', N'08TF30 G2 01 061', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (164, N'Schutzhandschuhe', N'08TF30 G2 01 062', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (165, N'Schutzhandschuhe', N'08TF30 G2 01 063', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (166, N'Schutzhandschuhe', N'08TF30 G2 01 064', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (167, N'Klebeband Rolle', N'08TF30 G2 09 023', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (168, N'Absperrmaterial Satz', N'08TF30 G2 06 047', 0, N'Vollständigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (169, N'Verkehrswarnleuchten', N'08TF30 G2 06 048', 0, N'4x Stab- 2x Stehleuchten', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (170, N'Verkehrswarnleuchten', N'08TF30 G2 06 049', 0, N'4x Stab- 2x Stehleuchten', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (171, N'Verkehrswarnleuchten', N'08TF30 G2 06 050', 0, N'4x Stab- 2x Stehleuchten', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (172, N'Verkehrswarnleuchten', N'08TF30 G2 06 051', 0, N'4x Stab- 2x Stehleuchten', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (173, N'Verkehrswarnleuchten', N'08TF30 G2 06 052', 0, N'4x Stab- 2x Stehleuchten', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (174, N'Verkehrswarnleuchten', N'08TF30 G2 06 053', 0, N'4x Stab- 2x Stehleuchten', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (175, N'Verkehrsleitkegel', N'08TF30 G2 06 054', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (176, N'Verkehrsleitkegel', N'08TF30 G2 06 055', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (177, N'Verkehrsleitkegel', N'08TF30 G2 06 056', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (178, N'Verkehrsleitkegel', N'08TF30 G2 06 057', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (179, N'Verkehrsleitkegel', N'08TF30 G2 06 058', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (180, N'Verkehrsleitkegel', N'08TF30 G2 06 059', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (181, N'Verkehrsleitkegel', N'08TF30 G2 06 060', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (182, N'Verkehrsleitkegel', N'08TF30 G2 06 061', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (183, N'Verkehrsleitkegel', N'08TF30 G2 06 062', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (184, N'Verkehrsleitkegel', N'08TF30 G2 06 063', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (185, N'Faltsignal', N'08TF30 G2 06 064', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (186, N'Faltsignal', N'08TF30 G2 06 065', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (187, N'Streuwagen Gard. Klein', N'08TF30 G2 09 024', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (188, N'Motorkettensäge + Zubehör', N'08TF30 G2 07 033', 1, N'Nach Prüfordnung', N'Öl Dichtung instandgesetzt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (189, N'Schwert Titan f. Kettens.', N'08TF30 G2 07 034', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (190, N'Kombikanister', N'08TF30 G2 07 035', 1, N'', N'Ablaufdatum 11/11', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (191, N'Motortrennschleifer + Zubehör', N'08TF30 G2 07 035', 0, N'Nach Prüfordnung', N'Anziehvorrichtung defekt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (192, N'Pressluftatmer', N'08TF30 G3 01 065', 0, N'KFTZ', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (193, N'Pressluftatmer', N'08TF30 G3 01 066', 0, N'KFTZ', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (194, N'PA Reserveflasche', N'08TF30 G3 01 069', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (195, N'PA Reserveflasche', N'08TF30 G3 01 070', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (196, N'PA Reserveflasche', N'08TF30 G3 01 071', 0, N'', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (197, N'PA Reserveflasche', N'08TF30 G3 01 072', 0, N'', N'Leer', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (198, N'Kübelspritze', N'08TF30 G3 02 010', 0, N'Funktion/Füllung', N'D-Schlauch undicht', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (199, N'Brandfluchthaube zu PA', N'08TF30 G3 01 073', 0, N'Zustand/Ablaufdatum 10/18', N'', NULL)
GO
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (200, N'Brandfluchthaube zu PA', N'08TF30 G3 01 074', 0, N'Zustand/Ablaufdatum 10/19', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (201, N'Rohrdichtkissen 1', N'08TF30 G3 07 040', 0, N'Zustand/ Dichttigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (202, N'Rohrdichtkissen 2', N'08TF30 G3 07 041', 0, N'Zustand/ Dichttigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (203, N'Rohrdichtkissen 3', N'08TF30 G3 07 042', 0, N'Zustand/ Dichttigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (204, N'Standrohr 1', N'08TF30 G4 03 057', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (205, N'Standrohr 2', N'08TF30 G4 03 058', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (206, N'Hohlstrahlrohr C 1', N'08TF30 G4 03 064', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (207, N'Hohlstrahlrohr C 2', N'08TF30 G4 03 065', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (208, N'Hohlstrahlrohr C 3', N'08TF30 G4 03 066', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (209, N'Hohlstrahlrohr B', N'08TF30 G4 03 067', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (210, N'Stützkrümmer B', N'08TF30 G4 03 068', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (211, N'Hydroschild', N'08TF30 G4 03 069', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (212, N'Aufsatz für Holstrahlrohr 1', N'08TF30 G4 03 070', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (213, N'Aufsatz für Holstrahlrohr 2', N'08TF30 G4 03 071', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (214, N'Löschlanze', N'08TF30 G4 03 073', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (215, N'Seilschlauchhalter 1', N'08TF30 G4 03 059', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (216, N'Seilschlauchhalter 2', N'08TF30 G4 03 060', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (217, N'Seilschlauchhalter 3', N'08TF30 G4 03 061', 0, N'', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (218, N'Seilschlauchhalter 4', N'08TF30 G4 03 062', 0, N'', N'Fehlt', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (219, N'Feuerwehraxt', N'08TF30 G4 07 043', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (220, N'Schlauchtragekorb C15 1', N'08TF30 G5 03 074', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (221, N'Schlauchtragekorb C15 2', N'08TF30 G5 03 075', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (222, N'Schlauchtragekorb C15 3', N'08TF30 G5 03 076', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (223, N'Schlauchtragekorb C15 4', N'08TF30 G5 03 077', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (224, N'Verteiler', N'08TF30 G5 03 078', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (225, N'Druckbegrenzungsventil', N'08TF30 G5 03 079', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (226, N'Strahlrohr D', N'08TF30 G5 03 080', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (227, N'Übergangsstück C-D', N'08TF30 G5 03 082', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (228, N'Übergangsstück B-C', N'08TF30 G5 03 083', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (229, N'Übergangsstück B-C', N'08TF30 G5 03 084', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (230, N'Druckschlauch D', N'08TF30 G5 03 080', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (231, N'Saugschlauch A 1', N'08TF30 G5 03 085', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (232, N'Saugschlauch A 2', N'08TF30 G5 03 086', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (233, N'Saugschlauch A 3', N'08TF30 G5 03 087', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (234, N'Saugschlauch A 4', N'08TF30 G5 03 088', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (235, N'Montage Werkzeugkasten', N'08TF30 G5 08 005', 0, N'Inhalt', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (236, N'Leckdichtkissen m. Zub.', N'08TF30 G5 07 044', 0, N'Zustand/Dichtigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (237, N'Rohrdichtkissen 1', N'08TF30 G5 07 047', 0, N'Zustand/Dichtigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (238, N'Rohrdichtkissen 2', N'08TF30 G5 07 048', 0, N'Zustand/Dichtigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (239, N'Saugschlauch 1', N'08TF30 G5 07 085', 1, N'Nach Prüfordnung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (240, N'Saugschlauch 2', N'08TF30 G5 07 086', 1, N'Nach Prüfordnung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (241, N'Saugschlauch 3', N'08TF30 G5 07 087', 1, N'Nach Prüfordnung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (242, N'Saugschlauch 4', N'08TF30 G5 07 088', 1, N'Nach Prüfordnung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (243, N'Hohlstrahlrohr C CAFS 1', N'08TF30 G6 03 092', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (244, N'Hohlstrahlrohr C CAFS 2', N'08TF30 G6 03 093', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (245, N'Anschlußst. Schaumrohr 1', N'08TF30 G6 03 094', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (246, N'Anschlußst. Schaumrohr 1', N'08TF30 G6 03 095', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (247, N'Schlauchtragekkorb C ', N'08TF30 G6 03 096', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (248, N'Verteiler', N'08TF30 G6 03 097', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (249, N'Anschlußst. Holstrahlrohr 1', N'08TF30 G6 03 098', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (250, N'Anschlußst. Holstrahlrohr 2', N'08TF30 G6 03 099', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (251, N'Druckschlauch S DN 25/50', N'08TF30 G6 03 090', 1, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (252, N'Hohlstrahlrohr am SA', N'08TF30 G6 03 091', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (253, N'Schaummittelbehälter 1', N'08TF30 G6 02 011', 0, N'WHZ (Empfehlung)', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (254, N'Schaummittelbehälter 2', N'08TF30 G6 02 012', 0, N'WHZ (Empfehlung)', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (255, N'Schaummittelbehälter 3', N'08TF30 G6 02 013', 0, N'WHZ (Empfehlung)', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (256, N'Schaummittelbehälter 4', N'08TF30 G6 02 014', 0, N'WHZ (Empfehlung)', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (257, N'Schaummittelbehälter 5', N'08TF30 G6 02 015', 0, N'WHZ (Empfehlung)', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (258, N'Schaummittelbehälter 6', N'08TF30 G6 02 016', 0, N'WHZ (Empfehlung)', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (259, N'Saugschlauch f. One 7 1', N'08TF30 G6 02 018', 0, N'Belastungsprüfung', N'Belüftungsbohrung gefertigt 02 019', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (260, N'Saugschlauch f. One 7 2', N'08TF30 G6 02 019', 0, N'Belastungsprüfung', N'Belüftungsbohrung gefertigt 02 020', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (261, N'Saugschlauch f. One 7 3', N'08TF30 G6 02 020', 0, N'Belastungsprüfung', N'Belüftungsbohrung gefertigt 02 021', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (262, N'One 7 Anlage Schaum', N'08TF30 G6 02 021', 1, N'Funktion', N'Abgang 2 Schaum nicht i. O.', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (263, N'Sammelstück', N'08TF30 GR 03 100', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (264, N'Druckschlauch B 5m', N'08TF30 GR 03 101', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (265, N'Übergangsstück A-B', N'08TF30 GR 03 102', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (266, N'Übergangsstück A-B', N'08TF30 GR 03 103', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (267, N'Saugkorb', N'08TF30 GR 03 104', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (268, N'Saugschutzkorb', N'08TF30 GR 03 105', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (269, N'Übergangsstück B-C', N'08TF30 GR 03 106', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (270, N'Mehrzweckleine 1', N'08TF30 GR 07 049', 0, N'Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (271, N'Mehrzweckleine 2', N'08TF30 GR 07 050', 0, N'Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (272, N'Saugkorb', N'08TF30 GR 03 108', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (273, N'Pumpe FPN 10-2000', N'08TF30 GR 07 051', 1, N'Trockensaugp. Schließdr.', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (274, N'Wasserwerfer', N'08TF30 DA 03 114', 0, N'Funktion/Zustand', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (275, N'Mehrzweckdüse', N'08TF30 DA 03 115', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (276, N'Rundstrahldüse', N'08TF30 DA 03 116', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (277, N'Druckschlauch B 30 cm', N'08TF30 DA 03 117', 0, N'Belastungsprüfung', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (278, N'Xenon Scheinwerfer 1', N'08TF30 DA 06 066', 0, N'Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (279, N'Xenon Scheinwerfer 2', N'08TF30 DA 06 067', 0, N'Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (280, N'Pressluftatmer 1', N'08TF30 MR 01 075', 0, N'KFTZ', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (281, N'Pressluftatmer 2', N'08TF30 MR 01 076', 0, N'KFTZ', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (282, N'Handscheinwerfer 1', N'08TF30 MR 06 068', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (283, N'Handscheinwerfer 2', N'08TF30 MR 06 069', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (284, N'Handlampe Adalit 1', N'08TF30 MR 06 070', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (285, N'Handlampe Adalit 2', N'08TF30 MR 06 071', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (286, N'Handlampe Adalit 3', N'08TF30 MR 06 072', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (287, N'Verkehrswarnweste 1', N'08TF30 MR 01 083', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (288, N'Verkehrswarnweste 2', N'08TF30 MR 01 084', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (289, N'Verkehrswarnweste 3', N'08TF30 MR 01 085', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (290, N'Verkehrswarnweste 4', N'08TF30 MR 01 086', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (291, N'Brandfluchthaube 1', N'08TF30 MR 01 087', 0, N'Ablaufd. 09/18', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (292, N'Brandfluchthaube 2', N'08TF30 MR 01 088', 0, N'Ablaufd. 09/19', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (293, N'Atemsch. Überwachungst. 1', N'08TF30 MR 01 089', 0, N'Funktion/Batterie', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (294, N'Atemsch. Überwachungst. 2', N'08TF30 MR 01 090', 0, N'Funktion/Batterie', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (295, N'Sicherheitstrupptasche', N'08TF30 MR 01 091', 0, N'Reparatur durch KFZ durchgef.', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (296, N'Digitalfunkgerät 1', N'08TF30 MR 06 073', 0, N'Reparatur durch KFZ R. Strake', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (297, N'Digitalfunkgerät 2', N'08TF30 MR 06 074', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (298, N'Multimeter Atomar', N'08TF30 MR 08 006', 0, N'Kalibriert bis 2016', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (299, N'Warnlampe LKW 1', N'08TF30 S1 06 075', 0, N'Funktion', N'', NULL)
GO
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (300, N'Warnlampe LKW 2', N'08TF30 S1 06 076', 0, N'Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (301, N'Atemans. Normal Druck 1', N'08TF30 S1 01 093', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (302, N'Atemans. Normal Druck 2', N'08TF30 S1 01 094', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (303, N'Atemans. Normal Druck 3', N'08TF30 S1 01 095', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (304, N'Atemans. Normal Druck 4', N'08TF30 S1 01 096', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (305, N'Abeck Filter 1', N'08TF30 S1 01 097', 0, N'Zustand/Ablaufdatum 12/12', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (306, N'Abeck Filter 2', N'08TF30 S1 01 098', 0, N'Zustand/Ablaufdatum 12/13', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (307, N'Abeck Filter 3', N'08TF30 S1 01 099', 0, N'Zustand/Ablaufdatum 12/14', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (308, N'Abeck Filter 4', N'08TF30 S1 01 100', 0, N'Zustand/Ablaufdatum 12/15', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (309, N'Feuerwehrbeil 1', N'08TF30 S1 01 101', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (310, N'Feuerwehrbeil 2', N'08TF30 S1 01 102', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (311, N'Feuerwehrbeil 3', N'08TF30 S1 01 103', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (312, N'Schnittschutz Jacke ', N'08TF30 S1 01 104', 0, N'Zustand/Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (313, N'Schnittschutz Hose ', N'08TF30 S1 01 105', 0, N'Zustand/Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (314, N'Schnittschutz Handschuhe', N'08TF30 S1 01 106', 0, N'Zustand/Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (315, N'Schnittschutz Helm', N'08TF30 S1 01 107', 0, N'Zustand/Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (316, N'Verbandskasten FW', N'08TF30 S2 05 003', 0, N'Vollständigkeit/Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (317, N'Schäkel 1', N'08TF30 S2 07 072', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (318, N'Schäkel 2', N'08TF30 S2 07 073', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (319, N'Abschleppseil 5m', N'08TF30 S2 07 074', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (320, N'Bindestänge 1', N'08TF30 S2 07 075', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (321, N'Bindestänge 2', N'08TF30 S2 07 076', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (322, N'Bindestänge 3', N'08TF30 S2 07 077', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (323, N'Bindestänge 4', N'08TF30 S2 07 078', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (324, N'Bindestänge 5', N'08TF30 S2 07 079', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (325, N'Bindestänge 6', N'08TF30 S2 07 080', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (326, N'Handlautsprecher', N'08TF30 S2 06 077', 0, N'Ladung/Funktion', N'Batt neu 02/15', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (327, N'Werkzeug Makita', N'08TF30 S2 08 007', 0, N'Zustand/Vollständigkeit', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (328, N'Coldpack 1', N'08TF30 S2 ', 0, N'Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (329, N'Coldpack 2', N'08TF30 S2 ', 0, N'Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (330, N'Desinfektionsmittel', N'08TF30 S2 ', 0, N'Ablaufdatum', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (331, N'Winkerkelle 1', N'08TF30 FR 06 080', 0, N'', N'Batt neu 02/15', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (332, N'Winkerkelle 2', N'08TF30 FR 06 081', 0, N'', N'Batt neu 02/15', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (333, N'Mehrgasmessgerät', N'08TF30 FR 08 008', 0, N'Kalibrieren', N'Pb-Süd 07/15', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (334, N'Digitfunkgerät 1', N'08TF30 FR 06 082', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (335, N'Digitfunkgerät 2', N'08TF30 FR 06 083', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (336, N'Verkehrswarnweste', N'08TF30 FR 01 109', 0, N'', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (337, N'FuG 4m Analog', N'08TF30 FR 06 084', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (338, N'Funkgerät MRT', N'08TF30 FR 06 085', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (339, N'Metabo Handlampe', N'08TF30 FR 06 087', 0, N'Ladung/Funktion', N'', NULL)
INSERT [dbo].[Gegenstand] ([Id], [Name], [InvNummer], [Pruefkarte], [Art], [Kommentar], [Zeitraum_id]) VALUES (340, N'Handlampe Adalit ', N'08TF30 FR 06 088', 0, N'Ladung/Funktion', N'', NULL)
SET IDENTITY_INSERT [dbo].[Gegenstand] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [Email], [PwHash], [PwSalt], [Type]) VALUES (1, N'hs', N'hermann.schmidt24@freenet.de', N'$2a$06$PsAJEE0sHthY1qcEvS179.c5IOnN78flJFP9qK1LmbuIonR1RW/6O', N'$2a$06$PsAJEE0sHthY1qcEvS179.', 2)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Zeitraum] ON 

INSERT [dbo].[Zeitraum] ([Id], [Januar], [Februar], [Maerz], [April], [Mai], [Juni], [Juli], [August], [September], [Oktober], [November], [Dezember]) VALUES (1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[Zeitraum] OFF
ALTER TABLE [dbo].[Gegenstand]  WITH CHECK ADD  CONSTRAINT [FK69A84C354D0F1FDB] FOREIGN KEY([Zeitraum_id])
REFERENCES [dbo].[Zeitraum] ([Id])
GO
ALTER TABLE [dbo].[Gegenstand] CHECK CONSTRAINT [FK69A84C354D0F1FDB]
GO
ALTER TABLE [dbo].[Gegenstand]  WITH CHECK ADD  CONSTRAINT [FK69A84C354FF41D70] FOREIGN KEY([Zeitraum_id])
REFERENCES [dbo].[Zeitraum] ([Id])
GO
ALTER TABLE [dbo].[Gegenstand] CHECK CONSTRAINT [FK69A84C354FF41D70]
GO
ALTER TABLE [dbo].[Pruefung]  WITH CHECK ADD  CONSTRAINT [FK60F32EE93FF21D6C] FOREIGN KEY([Gegenstand_id])
REFERENCES [dbo].[Gegenstand] ([Id])
GO
ALTER TABLE [dbo].[Pruefung] CHECK CONSTRAINT [FK60F32EE93FF21D6C]
GO
ALTER TABLE [dbo].[Pruefung]  WITH CHECK ADD  CONSTRAINT [FK60F32EE9476E40FA] FOREIGN KEY([Tester_id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Pruefung] CHECK CONSTRAINT [FK60F32EE9476E40FA]
GO
ALTER TABLE [dbo].[Pruefung]  WITH CHECK ADD  CONSTRAINT [FK60F32EE96D95A47B] FOREIGN KEY([Tester_id])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Pruefung] CHECK CONSTRAINT [FK60F32EE96D95A47B]
GO
ALTER TABLE [dbo].[Pruefung]  WITH CHECK ADD  CONSTRAINT [FK60F32EE98ADAE9FE] FOREIGN KEY([Gegenstand_id])
REFERENCES [dbo].[Gegenstand] ([Id])
GO
ALTER TABLE [dbo].[Pruefung] CHECK CONSTRAINT [FK60F32EE98ADAE9FE]
GO
USE [master]
GO
ALTER DATABASE [FWA] SET  READ_WRITE 
GO
