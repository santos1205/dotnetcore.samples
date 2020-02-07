namespace BaseAccess.VModels
{
    public class ContatoVM
    {
        public int IdContato { get; set; }
        public string Cpf { get; set; }     // cpf do segurado / dependente.
        public int? IdTitular { get; set; }
        public int? IdDependente { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
    }
}
