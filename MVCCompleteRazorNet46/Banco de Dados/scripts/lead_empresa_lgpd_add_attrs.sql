use QB
ALTER TABLE [QB].[dbo].[lead_empresa_lgpd] ADD lead_hash varchar(200) null
ALTER TABLE [dbo].[lead_empresa_lgpd] ADD etapa_chat int null
ALTER TABLE [dbo].[lead_empresa_lgpd] ADD lgpd_armazena_dados char(3) null
ALTER TABLE [dbo].[lead_empresa_lgpd] ADD lgpd_situacao_ti  char(1) null
ALTER TABLE [dbo].[lead_empresa_lgpd] ADD lgpd_situacao_juridico char(1) null
ALTER TABLE [dbo].[lead_empresa_lgpd] ADD lgpd_id_empresa int null