USE [Multiseguros]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[stpRsCliCpf]'))
DROP PROCEDURE [dbo].[stpRsCliCpf]

GO
/****** Object:  StoredProcedure [dbo].[stpRsCliCpf]    Script Date: 06/04/2021 12:46:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[stpRsCliCpf]
	@Chave char (16),
	@CodModalidadeSeg int
AS

IF @CodModalidadeSeg = 10969
BEGIN
	SELECT Cliente.Cli_Cpf_Cgc, Cliente.Cli_Nome, Cliente.Cli_Email,
	RsContratoImovel.CniInicioVigencia, RsContratoImovel.CniFimVigencia,
	RsContratoImovel.CniApolice, RsContratoImovel.CniPremioTotal, Seguradora.Seg_RazaoSoc, 
	Seguradora.Seg_CodDDD + ' ' + Seguradora.Seg_NumTel AS seguradora_contato, 
	Cliente.Cli_Endereco, Cliente.Cli_CodDDD + ' ' + Cliente.Cli_NumTel AS cli_contato, 
	RsBens.BemDescricao,  RsLocalRisco.LriEndereco, RsContratoImovel.CniStatus, 
	ModalidadeSeguro.Mse_CodSeguro, ModalidadeSeguro.Mse_DescMod, Seguradora.Seg_CodSusep
	FROM RsContratoImovel LEFT JOIN
	RsBens ON RsContratoImovel.CniNumero = RsBens.CniNumero LEFT JOIN
	RsLocalRisco ON RsContratoImovel.CniNumero = RsLocalRisco.CniNumero AND RsBens.CniNumero = RsLocalRisco.CniNumero AND RsBens.LriItem = RsLocalRisco.LriItem INNER JOIN
	Seguradora ON RsContratoImovel.SegCodSusep = Seguradora.Seg_CodSusep INNER JOIN
	Cliente ON RsContratoImovel.CliCodigo = Cliente.CliCodigo INNER JOIN
	ModalidadeSeguro ON RsContratoImovel.MseCodigo = ModalidadeSeguro.Mse_CodSeguro
	WHERE ModalidadeSeguro.Mse_CodSeguro IN (@CodModalidadeSeg, 11369)		--@CodModalidadeSeg   --10171	--@CodModalidadeSeg	
	AND Cliente.Cli_Cpf_Cgc = @Chave
	AND RsContratoImovel.CniStatus = 0
END
ELSE IF @CodModalidadeSeg = 10171
BEGIN
	SELECT Cliente.Cli_Cpf_Cgc, Cliente.Cli_Nome, Cliente.Cli_Email,
	RsContratoImovel.CniInicioVigencia, RsContratoImovel.CniFimVigencia,
	RsContratoImovel.CniApolice, RsContratoImovel.CniPremioTotal, Seguradora.Seg_RazaoSoc, 
	Seguradora.Seg_CodDDD + ' ' + Seguradora.Seg_NumTel AS seguradora_contato, 
	Cliente.Cli_Endereco, Cliente.Cli_CodDDD + ' ' + Cliente.Cli_NumTel AS cli_contato, 
	RsBens.BemDescricao,  RsLocalRisco.LriEndereco, RsContratoImovel.CniStatus, 
	ModalidadeSeguro.Mse_CodSeguro, ModalidadeSeguro.Mse_DescMod, Seguradora.Seg_CodSusep
	FROM RsContratoImovel LEFT JOIN
	RsBens ON RsContratoImovel.CniNumero = RsBens.CniNumero LEFT JOIN
	RsLocalRisco ON RsContratoImovel.CniNumero = RsLocalRisco.CniNumero AND RsBens.CniNumero = RsLocalRisco.CniNumero AND RsBens.LriItem = RsLocalRisco.LriItem INNER JOIN
	Seguradora ON RsContratoImovel.SegCodSusep = Seguradora.Seg_CodSusep INNER JOIN
	Cliente ON RsContratoImovel.CliCodigo = Cliente.CliCodigo INNER JOIN
	ModalidadeSeguro ON RsContratoImovel.MseCodigo = ModalidadeSeguro.Mse_CodSeguro
	WHERE ModalidadeSeguro.Mse_CodSeguro = @CodModalidadeSeg   --10171	--@CodModalidadeSeg
	AND Seguradora.Seg_CodSusep = 2798
	AND Cliente.Cli_Cpf_Cgc = @Chave
	AND RsContratoImovel.CniStatus = 0
END
ELSE	
BEGIN
	SELECT Cliente.Cli_Cpf_Cgc, Cliente.Cli_Nome, Cliente.Cli_Email,
	RsContratoImovel.CniInicioVigencia, RsContratoImovel.CniFimVigencia,
	RsContratoImovel.CniApolice, RsContratoImovel.CniPremioTotal, Seguradora.Seg_RazaoSoc, 
	Seguradora.Seg_CodDDD + ' ' + Seguradora.Seg_NumTel AS seguradora_contato, 
	Cliente.Cli_Endereco, Cliente.Cli_CodDDD + ' ' + Cliente.Cli_NumTel AS cli_contato, 
	RsBens.BemDescricao,  RsLocalRisco.LriEndereco, RsContratoImovel.CniStatus, 
	ModalidadeSeguro.Mse_CodSeguro, ModalidadeSeguro.Mse_DescMod, Seguradora.Seg_CodSusep
	FROM RsContratoImovel LEFT JOIN
	RsBens ON RsContratoImovel.CniNumero = RsBens.CniNumero LEFT JOIN
	RsLocalRisco ON RsContratoImovel.CniNumero = RsLocalRisco.CniNumero AND RsBens.CniNumero = RsLocalRisco.CniNumero AND RsBens.LriItem = RsLocalRisco.LriItem INNER JOIN
	Seguradora ON RsContratoImovel.SegCodSusep = Seguradora.Seg_CodSusep INNER JOIN
	Cliente ON RsContratoImovel.CliCodigo = Cliente.CliCodigo INNER JOIN
	ModalidadeSeguro ON RsContratoImovel.MseCodigo = ModalidadeSeguro.Mse_CodSeguro
	WHERE ModalidadeSeguro.Mse_CodSeguro = @CodModalidadeSeg   --10171	--@CodModalidadeSeg	
	AND Cliente.Cli_Cpf_Cgc = @Chave
	AND RsContratoImovel.CniStatus = 0
END

-- Seguro PET - Mse_CodSeguro = 11253
-- Seguro Residencial - Mse_CodSeguro = 10114
-- Seguro Viagem - Mse_CodSeguro = 10969 e 11369
-- BIKE - PATRIMONIAL RISCOS DIVERSOS - 10171 (FILTRAR PELA SEGURADORA ARGO - 2798)