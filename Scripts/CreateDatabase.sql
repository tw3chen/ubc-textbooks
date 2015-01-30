CREATE DATABASE UBCTextbooks

GO

CREATE LOGIN ubctext_admin WITH PASSWORD = 'G72y3bnkw'

GO

USE UBCTextbooks
CREATE USER ubctext_admin FOR LOGIN ubctext_admin

GO

USE UBCTextbooks
CREATE LOGIN ubctext_visitor WITH PASSWORD = 'G72y3bnkw'

GO

USE UBCTextbooks
CREATE USER ubctext_visitor FOR LOGIN ubctext_visitor

GO

USE UBCTextbooks
CREATE LOGIN ubctext_user WITH PASSWORD = 'G72y3bnkw'

GO

USE UBCTextbooks
CREATE USER ubctext_user FOR LOGIN ubctext_user

GO

USE UBCTextbooks
CREATE TABLE dbo.Log
(
	LogId int PRIMARY KEY IDENTITY,
	MethodName varchar(30),
	StackTrace varchar(MAX),
	TimeStamp datetime
)

GO

USE UBCTextbooks
CREATE TABLE dbo.Account
(
	UserId int PRIMARY KEY IDENTITY,
	EmailAddress varchar(60) NOT NULL,
	Password varchar(15) NOT NULL,
	DisplayName nvarchar(60) NOT NULL,
	UserGuid varchar(40) NOT NULL,
	IsActivated bit NOT NULL
)

GO

USE UBCTextbooks
CREATE TABLE dbo.Department
(
	DepartmentId int PRIMARY KEY IDENTITY,
	Department varchar(8)
)

GO

USE UBCTextbooks
CREATE TABLE dbo.Area
(
	AreaId int PRIMARY KEY IDENTITY,
	Area nvarchar(30)
)

GO

USE UBCTextbooks
CREATE TABLE dbo.Book
(
	BookId int PRIMARY KEY IDENTITY,
	BookName nvarchar(100) NOT NULL,
	AuthorNames nvarchar(100),
	Edition varchar(8),
	ISBN varchar(15) UNIQUE,
	EAN varchar(15),
	ImageUrl varchar(100),
	Price smallmoney,
	BookBinding nvarchar(30),
	BookManufacturer nvarchar(50)
)

GO

USE UBCTextbooks
CREATE TABLE dbo.Course
(
	CourseId int PRIMARY KEY IDENTITY,
	CourseCode varchar(10) NOT NULL UNIQUE
)

GO

USE UBCTextbooks
CREATE TABLE dbo.R_CourseBook
(
	CourseId int FOREIGN KEY REFERENCES dbo.Course(CourseId) NOT NULL,
	BookId int FOREIGN KEY REFERENCES dbo.Book(BookId) NOT NULL
)

GO

CREATE PROCEDURE sp_AccountInsert
	@EmailAddress varchar(60),
	@Password varchar(15),
	@DisplayName nvarchar(60),
	@UserGuid varchar(40)
AS
INSERT INTO dbo.Account (EmailAddress, Password, DisplayName, UserGuid, IsActivated)
VALUES (@EmailAddress, @Password, @DisplayName, @UserGuid, 0)

GO

USE UBCTextbooks
CREATE TABLE dbo.List
(
	ListId int PRIMARY KEY IDENTITY,
	UserId int FOREIGN KEY REFERENCES dbo.Account(UserId) NOT NULL,
	BookId int FOREIGN KEY REFERENCES dbo.Book(BookId) NOT NULL,
	Condition varchar(15),
	Price smallmoney,
	Area nvarchar(30),
	Location varchar(80),
	Notes nvarchar(300),
	NumberOfOffers int NOT NULL,
	OfferAccepted bit NOT NULL,
	TimeListed smalldatetime
)

GO

