use CorretoraPremium
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spVerificaLogin]
(
@cpf VARCHAR(80),
@senha nvarchar(50)
)

AS
BEGIN	
	SELECT * from dbo.Usuario
	where usr_cpf = @cpf and
	usr_senha = @senha
	and usr_deletado = 0	
END
