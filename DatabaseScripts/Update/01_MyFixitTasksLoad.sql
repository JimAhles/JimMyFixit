--USE [Master]
--GO
--IF NOT EXISTS(SELECT * FROM sys.sysdatabases where name = 'MyFixitTasks')
--BEGIN
-- RETURN
--END
--CREATE DATABASE MyFixItTasks
--GO

--USE MyFixItTasks
--GO

/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 12/9/2015 8:36:27 AM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
if not exists (select * from sysobjects where name='__MigrationHistory' and xtype='U')
BEGIN
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
END
--GO
--/****** Object:  Table [dbo].[FixItTasks]    Script Date: 12/9/2015 8:36:27 AM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
if not exists (select * from sysobjects where name='FixItTasks' and xtype='U')
BEGIN
CREATE TABLE [dbo].[FixItTasks](
	[FixItTaskId] [int] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](80) NULL,
	[Owner] [nvarchar](80) NOT NULL,
	[Title] [nvarchar](80) NOT NULL,
	[Notes] [nvarchar](1000) NULL,
	[PhotoUrl] [nvarchar](200) NULL,
	[IsDone] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.FixItTasks] PRIMARY KEY CLUSTERED 
(
	[FixItTaskId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)
END
--GO
