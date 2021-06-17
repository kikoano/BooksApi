CREATE PROCEDURE [dbo].[spDeleteBooks]
	@id int
AS
BEGIN
	DELETE FROM Books WHERE Id = @id
END