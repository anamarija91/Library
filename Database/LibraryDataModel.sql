
-- Drop Foreign Key Constraints

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Books].[FK_BookCopy_BookTitle]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1) 
ALTER TABLE [Books].[BookCopy] DROP CONSTRAINT [FK_BookCopy_BookTitle]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Users].[FK_MRZData_User]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1) 
ALTER TABLE [Users].[MRZData] DROP CONSTRAINT [FK_MRZData_User]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Rentals].[FK_Rental_BookCopy]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1) 
ALTER TABLE [Rentals].[Rental] DROP CONSTRAINT [FK_Rental_BookCopy]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Rentals].[FK_Rental_User]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1) 
ALTER TABLE [Rentals].[Rental] DROP CONSTRAINT [FK_Rental_User]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Users].[FK_UserContact_User]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1) 
ALTER TABLE [Users].[UserContact] DROP CONSTRAINT [FK_UserContact_User]
GO

-- Drop Tables

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Books].[BookCopy]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [Books].[BookCopy]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Books].[BookTitle]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [Books].[BookTitle]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Users].[MRZData]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [Users].[MRZData]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Rentals].[Rental]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [Rentals].[Rental]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Users].[User]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [Users].[User]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Users].[UserContact]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [Users].[UserContact]
GO

-- Create Tables

CREATE TABLE [Books].[BookCopy]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[BookTitleId] int NOT NULL
)
GO

CREATE TABLE [Books].[BookTitle]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[Title] varchar(250) NOT NULL,
	[Author] varchar(250) NOT NULL
)
GO

CREATE TABLE [Users].[MRZData]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[FirstRow] varchar(30) NOT NULL,
	[SecondRow] varchar(30) NOT NULL,
	[ThirdRow] varchar(30) NOT NULL,
	[DOBValid] bit NOT NULL,
	[CardNumberValid] bit NOT NULL,
	[DOEValid] bit NOT NULL,
	[CompositeCheckValid] bit NOT NULL,
	[UserId] int NOT NULL,
	[CardNumber] varchar(9) NULL,
	[DateOfExpiry] date NULL
)
GO

CREATE TABLE [Rentals].[Rental]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[UserId] int NOT NULL,
	[DateRented] date NOT NULL,
	[DateDue] date NOT NULL,
	[DateReturned] date NULL,
	[BookCopyId] int NOT NULL
)
GO

CREATE TABLE [Users].[User]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[FirstName] varchar(150) NOT NULL,
	[LastName] varchar(150) NOT NULL,
	[DateOfBirth] date NOT NULL
)
GO

CREATE TABLE [Users].[UserContact]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[UserId] int NOT NULL,
	[Contact] varchar(250) NOT NULL,
	[Type] varchar(5) NOT NULL
)
GO

-- Create Primary Keys, Indexes, Uniques, Checks

ALTER TABLE [Books].[BookCopy] 
 ADD CONSTRAINT [PK_BookCopy]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

CREATE NONCLUSTERED INDEX [IXFK_BookCopy_BookTitle] 
 ON [Books].[BookCopy] ([BookTitleId] ASC)
GO

ALTER TABLE [Books].[BookTitle] 
 ADD CONSTRAINT [PK_BookTitle]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

ALTER TABLE [Users].[MRZData] 
 ADD CONSTRAINT [PK_MRZData]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

CREATE NONCLUSTERED INDEX [IXFK_MRZData_User] 
 ON [Users].[MRZData] ([UserId] ASC)
GO

ALTER TABLE [Rentals].[Rental] 
 ADD CONSTRAINT [PK_BookRental]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

CREATE NONCLUSTERED INDEX [IXFK_Rental_BookCopy] 
 ON [Rentals].[Rental] ([BookCopyId] ASC)
GO

CREATE NONCLUSTERED INDEX [IXFK_Rental_User] 
 ON [Rentals].[Rental] ([UserId] ASC)
GO

ALTER TABLE [Users].[User] 
 ADD CONSTRAINT [PK_User]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

ALTER TABLE [Users].[UserContact] 
 ADD CONSTRAINT [PK_UserContact]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

ALTER TABLE [Users].[UserContact] 
 ADD CONSTRAINT [CHK_Type] CHECK (Type IN ('EMAIL','PHONE'))
GO

CREATE NONCLUSTERED INDEX [IXFK_UserContact_User] 
 ON [Users].[UserContact] ([UserId] ASC)
GO

-- Create Foreign Key Constraints

ALTER TABLE [Books].[BookCopy] ADD CONSTRAINT [FK_BookCopy_BookTitle]
	FOREIGN KEY ([BookTitleId]) REFERENCES [Books].[BookTitle] ([Id]) ON DELETE Cascade ON UPDATE Cascade
GO

ALTER TABLE [Users].[MRZData] ADD CONSTRAINT [FK_MRZData_User]
	FOREIGN KEY ([UserId]) REFERENCES [Users].[User] ([Id]) ON DELETE Cascade ON UPDATE Cascade
GO

ALTER TABLE [Rentals].[Rental] ADD CONSTRAINT [FK_Rental_BookCopy]
	FOREIGN KEY ([BookCopyId]) REFERENCES [Books].[BookCopy] ([Id]) ON DELETE Cascade ON UPDATE Cascade
GO

ALTER TABLE [Rentals].[Rental] ADD CONSTRAINT [FK_Rental_User]
	FOREIGN KEY ([UserId]) REFERENCES [Users].[User] ([Id]) ON DELETE Cascade ON UPDATE Cascade
GO

ALTER TABLE [Users].[UserContact] ADD CONSTRAINT [FK_UserContact_User]
	FOREIGN KEY ([UserId]) REFERENCES [Users].[User] ([Id]) ON DELETE Cascade ON UPDATE Cascade
GO
