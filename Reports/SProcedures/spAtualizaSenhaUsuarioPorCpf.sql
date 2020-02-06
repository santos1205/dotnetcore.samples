USE [EnergiaSolar]
GO
/****** Object:  StoredProcedure [dbo].[spConsultarUsuarioPorEmail]    Script Date: 11/08/2017 13:21:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAtualizaSenhaUsuarioPorCpf] 
(
@cpf CHAR(11),
@novaSenha NVARCHAR(50)
)

AS
BEGIN	
	UPDATE dbo.Usuario set usr_senha = @novaSenha where usr_cpf = @cpf
END
