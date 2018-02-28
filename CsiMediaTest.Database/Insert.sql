CREATE PROCEDURE [dbo].[Insert]
	@OrderedDirection nvarchar (10),
	@TimeTakenToSort int,
	@Numbers nvarchar(max)
AS

BEGIN
	INSERT INTO SortedNumber(
	[OrderedDirection],
	[TimeTakenToSort],
	[Numbers])
VALUES(
	@OrderedDirection,
	@TimeTakenToSort,
	@Numbers)
END

Go

