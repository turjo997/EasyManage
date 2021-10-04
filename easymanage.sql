
CREATE TABLE [dbo].[Users]
(
   [UserID] INT IDENTITY(1,1) primary key,
   [UserEmail] NVARCHAR(50) not null , 
   [UserPassword] NVARCHAR(50) not null , 
   [ConfirmPassword] NVARCHAR(50) not null ,
   [UserName] NVARCHAR(50) , 
   [UserAddress] NVARCHAR(MAX) ,
   [UserContact] NVARCHAR(50) not null
)

CREATE TABLE [dbo].[Admin]
(
   [AdminID] INT IDENTITY(1,1) primary key,
   [AdminEmail] NVARCHAR(50) not null , 
   [Password] NVARCHAR(50) not null , 
   [ConfirmPassword] NVARCHAR(50) not null ,
)

CREATE TABLE [dbo].[Contact]
(
   [ContactID] INT IDENTITY(1,1) primary key,
   [Email] NVARCHAR(50) not null , 
   [Name] NVARCHAR(50) not null , 
   [Subject] NVARCHAR(100) not null ,
   [Message] TEXT not null
   
)

CREATE TABLE [dbo].[ProductTypes]
(
   [ProductTypeID] INT IDENTITY(1,1) primary key,
   [ProductTypeName] NVARCHAR(50) not null   
)

CREATE TABLE [dbo].[Products]
(
   [ProductID] INT IDENTITY(1,1) primary key,
   [ProductName] NVARCHAR(50) not null ,
   [ProductPrice] DECIMAL(18 , 2) not null ,
   [ProductDetails] TEXT null ,
   [ProductTypeId] INT not null foreign key references ProductTypes(ProductTypeID)
)
CREATE TABLE [dbo].[Transaction]
(
   [ID] INT IDENTITY(1,1) primary key,
   [UserID] INT not null foreign key references Users(UserID) ,
   [OrderID] INT not null foreign key references Orders(OrderID) ,
   [UserName] NVARCHAR(50) not null,
   [CardType] NVARCHAR(50) not null,
   [TranID] NVARCHAR(150) not null  ,
   [BankID] NVARCHAR(150) not null  ,
   [Category] NVARCHAR(50) not null , 
   [Quantity] SMALLINT not null ,
   [Amount] DECIMAL(18 , 2) not null ,
   [Status] NVARCHAR(50) not null
)

CREATE TABLE [dbo].[Invoice]
(
   [ID] INT IDENTITY(1,1) primary key,
   [UserID] INT not null foreign key references Users(UserID) ,
   [Date] DATETIME(),
   [Totalbill] Double null 
)


CREATE TABLE [dbo].[Order1]
(
   [ID] INT IDENTITY(1,1) primary key,
   [InvoiceID] INT not null foreign key references Invoice(ID) ,
   [ProductID] INT not null foreign key references Products(ProductID),
   [O_Date]  DATETIME() ,
   [Quantity] SMALLINT not null,
   [O_bill] Double null ,
   [O_UnitPrice] int null
)

CREATE TABLE [dbo].[Comments]
(
   [CommentId] INT IDENTITY(1,1) primary key,
   [Comments] NVARCHAR(max)null , 
   [ThisDateTime] datetime not null , 
   [OrderId] int null,
   [Rating] int null 
)






