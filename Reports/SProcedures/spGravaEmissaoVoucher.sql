USE [CorretoraPremium]
GO
/****** Object:  StoredProcedure [dbo].[spGravaEmissaoVoucher]    Script Date: 03/01/2018 13:48:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
 Procedimento para inclusão na tabela emissao_voucher
 Parâmetros : todos os campos da tabela
 Autor : Julio
 Data  : 19/02/2018
*/               
              
ALTER  PROCEDURE [dbo].[spGravaEmissaoVoucher]  
		@ems_nome_passageiro	varchar(180),
		@ems_cpf				varchar(11),
		@ems_nr_voucher			varchar(50),
		@ems_diaria				int,
		@ems_destino			varchar(150),
		@ems_inicio_vigencia    date,
		@ems_fim_vigencia       date,
		@ems_premio_licitacao   numeric(10,2),
		@ems_status             int, --- (1)Emitido (2)Cancelado (3)Descontinuado
		@ems_deletado           bit,
		@ems_id					int,
        @idUsuario              int
        
        
AS    

DECLARE
		@data                   datetime = getdate(),
		@deletado               bit = 0,
		@descricao              varchar(100),
		@MsgRetorno             varchar(1000)

		
----------------------------------------    
--Inclusao
----------------------------------------
    INSERT INTO emissao_voucher
    (ems_nome_passageiro,ems_cpf,ems_nr_voucher,ems_diaria,ems_destino,ems_inicio_vigencia,ems_fim_vigencia,ems_premio_licitacao,ems_status, ems_deletado, ems_dt_cadastro)
    VALUES    
    (    
		@ems_nome_passageiro,
		@ems_cpf,				
		@ems_nr_voucher,
        @ems_diaria,			
        @ems_destino,		
        @ems_inicio_vigencia,
        @ems_fim_vigencia,
        @ems_premio_licitacao,
        @ems_status,
        @ems_deletado,
        GETDATE()
    )            
    
    SELECT @ems_id = MAX(ems_id) FROM emissao_voucher
    SELECT @descricao = 'Registro Voucher'
    
	 exec spInserirLog
		  @descricao ,
		  @ems_nr_voucher,
		  NULL,
		  NULL,
		  @data ,
		  @idUsuario ,
		  @deletado 
    
    SET @MsgRetorno = 'Registro incluido com sucesso' 
    
    SELECT @MsgRetorno
    		 

