CREATE PROC spAM_UsersGet
	@guidUser VARCHAR(50)
AS
BEGIN

	SELECT u.[guid]
			,firstName
			,lastName
			,usr
			,active = ISNULL(active,0)
	FROM tblUsers u WITH(NOLOCK)
	WHERE u.[guid] = @guidUser

END