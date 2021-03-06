USE [Multiseguros]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[stpAuCliCpf]'))
DROP PROCEDURE [dbo].stpAuCliCpf

GO
/****** Object:  StoredProcedure [dbo].[stpAuCliCpf]    Script Date: 06/04/2021 12:46:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[stpAuCliCpf]
                 @Chave char (16)
AS
SELECT Cli_Cpf_Cgc, Cli_Nome, ItaPlaca, DmoDescricao,
Cli_Email, CONTR.CtaDataContrato, CONTR.CtaDataFim
, CtaApolice, Seg_RazaoSoc as NomeSeguradora, '' as ContatoCorr 
, CtaPremioTotal
--, Cec_Endereco
--, ContDDD + ' ' + ContFone as NumTel
FROM CLIENTE CLI
INNER JOIN AuContratoAuto CONTR ON CLI.CliCodigo = CONTR.CliCodigo
INNER JOIN AuItemAuto IAuto ON CONTR.CtaProposta = IAuto.CtaProposta
INNER JOIN AuDescricaoModelo MAuto ON MAuto.DmoCodigo = IAuto.DmoCodigo
INNER JOIN ClienteEndComercial EndCom ON CLI.CliCodigo = EndCom.CliCodigo
INNER JOIN ClienteContatos CliContato ON CLI.CliCodigo = CliContato.CliCodigo
LEFT JOIN Seguradora SEG ON SEG.Seg_CodSusep = CONTR.CtaNumSusep
WHERE Cli_Cpf_Cgc = @Chave
AND CtaStatus = 0
group by Cli_Cpf_Cgc, Cli_Nome, ItaPlaca, DmoDescricao, 
Cli_Email, CONTR.CtaDataContrato, CONTR.CtaDataFim,
CtaApolice, Seg_RazaoSoc, CtaPremioTotal
--, Cec_Endereco
--, ContDDD + ' ' + ContFone
order by cli_Nome, CtaDataContrato

