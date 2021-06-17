CREATE PROCEDURE [dbo].[spGetByIdBooks]
	@id int
AS
BEGIN
SELECT * FROM Books WHERE Id = @id
END