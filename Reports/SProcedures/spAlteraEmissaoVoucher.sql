USE [CorretoraPremium]
GO
/****** Object:  StoredProcedure [dbo].[spAlteraEmissaoVoucher]    Script Date: 03/01/2018 13:48:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



/*
 Procedimento para alterar voucher
 Parâmetros : @ems_status : (1)Emitido (2)Cancelado (3)Descontinuado
			  @ems_id     : id do voucher
			  @id_usuario : id do usuário
			  @ems_data_cancelamento : data do cancelamento do voucher
 Autor : Julio
 Data  : 26/02/2018
*/               
              
ALTER  PROCEDURE [dbo].[spAlteraEmissaoVoucher]
		@ems_status             int, 
		@ems_id					int,
        @ems_data_cancelamento  date,		
        @idUsuario              int

AS

DECLARE
		@data                   datetime = getdate(),
		@deletado               bit = 0,
		@ems_nr_voucher         varchar(50),
		@ems_status_anterior    int, 
		@descricao              varchar(100),
		@MsgRetorno             varchar(1000),
		@ems_inicio_vigencia    date,
		@ems_premio_licitacao   numeric(10,2)
		


		 SELECT @ems_inicio_vigencia = ems_inicio_vigencia,
				@ems_premio_licitacao = ems_premio_licitacao,
				@ems_nr_voucher = ems_nr_voucher, 
				@ems_status_anterior = ems_status
		 FROM emissao_voucher 
		 WHERE ems_id = @ems_id	
		
IF @ems_status = 2 and @ems_data_cancelamento >= (SELECT ems_inicio_vigencia FROM emissao_voucher WHERE ems_id = @ems_id)
		BEGIN 	     
			 SET @MsgRetorno = 'Data de cancelamento superior ou igual ao início de vigência do Voucher' 
		     SELECT @MsgRetorno as retorno
		END
	 ELSE
	    IF @ems_status = 2 and @ems_data_cancelamento < (SELECT ems_inicio_vigencia FROM emissao_voucher WHERE ems_id = @ems_id)
			BEGIN
				 SELECT @descricao = 'Alteracao Status'
			    
				 exec spInserirLog
					  @descricao ,
					  @ems_nr_voucher,
					  @ems_status_anterior,
					  @ems_status,
					  @data ,
					  @idUsuario ,
					  @deletado 
					  
					 UPDATE emissao_voucher SET     
					 ems_premio_licitacao = 0,
					 ems_premio_anterior  = @ems_premio_licitacao, 
					 ems_status           = @ems_status,
					 ems_data_cancelamento= @ems_data_cancelamento
					 WHERE    
					 ems_id               = @ems_id

					 SET @MsgRetorno = 'Voucher cancelado com sucesso'
					 SELECT @MsgRetorno	 as retorno			 				  	    
			END 
		ELSE 
	       IF @ems_status = 3 and @ems_data_cancelamento < (SELECT ems_inicio_vigencia FROM emissao_voucher WHERE ems_id = @ems_id)
			   BEGIN
					 SET @MsgRetorno = 'Data de DESCONTINUIDADE não pode ser inferior à data de início de vigência do Voucher' 
					 SELECT @MsgRetorno  as retorno
			   END 
		   ELSE
			   IF @ems_status = 3 and @ems_data_cancelamento >= (SELECT ems_inicio_vigencia FROM emissao_voucher WHERE ems_id = @ems_id)
				   BEGIN
						 SELECT @descricao = 'Alteracao Status'
			    
						 exec spInserirLog
							  @descricao ,
							  @ems_nr_voucher,
							  @ems_status_anterior,
							  @ems_status,
							  @data ,
							  @idUsuario ,
							  @deletado 
							  
							 DECLARE @QtdDiarias int, @NovoPremio numeric(10,2) 
							 SET @QtdDiarias = DATEDIFF(DAY,@ems_inicio_vigencia,@ems_data_cancelamento)
					         SET @NovoPremio = @QtdDiarias * 7.33
					         
							  
							 UPDATE emissao_voucher SET     
							 ems_premio_licitacao = @NovoPremio,
							 ems_premio_anterior  = @ems_premio_licitacao, 
							 ems_status           = @ems_status,
							 ems_data_cancelamento= @ems_data_cancelamento
							 WHERE    
							 ems_id               = @ems_id

							 SET @MsgRetorno = 'Voucher descontinuado com sucesso'
							 SELECT @MsgRetorno	 as retorno							 
				   END 
			   ELSE		   
					BEGIN   
						 SELECT @descricao = 'Alteracao Status'
					    
						 exec spInserirLog
							  @descricao ,
							  @ems_nr_voucher,
							  @ems_status_anterior,
							  @ems_status,
							  @data ,
							  @idUsuario ,
							  @deletado 
					   
						 UPDATE emissao_voucher SET     
						 ems_status           = @ems_status,
						 ems_premio_anterior  = @ems_premio_licitacao,
						 ems_premio_licitacao = 0 
						 WHERE    
						 ems_id               = @ems_id
						 
						SET @MsgRetorno = 'Registro alterado com sucesso' 			 
						
						SELECT @MsgRetorno  as retorno
					END 


