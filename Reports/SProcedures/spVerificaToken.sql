USE CorretoraPremium
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spVerificaToken]
(
@token NVARCHAR(50)
)

AS
BEGIN	
	SELECT * from dbo.Usuario
	where usr_senha = @token 
	and usr_deletado = 0	
END
