CREATE PROC spAM_UsersValidateIdentity
	@usr VARCHAR(100)
AS
BEGIN

	SELECT TOP 1 [guid]
	FROM tblUsers WITH(NOLOCK)
	WHERE ISNULL(active,0) = 1
		AND usr = @usr

END