USE UBCTextbooks
CREATE TABLE dbo.Comment
(
	CommentId int PRIMARY KEY IDENTITY,
	ListId int FOREIGN KEY REFERENCES dbo.List(ListId) NOT NULL,
	DisplayName nvarchar(60),
	EmailAddress varchar(60),
	OfferOrComment varchar(8),
	Comments nvarchar(500),
	CommentedTime smalldatetime
)

GO

CREATE PROCEDURE sp_ListInsert
	@UserId int,
	@BookId int,
	@Condition varchar(15),
	@Price smallmoney,
	@Area nvarchar(30),
	@Location varchar(80),
	@Notes nvarchar(300)
AS
INSERT INTO dbo.List (UserId, BookId, Condition, Price, Area, Location, Notes, NumberOfOffers, OfferAccepted, TimeListed)
VALUES (@UserId, @BookId, @Condition, @Price, @Area, @Location, @Notes, 0, 0, GETDATE())
SELECT 1

GO

CREATE PROCEDURE sp_BookInsert
	@CourseCode varchar(10),
	@BookName nvarchar(100),
	@AuthorNames nvarchar(100),
	@Edition varchar(8),
	@ISBN varchar(15),
	@EAN varchar(15),
	@ImageUrl varchar(100),
	@Price smallmoney,
	@BookBinding nvarchar(30),
	@BookManufacturer nvarchar(50)
AS
DECLARE @InsertedCourseId int
DECLARE @InsertedBookId int
IF NOT EXISTS(SELECT CourseId FROM dbo.Course WHERE CourseCode = @CourseCode)
BEGIN
	INSERT INTO dbo.Course (CourseCode) VALUES (@CourseCode)
END
INSERT INTO dbo.Book (BookName, AuthorNames, Edition, ISBN, EAN, ImageUrl, Price, BookBinding, BookManufacturer)
VALUES (@BookName, @AuthorNames, @Edition, @ISBN, @EAN, @ImageUrl, @Price, @BookBinding, @BookManufacturer)
SET @InsertedCourseId = (SELECT CourseId FROM dbo.Course WHERE CourseCode = @CourseCode)
SET @InsertedBookId = (SELECT BookId FROM dbo.Book WHERE ISBN = @ISBN OR EAN = @ISBN)
INSERT INTO dbo.R_CourseBook (CourseId, BookId) VALUES (@InsertedCourseId, @InsertedBookId)
SELECT @InsertedBookId

GO

CREATE PROCEDURE sp_RetrieveDepartment
AS
SELECT Department FROM dbo.Department

GO

CREATE PROCEDURE sp_RetrieveArea
AS
SELECT Area FROM dbo.Area

GO

CREATE PROCEDURE sp_AccountEdit
	@UserId int,
	@EmailAddress varchar(60),
	@Password varchar(15),
	@DisplayName nvarchar(60)
AS
UPDATE dbo.Account
SET EmailAddress = @EmailAddress, Password = @Password, DisplayName = @DisplayName
WHERE Userid = @UserId

GO

CREATE PROCEDURE sp_RetrieveListByUser
	@UserId int
AS
SELECT List.ListId, Book.BookName, Book.AuthorNames, Book.Edition, Book.ISBN, Book.EAN, List.Condition, List.Price, List.Area, List.NumberOfOffers, CONVERT(VARCHAR(19), List.TimeListed, 120) AS TimePosted
FROM List 
INNER JOIN Book
ON List.BookId = Book.BookId
WHERE List.UserId = @UserId
ORDER BY List.TimeListed DESC

GO

CREATE PROCEDURE sp_IsEmailOrNameRepeated
	@EmailAddress varchar(60),
	@DisplayName nvarchar(60),
	@UserId int
AS
SELECT count(*) FROM dbo.Account WHERE (EmailAddress = @EmailAddress OR DisplayName = @DisplayName) AND (UserId <> @UserId)

GO

CREATE PROCEDURE sp_ActivateEmail
	@UserGuid varchar(40)
AS
IF EXISTS(SELECT UserId FROM dbo.Account WHERE UserGuid = @UserGuid)
BEGIN
	UPDATE dbo.Account
	SET IsActivated = 1
	WHERE UserGuid = @UserGuid
	SELECT 1
