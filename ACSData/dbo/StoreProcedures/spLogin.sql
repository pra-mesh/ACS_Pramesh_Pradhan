CREATE PROCEDURE [dbo].[spLogin]
	@userName nvarchar(256),
	@password nvarchar(256)
AS
	SELECT [ID], '' as token,'' as RefreshToken,EmailConfirmed from [Users] where  [UserName]=@userName and [PasswordHash] =HASHBYTES('SHA2_512',@password);
RETURN 0
