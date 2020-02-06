use CorretoraPremium
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spInserirLog]
(
	@descricao VARCHAR(100),
	@data datetime,
	@idUsuario int,
	@deletado bit
)

AS
BEGIN	
	INSERT INTO dbo.Log
 VALUES
   (@descricao
   ,@idUsuario
   ,@deletado
   ,@data       
   )
END