END
ELSE
	SELECT 0

GO

CREATE PROCEDURE sp_Login
	@EmailAddress varchar(60)
AS
	SELECT UserId, Password, DisplayName, EmailAddress FROM dbo.Account WHERE EmailAddress = @EmailAddress AND IsActivated = 1

GO

CREATE PROCEDURE sp_CheckIfBookInDB
	@ISBN varchar(15),
	@CourseCode varchar(10)
AS
DECLARE @BookId int
SET @BookId = (SELECT BookId FROM dbo.Book WHERE ISBN = @ISBN OR EAN = @ISBN)
IF (@BookId > 0)
BEGIN
	IF (SELECT count(*) FROM R_CourseBook LEFT JOIN Course ON R_CourseBook.CourseId = Course.CourseId WHERE CourseCode = @CourseCode AND R_CourseBook.BookId = @BookId) > 0
	BEGIN
		SELECT 2
	END
	ELSE
	SELECT 1
END
ELSE
	SELECT 0

GO

CREATE PROCEDURE sp_RetrieveBookByISBN
	@ISBN varchar(15)
AS
SELECT BookId, BookName, AuthorNames, Edition, ISBN, EAN, ImageUrl, Price, BookBinding, BookManufacturer
FROM dbo.Book
WHERE ISBN = @ISBN OR EAN = @ISBN

GO

CREATE PROCEDURE sp_RetrieveCompleteList
	@ListId int
AS
DECLARE @CourseList varchar(100)
SET @CourseList = (SELECT CourseCode + '/' FROM List INNER JOIN Book ON List.BookId = Book.BookId INNER JOIN R_CourseBook ON Book.BookId = R_CourseBook.BookId INNER JOIN Course ON R_CourseBook.CourseId = Course.CourseId WHERE List.ListId = @ListId GROUP BY CourseCode FOR XML PATH(''))
SET @CourseList = SUBSTRING(@CourseList,1,LEN(@CourseList)-1)
SELECT Book.BookName, Book.AuthorNames, Book.Edition, Book.ISBN, Book.EAN, Book.ImageUrl, List.Location, List.Area, Book.Price AS RefPrice, Book.BookBinding, Book.BookManufacturer, List.Notes, @CourseList AS CourseList, List.Price, List.Condition, List.NumberOfOffers, List.TimeListed, List.UserId, Account.DisplayName
FROM List
INNER JOIN Book
ON Book.BookId = List.BookId
INNER JOIN Account
ON List.UserId = Account.UserId
WHERE ListId = @ListId

GO

CREATE PROCEDURE sp_RetrieveExtraInfo
	@ListId int
AS
DECLARE @CourseList varchar(100)
SET @CourseList = (SELECT CourseCode + '/' FROM List INNER JOIN Book ON List.BookId = Book.BookId INNER JOIN R_CourseBook ON Book.BookId = R_CourseBook.BookId INNER JOIN Course ON R_CourseBook.CourseId = Course.CourseId WHERE List.ListId = @ListId GROUP BY CourseCode FOR XML PATH(''))
SET @CourseList = SUBSTRING(@CourseList,1,LEN(@CourseList)-1)
SELECT Book.ImageUrl, List.Location, List.Area, Book.Price, Book.BookBinding, Book.BookManufacturer, List.Notes, @CourseList AS CourseList
FROM List
INNER JOIN Book
ON Book.BookId = List.BookId
WHERE ListId = @ListId

GO

CREATE PROCEDURE sp_CourseInsert
	@CourseCode varchar(10),
	@BookId int
AS
INSERT INTO dbo.Course (CourseCode)
VALUES (@CourseCode)
DECLARE @CourseId int
SET @CourseId = (SELECT CourseId FROM dbo.Course WHERE CourseCode = @CourseCode)
INSERT INTO dbo.R_CourseBook (CourseId, BookId)
VALUES (@CourseId, @BookId)
SELECT 1

