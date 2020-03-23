/* Create Database, if exists drop it */
USE MASTER
GO
DROP DATABASE IF EXISTS [SimplyHelp]

CREATE DATABASE [SimplyHelp]

/*** Create Role Table ***/
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[Role]    Script Date: 3/23/2020 12:31:00 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/*** Create Permissions Table ***/
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[Permissions]    Script Date: 3/23/2020 12:32:44 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Permissions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[module] [varchar](50) NOT NULL,
	[description] [varchar](100) NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/* Create tblSubscription */
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[tblSubscription]    Script Date: 3/23/2020 12:33:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblSubscription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[PhoneNumber] [varchar](12) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[Address1] [varchar](100) NOT NULL,
	[Address2] [varchar](100) NULL,
	[City] [varchar](100) NOT NULL,
	[State] [varchar](100) NOT NULL,
	[County] [varchar](100) NOT NULL,
	[ZipCode] [varchar](5) NOT NULL,
	[AuthorizeContact] [bit] NULL,
 CONSTRAINT [PK_tblSubscription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/* Role_Permission */
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[Role_Permission]    Script Date: 3/23/2020 12:33:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Role_Permission](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idRole] [int] NULL,
	[idPermission] [int] NULL,
 CONSTRAINT [PK_Role_Permission] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Role_Permission]  WITH CHECK ADD  CONSTRAINT [FK_Role_Permission_Permissions] FOREIGN KEY([idPermission])
REFERENCES [dbo].[Permissions] ([id])
GO

ALTER TABLE [dbo].[Role_Permission] CHECK CONSTRAINT [FK_Role_Permission_Permissions]
GO

ALTER TABLE [dbo].[Role_Permission]  WITH CHECK ADD  CONSTRAINT [FK_Role_Permission_Role] FOREIGN KEY([idRole])
REFERENCES [dbo].[Role] ([id])
GO

ALTER TABLE [dbo].[Role_Permission] CHECK CONSTRAINT [FK_Role_Permission_Role]
GO

/* Create users table */
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[users]    Script Date: 3/23/2020 12:34:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullName] [varchar](100) NOT NULL,
	[email] [varchar](100) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[idRole] [int] NOT NULL,
	[date] [datetime] NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_Role] FOREIGN KEY([idRole])
REFERENCES [dbo].[Role] ([id])
GO

ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_Role]
GO

/* Create userGeo table */
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[userGeo]    Script Date: 3/23/2020 12:35:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[userGeo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NOT NULL,
	[latitude] [varchar](50) NULL,
	[longitude] [varchar](50) NULL,
	[dateAdded] [varchar](50) NULL,
 CONSTRAINT [PK_userGeo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/* CREATE UserMembers table*/
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[UserMembers]    Script Date: 3/23/2020 12:35:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserMembers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[fullName] [varchar](100) NOT NULL,
	[phoneNumber] [varchar](50) NULL,
	[email] [varchar](100) NULL,
	[latitude] [varchar](100) NULL,
	[longitude] [varchar](100) NULL,
	[zipCode] [varchar](6) NULL,
 CONSTRAINT [PK_UserMembers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/* Create Carrier Table */
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[Carrier]    Script Date: 3/23/2020 12:36:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Carrier](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[carrier_Name] [varchar](50) NOT NULL,
	[carrier_Gateway] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Carrier] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/* Create Disaster */
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[Disaster]    Script Date: 3/23/2020 12:38:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Disaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisasterName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Disaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/* Create AlertType */
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[AlertType]    Script Date: 3/23/2020 12:38:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AlertType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AlertTypeName] [varchar](50) NOT NULL,
	[id_Disaster] [int] NOT NULL,
 CONSTRAINT [PK_AlertType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AlertType]  WITH CHECK ADD  CONSTRAINT [FK_AlertType_Disaster] FOREIGN KEY([id_Disaster])
REFERENCES [dbo].[Disaster] ([Id])
GO

ALTER TABLE [dbo].[AlertType] CHECK CONSTRAINT [FK_AlertType_Disaster]
GO

/* Create AlertMessage */
USE [SimplyHelp]
GO

/****** Object:  Table [dbo].[AlertMessage]    Script Date: 3/23/2020 12:39:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AlertMessage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[AlertMessageName] [varchar](100) NOT NULL,
	[id_AlertType] [int] NOT NULL,
 CONSTRAINT [PK_AlertMessage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AlertMessage]  WITH CHECK ADD  CONSTRAINT [FK_AlertMessage_AlertType] FOREIGN KEY([id_AlertType])
REFERENCES [dbo].[AlertType] ([id])
GO

ALTER TABLE [dbo].[AlertMessage] CHECK CONSTRAINT [FK_AlertMessage_AlertType]
GO



/* Insert into table role */
insert into [dbo].[Role] values ('Admin');
insert into [dbo].[Role] values ('Manager');
insert into [dbo].[Role] values ('User');

/* Insert into table role */

insert into [dbo].[users] values ('Jane Doe','janed@mail.com','98563214',3,NULL)
insert into [dbo].[users] values ('James Doe','jamesd@mail.com','123456789',2,NULL)








