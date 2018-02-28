CREATE TABLE [dbo].[SortedNumber]
(
	[Id] INT  IDENTITY (1, 1) NOT NULL PRIMARY KEY, 
    [OrderedDirection] NCHAR(10) NOT NULL, 
    [TimeTakenToSort] INT NOT NULL, 
    [Numbers] NVARCHAR(MAX) NOT NULL
)