/*
ELSE  
----------------------------------------    
--Alteracao
----------------------------------------    
if @flagInsert = 'A'
  BEGIN   
	 
	 IF @ems_status = 2 and @ems_data_cancelamento >= (SELECT ems_inicio_vigencia FROM emissao_voucher WHERE ems_id = @ems_id)
		BEGIN 	     
			 SET @MsgRetorno = 'Data de cancelamento superior ou igual ao início de vigência do Voucher' 
		     SELECT @MsgRetorno
		END
	 ELSE
	    IF @ems_status = 2 and @ems_data_cancelamento < (SELECT ems_inicio_vigencia FROM emissao_voucher WHERE ems_id = @ems_id)
			BEGIN
				 SELECT @descricao = 'Atualizacao realizada com sucesso no voucher : ' + convert(char(10),@ems_id)
			    
				 exec spInserirLog
					  @descricao ,
					  @data ,
					  @idUsuario ,
					  @deletado 
					  
					 UPDATE emissao_voucher SET     
					 ems_nome_passageiro  = @ems_nome_passageiro,
					 ems_cpf              = @ems_cpf,	
					 ems_nr_voucher       = @ems_nr_voucher,
					 ems_diaria           = @ems_diaria,
					 ems_destino          = @ems_destino,
					 ems_inicio_vigencia  = @ems_inicio_vigencia,
					 ems_fim_vigencia     = @ems_fim_vigencia,
					 ems_premio_licitacao = 0,
					 ems_premio_anterior  = @ems_premio_licitacao, 
					 ems_status           = @ems_status,
					 ems_deletado         = @ems_deletado,
					 ems_data_cancelamento= @ems_data_cancelamento
					 WHERE    
					 ems_id               = @ems_id

					 SET @MsgRetorno = 'Voucher cancelado com sucesso'
					 SELECT @MsgRetorno				 				  	    
			END 
		ELSE 
	       IF @ems_status = 3 and @ems_data_cancelamento < (SELECT ems_inicio_vigencia FROM emissao_voucher WHERE ems_id = @ems_id)
			   BEGIN
					 SET @MsgRetorno = 'Data de DESCONTINUIDADE não pode ser inferior à data de início de vigência do Voucher' 
					 SELECT @MsgRetorno
			   END 
		   ELSE
			   IF @ems_status = 3 and @ems_data_cancelamento >= (SELECT ems_inicio_vigencia FROM emissao_voucher WHERE ems_id = @ems_id)
				   BEGIN
						 SELECT @descricao = 'Atualizacao realizada com sucesso no voucher : ' + convert(char(10),@ems_id)
			    
						 exec spInserirLog
							  @descricao ,
							  @data ,
							  @idUsuario ,
							  @deletado 
							  
							 DECLARE @QtdDiarias int, @NovoPremio numeric(10,2) 
							 SET @QtdDiarias = DATEDIFF(DAY,@ems_inicio_vigencia,@ems_data_cancelamento)
					         SET @NovoPremio = @QtdDiarias * 7.33
					         
							  
							 UPDATE emissao_voucher SET     
							 ems_nome_passageiro  = @ems_nome_passageiro,
							 ems_cpf              = @ems_cpf,	
							 ems_nr_voucher       = @ems_nr_voucher,
							 ems_diaria           = @ems_diaria,
							 ems_destino          = @ems_destino,
							 ems_inicio_vigencia  = @ems_inicio_vigencia,
							 ems_fim_vigencia     = @ems_fim_vigencia,
							 ems_premio_licitacao = @NovoPremio,
							 ems_premio_anterior  = @ems_premio_licitacao, 
							 ems_status           = @ems_status,
							 ems_deletado         = @ems_deletado,
							 ems_data_cancelamento= @ems_data_cancelamento
							 WHERE    
							 ems_id               = @ems_id

							 SET @MsgRetorno = 'Voucher descontinuado com sucesso'
							 SELECT @MsgRetorno								 
				   END 
			   ELSE		   
					BEGIN   
						 SELECT @descricao = 'Atualizacao realizada com sucesso no voucher : ' + convert(char(10),@ems_id)
					    
						 exec spInserirLog
							  @descricao ,
							  @data ,
							  @idUsuario ,
							  @deletado 
					   
						 UPDATE emissao_voucher SET     
						 ems_nome_passageiro  = @ems_nome_passageiro,
						 ems_cpf              = @ems_cpf,	
						 ems_nr_voucher       = @ems_nr_voucher,
						 ems_diaria           = @ems_diaria,
						 ems_destino          = @ems_destino,
						 ems_inicio_vigencia  = @ems_inicio_vigencia,
						 ems_fim_vigencia     = @ems_fim_vigencia,
						 ems_premio_licitacao = @ems_premio_licitacao,
						 ems_status           = @ems_status,
						 ems_deletado         = @ems_deletado
						 WHERE    
						 ems_id               = @ems_id
						 
						SET @MsgRetorno = 'Registro alterado com sucesso' 			 
						
						SELECT @MsgRetorno
					END 
  END    
ELSE
----------------------------------------    
-- Exclusao
----------------------------------------    
if @flagInsert = 'E'
  BEGIN   
	     
	 SELECT @descricao = 'Exclusao realizada com sucesso do voucher : ' + convert(char(10),@ems_id)
    
     exec spInserirLog
	      @descricao ,
		  @data ,
		  @idUsuario ,
		  @deletado 
   
     UPDATE emissao_voucher SET     
     ems_deletado         = 1
     WHERE    
     ems_id               = @ems_id
     
     SET @MsgRetorno = 'Registro excluido com sucesso' 	
     
	 SELECT @MsgRetorno     
     
  END    
*/  