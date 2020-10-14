USE [Library]

GO

-- USERS

SET IDENTITY_INSERT [Users].[User] ON

INSERT INTO [Users].[User]
           ([Id]
		   ,[FirstName]
           ,[LastName]
           ,[DateOfBirth])
     VALUES
		  (1, 'Chuck', 'Gates', '12/31/1969'),
		  (2, 'Ruby', 'Richard', '01/03/1983'),
		  (3, 'Cynthia', 'Wilcox', '10/25/1999'),
		  (4, 'Carson', 'Weeks', '02/03/1978'),
		  (5, 'Colette', 'Foley', '05/04/2010')
  
GO

SET IDENTITY_INSERT [Users].[User] OFF

-- USER CONTACTS

SET IDENTITY_INSERT [Users].[UserContact] ON

INSERT INTO [Users].[UserContact]
           ([Id]
		   ,[UserId]
           ,[Contact]
           ,[Type])
     VALUES 
		  (1, 1, 'eu@ridiculus.com', 'EMAIL' ),
		  (2, 1, '+385-33-533-7717', 'PHONE'),
		  (3, 2, 'ru.rich@adipiscing.com', 'EMAIL'),
		  (4, 1, 'facilisis.lorem@consequatnec.com', 'EMAIL' ),
		  (5, 2, '+385-91-622-2482', 'PHONE'),
		  (6, 3, 'cynthia@erat.edu', 'EMAIL'),
		  (7, 4, 'weeks.carson@egetmetus.org', 'EMAIL'),
		  (8, 4, '+385-99-912-1374', 'PHONE'),
		  (9, 4, '+385-51-158-3387', 'PHONE')

SET IDENTITY_INSERT [Users].[UserContact] OFF

GO

-- BOOK TITLES

SET IDENTITY_INSERT [Books].[BookTitle] ON

INSERT INTO [Books].[BookTitle]
           ([Id]
		   ,[Title]
           ,[Author])
     VALUES
			(1, 'Harry Potter and the Philosopher''s stone', 'J.K. Rowling'),
			(2, 'The Great Gatsby', 'F. Scott Fitzgerald'),
			(3, 'The Lord of the Rings', 'J.R.R. Tolkien'),
			(4, 'To the Lighthouse', 'Virginia Wolf'),
			(5, 'Clean Architecture', 'Robert C. Martin'),
			(6, 'Animal Farm', 'George Orwell'),
			(7, 'The Chronicles of Narnia', 'C.S. Lewis'),
			(8, 'The Portrait of a Lady', 'Charles Dickens'),
			(9, 'Charlotte''s Web', 'E.B. White'),
			(10, 'The Old Man and the Sea', 'Ernest Hemingway'),
			(11, ' The Call of the Wild', 'Jack London'),
			(12, 'Foundation', 'Isac Asimov')

GO

SET IDENTITY_INSERT [Books].[BookTitle] OFF

-- BOOK COPY

SET IDENTITY_INSERT [Books].[BookCopy] ON

INSERT INTO [Books].[BookCopy]
           ([Id],
		   [BookTitleId])
     VALUES
           (1,1),
		   (2,1),
		   (3,1),
		   (4,2),
		   (5,3),
		   (6,4),
		   (7,5),
		   (8,3),
		   (9,6),
		   (10,10),
		   (11,10),
		   (12,2),
		   (13,11),
		   (14,1),
		   (15,7),
		   (16,2),
		   (17,4),
		   (18,5),
		   (19,1),
		   (20,2),
		   (21,8),
		   (22,6),
		   (23,3),
		   (24,2),
		   (25,7),
		   (26,9),
		   (27,1),
		   (28,2)

GO

SET IDENTITY_INSERT [Books].[BookCopy] OFF

-- Rental

SET IDENTITY_INSERT [Rentals].[Rental] ON

INSERT INTO [Rentals].[Rental]
           ([Id]
		   ,[UserId]
		   ,[BookCopyId]
           ,[DateRented]
           ,[DateDue]
           ,[DateReturned]
           )
     VALUES
           (1, 1, 3, '02/01/2020',  '03/03/2020', NULL),
		   (2, 1, 5, '02/01/2020',  '03/30/2020', NULL),
		   (3, 2, 4, '05/04/2020',  '06/03/2020', NULL),
		   (4, 3, 4, '02/29/2020',  '04/03/2020', '04/04/2020'),
		   (5, 4, 5, '01/05/2020',  '01/31/2020', NULL),
		   (6, 5, 10, '03/11/2020',  '04/29/2020', '04/10/2020'),
		   (7, 3, 10, '04/15/2020',  '05/15/2020', '06/10/2020'),
		   (8, 5, 10, '07/01/2020',  '07/31/2020', NULL)   

SET IDENTITY_INSERT [Rentals].[Rental] OFF
