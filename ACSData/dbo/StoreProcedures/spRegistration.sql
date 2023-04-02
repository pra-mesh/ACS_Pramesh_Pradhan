CREATE PROCEDURE [dbo].[spRegistration](
	@Id nvarchar(128),
	@Email nvarchar(256),
	@password nvarchar(256),
	@PhoneNumber nvarchar(256),
	@firstname  nvarchar(100),
	@lastName nvarchar(100)
	)
AS
	INSERT INTO [dbo].[Users]
           ([Id],
		   [Email],
		   [PasswordHash],
		   [PhoneNumber],
		   [FirstName],
		   [LastName],
		   [UserName] )
		   Values 
		   (@Id,
		   @Email,
		   HASHBYTES('SHA2_512',@password),
		   @PhoneNumber,
		   @firstname,
		   @lastName,
		   @Email
		   )
RETURN 0
