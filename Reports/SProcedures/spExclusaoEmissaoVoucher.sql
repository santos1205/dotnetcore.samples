USE [CorretoraPremium]
GO
/****** Object:  StoredProcedure [dbo].[spExclusaoEmissaoVoucher]    Script Date: 03/01/2018 13:48:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
 Procedimento para excluir voucher
 Parâmetros : @ems_id     : id do voucher
			  @id_usuario : id do usuário
 Autor : Julio
 Data  : 26/02/2018
*/               
            
ALTER  PROCEDURE [dbo].[spExclusaoEmissaoVoucher] 
		@ems_id					int,
        @idUsuario              int

AS

DECLARE
		@data                   datetime = getdate(),
		@deletado               bit = 0,
		@descricao              varchar(100),
		@MsgRetorno             varchar(1000),
		@ems_nr_voucher         varchar(50)
		
 SELECT @ems_nr_voucher = ems_nr_voucher
 FROM   emissao_voucher 
 WHERE  ems_id = @ems_id			

SELECT @descricao = 'Exclusao Voucher'
    
exec spInserirLog
  @descricao ,
  @ems_nr_voucher,
  NULL,
  NULL,
  @data ,
  @idUsuario ,
  @deletado 
   
 UPDATE emissao_voucher SET     
 ems_deletado         = 1
 WHERE    
 ems_id               = @ems_id
 
 SET @MsgRetorno = 'Registro excluido com sucesso' 	
 
 SELECT @MsgRetorno     
     
