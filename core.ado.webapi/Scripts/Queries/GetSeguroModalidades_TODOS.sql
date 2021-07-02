SELECT Cliente.Cli_Nome, Cliente.Cli_Cpf_Cgc, RsContratoImovel.CniNumero, RsBens.BemDescricao, 
RsLocalRisco.LriEndereco, Seguradora.Seg_RazaoSoc, RsContratoImovel.CniStatus, 
ModalidadeSeguro.Mse_CodSeguro, ModalidadeSeguro.Mse_DescMod, Seguradora.Seg_CodSusep
FROM         RsContratoImovel INNER JOIN
RsBens ON RsContratoImovel.CniNumero = RsBens.CniNumero INNER JOIN
RsLocalRisco ON RsContratoImovel.CniNumero = RsLocalRisco.CniNumero AND RsBens.CniNumero = RsLocalRisco.CniNumero AND RsBens.LriItem = RsLocalRisco.LriItem INNER JOIN
Seguradora ON RsContratoImovel.SegCodSusep = Seguradora.Seg_CodSusep INNER JOIN
Cliente ON RsContratoImovel.CliCodigo = Cliente.CliCodigo INNER JOIN
ModalidadeSeguro ON RsContratoImovel.MseCodigo = ModalidadeSeguro.Mse_CodSeguro