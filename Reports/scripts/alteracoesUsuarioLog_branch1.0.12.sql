use CorretoraPremium
-- retira a FK da tbl log.
ALTER TABLE dbo.Log
DROP CONSTRAINT [FK_ID_USR]

-- adicionar campo data na tbl Log.
ALTER TABLE dbo.Log
Add [log_data] [datetime] NULL

-- adicionar campo aprovado.
ALTER TABLE dbo.Usuario
Add usr_aprovado bit NULL


