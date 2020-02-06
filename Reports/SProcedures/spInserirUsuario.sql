use CorretoraPremium
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spInserirUsuario]
(
@nome VARCHAR(100),
@genero CHAR(1),
@dataNascimento date,
@Cpf char(11),
@email VARCHAR(50),
@dataCadastro datetime,
@senha VARCHAR(50),
@telefone char(11),
@aprovado bit = null,
@deletado bit
)

AS
BEGIN	
	INSERT INTO [CorretoraPremium].[dbo].[Usuario]          
 VALUES
       (@nome
       ,@genero
       ,@dataNascimento
       ,@Cpf
       ,@email
       ,@dataCadastro
       ,@deletado
       ,@senha
       ,@telefone
       ,@aprovado)
END
