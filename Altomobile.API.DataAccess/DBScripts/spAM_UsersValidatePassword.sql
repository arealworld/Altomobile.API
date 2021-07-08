CREATE PROC spAM_UsersValidatePassword
	@guidUser VARCHAR(50),
	@pwd VARCHAR(50)
AS
BEGIN

	SELECT TOP 1 [guid]
	FROM tblUsers WITH(NOLOCK)
	WHERE ISNULL(active,0) = 1
		AND [guid] = @guidUser
		AND pwd = @pwd

END