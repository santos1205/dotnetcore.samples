USE [CorretoraPremium]
GO
/****** Object:  StoredProcedure [dbo].[spListarPeriodoVoucherPorAno]    Script Date: 05/14/2018 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spListarPeriodoVoucherPorAno]
(
@dataInicio date,
@dataFinal date
)

AS
BEGIN	
	--m 14/05/2018
	--BugId: 8927	
	--SELECT distinct DATEPART(MM, ems_inicio_vigencia) as ems_meses_disponiveis from dbo.emissao_voucher
	SELECT distinct DATEPART(MM, ems_dt_cadastro) as ems_meses_disponiveis from dbo.emissao_voucher
	--m 14/05/2018
	--BugId: 8927	
	--where ems_inicio_vigencia between @dataInicio and @dataFinal	
	where ems_dt_cadastro between @dataInicio and @dataFinal	
END

	