GO

CREATE PROCEDURE sp_CommentInsert
	@ListId int,
	@DisplayName nvarchar(60),
	@EmailAddress varchar(60),
	@OfferOrComment varchar(8),
	@Comments nvarchar(500)
AS
IF (@OfferOrComment = 'Offer')
BEGIN
	UPDATE dbo.List
	SET NumberOfOffers = NumberOfOffers + 1
	WHERE List.ListId = @ListId
END
INSERT INTO dbo.Comment (ListId, DisplayName, EmailAddress, OfferOrComment, Comments, CommentedTime)
VALUES (@ListId, @DisplayName, @EmailAddress, @OfferOrComment, @Comments, GETDATE())

GO

CREATE PROCEDURE sp_RetrieveComments
	@ListId int
AS
SELECT DisplayName, EmailAddress, OfferOrComment, Comments, CONVERT(VARCHAR(19), CommentedTime, 120) AS TimePosted
FROM dbo.Comment
WHERE ListId = @ListId
ORDER BY CommentedTime DESC

GO

CREATE PROCEDURE sp_RetrieveUserInfo
	@UserId int
AS
SELECT DisplayName, EmailAddress
FROM dbo.Account
WHERE UserId = @UserId

GO

CREATE PROCEDURE sp_InsertLog
	@MethodName varchar(30),
	@StackTrace varchar(MAX)
AS
INSERT INTO dbo.Log (MethodName, StackTrace, TimeStamp)
VALUES (@MethodName, @StackTrace, GETDATE())

GO

CREATE PROCEDURE sp_RetrieveLog
AS
SELECT LogId, MethodName, StackTrace, TimeStamp
FROM dbo.Log
ORDER BY TimeStamp DESC

GO

CREATE PROCEDURE sp_RetrieveSearch
	@SearchType varchar(10),
	@Keywords nvarchar(100),
	@Area nvarchar(30),
	@Price smallmoney,
	@EarliestTimeListed smalldatetime,
	@SortBy varchar(10)
AS
DECLARE @UArea varchar(30)
SET @UArea = '%'
IF (@Area <> 'All')
BEGIN
	SET @UArea = @Area
END
DECLARE @UTimeListed smalldatetime
SET @UTimeListed = @EarliestTimeListed
IF (@SearchType = 'CourseCode')
BEGIN
	SELECT List.ListId, Book.BookName, Book.AuthorNames, Book.Edition, Book.ISBN, Book.EAN, List.Condition, List.Price, List.Area, List.NumberOfOffers, CONVERT(VARCHAR(19), List.TimeListed, 120) AS TimePosted,
		CASE
		WHEN @SortBy = 'PostedTime' THEN (RANK() OVER (ORDER BY List.TimeListed DESC))
		WHEN @SortBy = 'Price' THEN (RANK() OVER (ORDER BY List.Price ASC))
		END AS RankNumber
	FROM List 
	INNER JOIN Book
	ON List.BookId = Book.BookId
	INNER JOIN R_CourseBook
	ON Book.BookId = R_CourseBook.BookId
	INNER JOIN Course
	ON R_CourseBook.CourseId = Course.CourseId
	WHERE Course.CourseCode = @Keywords AND List.Area LIKE @UArea AND List.Price <= @Price AND List.TimeListed > @UTimeListed
	ORDER BY RankNumber
END
IF (@SearchType = 'Title')
BEGIN
	SELECT List.ListId, Book.BookName, Book.AuthorNames, Book.Edition, Book.ISBN, Book.EAN, List.Condition, List.Price, List.Area, List.NumberOfOffers, CONVERT(VARCHAR(19), List.TimeListed, 120) AS TimePosted,
		CASE
		WHEN @SortBy = 'PostedTime' THEN (RANK() OVER (ORDER BY List.TimeListed DESC))
		WHEN @SortBy = 'Price' THEN (RANK() OVER (ORDER BY List.Price ASC))
		END AS RankNumber
	FROM List 
	INNER JOIN Book
	ON List.BookId = Book.BookId
	WHERE Book.BookName LIKE '%' + @Keywords +'%' AND List.Area LIKE @UArea AND List.Price <= @Price AND TimeListed > @UTimeListed
	ORDER BY RankNumber
