CREATE TABLE [dbo].[Comments]
(
   [CommentId] INT IDENTITY(1,1) primary key,
   [Comments] NVARCHAR(max)null , 
   [ThisDateTime] datetime not null , 
   [OrderTd] int null,
   [Rating] int null ,
)
   
