CREATE TABLE [dbo].[Articles]
(
   [AutoId] INT IDENTITY(1,1) primary key,
   [Title] NVARCHAR(50)null , 
   [Description] NVARCHAR(50)null , 
   [Active] bit null 
)
   
   
CREATE TABLE [dbo].[ArticlesComments]
(
   [CommentId] INT IDENTITY(1,1) primary key,
   [Comments] NVARCHAR(max)null , 
   [ThisDateTime] datetime not null , 
   [ArticleTd] int null,
   [Rating] int null ,
)
   