END
IF (@SearchType = 'Author')
BEGIN
	SELECT List.ListId, Book.BookName, Book.AuthorNames, Book.Edition, Book.ISBN, Book.EAN, List.Condition, List.Price, List.Area, List.NumberOfOffers, CONVERT(VARCHAR(19), List.TimeListed, 120) AS TimePosted,
		CASE
		WHEN @SortBy = 'PostedTime' THEN (RANK() OVER (ORDER BY List.TimeListed DESC))
		WHEN @SortBy = 'Price' THEN (RANK() OVER (ORDER BY List.Price ASC))
		END AS RankNumber
	FROM List 
	INNER JOIN Book
	ON List.BookId = Book.BookId
	WHERE Book.AuthorNames LIKE '%' + @Keywords +'%' AND List.Area LIKE @UArea AND List.Price <= @Price AND List.TimeListed > @UTimeListed
	ORDER BY RankNumber
END
IF (@SearchType = 'ISBN')
BEGIN
	SELECT List.ListId, Book.BookName, Book.AuthorNames, Book.Edition, Book.ISBN, Book.EAN, List.Condition, List.Price, List.Area, List.NumberOfOffers, CONVERT(VARCHAR(19), List.TimeListed, 120) AS TimePosted,
		CASE
		WHEN @SortBy = 'PostedTime' THEN (RANK() OVER (ORDER BY List.TimeListed DESC))
		WHEN @SortBy = 'Price' THEN (RANK() OVER (ORDER BY List.Price ASC))
		END AS RankNumber
	FROM List 
	INNER JOIN Book
	ON List.BookId = Book.BookId
	WHERE (Book.ISBN = @Keywords OR Book.EAN = @Keywords) AND List.Area LIKE @UArea AND List.Price <= @Price AND TimeListed > @UTimeListed
	ORDER BY RankNumber
END

GO

USE UBCTextbooks
GRANT EXECUTE ON sp_AccountInsert TO ubctext_visitor
GRANT EXECUTE ON sp_IsEmailOrNameRepeated TO ubctext_visitor
GRANT EXECUTE ON sp_ActivateEmail TO ubctext_visitor
GRANT EXECUTE ON sp_Login TO ubctext_visitor
GRANT EXECUTE ON sp_RetrieveDepartment TO ubctext_visitor
GRANT EXECUTE ON sp_RetrieveArea TO ubctext_visitor
GRANT EXECUTE ON sp_AccountEdit TO ubctext_user
GRANT EXECUTE ON sp_BookInsert TO ubctext_user
GRANT EXECUTE ON sp_ListInsert TO ubctext_user
GRANT EXECUTE ON sp_RetrieveListByUser TO ubctext_user
GRANT EXECUTE ON sp_CheckIfBookInDB TO ubctext_user
GRANT EXECUTE ON sp_RetrieveBookByISBN TO ubctext_user
GRANT EXECUTE ON sp_CourseInsert TO ubctext_user
GRANT EXECUTE ON sp_RetrieveBookByISBN TO ubctext_user
GRANT EXECUTE ON sp_RetrieveExtraInfo TO ubctext_user
GRANT EXECUTE ON sp_RetrieveCompleteList TO ubctext_visitor
GRANT EXECUTE ON sp_CommentInsert TO ubctext_visitor
GRANT EXECUTE ON sp_RetrieveComments TO ubctext_visitor
GRANT EXECUTE ON sp_RetrieveUserInfo TO ubctext_visitor
GRANT EXECUTE ON sp_RetrieveSearch TO ubctext_visitor
GRANT EXECUTE ON sp_InsertLog TO ubctext_visitor
GRANT EXECUTE ON sp_RetrieveLog TO ubctext_visitor