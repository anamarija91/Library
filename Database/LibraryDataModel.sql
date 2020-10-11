
-- Drop Foreign Key Constraints

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[FK_BookCopy_BookTitle]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1) 
ALTER TABLE [BookCopy] DROP CONSTRAINT [FK_BookCopy_BookTitle]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[FK_Rental_BookCopy]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1) 
ALTER TABLE [Rental] DROP CONSTRAINT [FK_Rental_BookCopy]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[FK_Rental_User]') AND OBJECTPROPERTY(id, N'IsForeignKey') = 1) 
ALTER TABLE [Rental] DROP CONSTRAINT [FK_Rental_User]
GO

-- Drop Tables

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[BookCopy]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [BookCopy]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[BookTitle]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [BookTitle]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[Rental]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [Rental]
GO

IF EXISTS (SELECT 1 FROM dbo.sysobjects WHERE id = object_id(N'[User]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1) 
DROP TABLE [User]
GO

-- Create Tables

CREATE TABLE [BookCopy]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[BookTitleId] int NOT NULL
)
GO

CREATE TABLE [BookTitle]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[Title] varchar(250) NOT NULL,
	[Author] varchar(250) NOT NULL
)
GO

CREATE TABLE [Rental]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[UserId] int NOT NULL,
	[DateRented] date NOT NULL,
	[DateDue] date NOT NULL,
	[DateReturned] date NULL,
	[BookCopyId] int NOT NULL
)
GO

CREATE TABLE [User]
(
	[Id] int NOT NULL IDENTITY (1, 1),
	[FirstName] varchar(150) NOT NULL,
	[LastName] varchar(150) NOT NULL,
	[DateOfBirth] date NOT NULL,
	[Email] varchar(250) NULL,
	[PhoneNumber] varchar(50) NULL,
	[Address] varchar(250) NOT NULL
)
GO

-- Create Primary Keys, Indexes, Uniques, Checks

ALTER TABLE [BookCopy] 
 ADD CONSTRAINT [PK_BookCopy]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

CREATE NONCLUSTERED INDEX [IXFK_BookCopy_BookTitle] 
 ON [BookCopy] ([BookTitleId] ASC)
GO

ALTER TABLE [BookTitle] 
 ADD CONSTRAINT [PK_BookTitle]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

ALTER TABLE [Rental] 
 ADD CONSTRAINT [PK_BookRental]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

CREATE NONCLUSTERED INDEX [IXFK_Rental_BookCopy] 
 ON [Rental] ([BookCopyId] ASC)
GO

CREATE NONCLUSTERED INDEX [IXFK_Rental_User] 
 ON [Rental] ([UserId] ASC)
GO

ALTER TABLE [User] 
 ADD CONSTRAINT [PK_User]
	PRIMARY KEY CLUSTERED ([Id] ASC)
GO

-- Create Foreign Key Constraints

ALTER TABLE [BookCopy] ADD CONSTRAINT [FK_BookCopy_BookTitle]
	FOREIGN KEY ([BookTitleId]) REFERENCES [BookTitle] ([Id]) ON DELETE Cascade ON UPDATE Cascade
GO

ALTER TABLE [Rental] ADD CONSTRAINT [FK_Rental_BookCopy]
	FOREIGN KEY ([BookCopyId]) REFERENCES [BookCopy] ([Id]) ON DELETE Cascade ON UPDATE Cascade
GO

ALTER TABLE [Rental] ADD CONSTRAINT [FK_Rental_User]
	FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE Cascade ON UPDATE Cascade
GO
