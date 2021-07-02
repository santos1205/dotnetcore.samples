SELECT Cliente.Cli_Nome, Cliente.Cli_Cpf_Cgc, RsContratoImovel.CniNumero, RsBens.BemDescricao, 
RsLocalRisco.LriEndereco, Seguradora.Seg_RazaoSoc, RsContratoImovel.CniStatus, 
ModalidadeSeguro.Mse_CodSeguro, ModalidadeSeguro.Mse_DescMod, Seguradora.Seg_CodSusep
FROM         RsContratoImovel LEFT JOIN
RsBens ON RsContratoImovel.CniNumero = RsBens.CniNumero LEFT JOIN
RsLocalRisco ON RsContratoImovel.CniNumero = RsLocalRisco.CniNumero AND RsBens.CniNumero = RsLocalRisco.CniNumero AND RsBens.LriItem = RsLocalRisco.LriItem INNER JOIN
Seguradora ON RsContratoImovel.SegCodSusep = Seguradora.Seg_CodSusep INNER JOIN
Cliente ON RsContratoImovel.CliCodigo = Cliente.CliCodigo INNER JOIN
ModalidadeSeguro ON RsContratoImovel.MseCodigo = ModalidadeSeguro.Mse_CodSeguro