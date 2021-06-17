CREATE PROCEDURE [dbo].[spCreateBooks]
	@title nvarchar(30),
	@author nvarchar(30),
	@description nvarchar(100)
AS
BEGIN
	INSERT INTO Books VALUES (QUOTENAME(@title),QUOTENAME(@author),QUOTENAME(@description))
END