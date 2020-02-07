namespace BaseAccess.VModels
{
    public class UsuarioVM
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string DataCadastro { get; set; }
        public string Aprovado { get; set; }
        public int NvlAcesso { get; set; }
        public int MaxCount { get; set; }
        public string MsgErro { get; set; }
        public string MsgSenha { get; set; }

    }
}
