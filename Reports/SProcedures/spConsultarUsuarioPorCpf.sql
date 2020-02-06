USE CorretoraPremium
GO
/****** Object:  StoredProcedure [dbo].[spConsultarUsuarioPorCpf]    Script Date: 11/08/2017 13:21:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spConsultarUsuarioPorCpf]
(
@cpf VARCHAR(80)
)

AS
BEGIN	
	SELECT * from dbo.Usuario
	where usr_cpf = @cpf
	and usr_deletado = 0
END
