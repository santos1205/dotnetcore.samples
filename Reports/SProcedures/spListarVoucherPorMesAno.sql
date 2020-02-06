USE [CorretoraPremium]
GO
/****** Object:  StoredProcedure [dbo].[spListarVoucherPorMesAno]    Script Date: 05/14/2018 14:08:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[spListarVoucherPorMesAno]
(
@dataInicio date,
@dataFinal date
)

AS
BEGIN	
	SELECT * from dbo.emissao_voucher
	--m 14/05/2018
	--BugId: 8927
	--where ems_inicio_vigencia between @dataInicio and @dataFinal and ems_deletado = 0
	where ems_dt_cadastro between @dataInicio and @dataFinal and ems_deletado = 0
	order by ems_id desc
END


