using System.Threading.Tasks;
using WSNetSegurancaWebReference;

namespace API.Seguros.Proseg.Domain.Entidades
{
    public class ControlMs
    {
        private string _Msg;
        public string Msg
        {
            get => _Msg;
            set => _Msg = value;
        }

        private WSNetSegurancaWebReference.WsSegurancaSoap _ObjSecurity;

        public WSNetSegurancaWebReference.WsSegurancaSoap ObjSecurity
        {
            get => _ObjSecurity;
            set => _ObjSecurity = value;
        }

        public AuthenticationSoapHeader AuthenticationSoapHeaderControl { get; set; }

        public bool ValidalAcessoMs(string sUsuario, string sSenha, string sOrigem)
        {

            //Objeto proxy de Usuário, sendo instanciado pelo consumo do método logar
            Task<UsuarioDto> oUsuario = ObjSecurity.LogarAsync(sUsuario, sSenha, sOrigem);

            if (oUsuario.Result.Autorizado == true)
            {
                //Criação da credencial para ser utilizada nos demais métodos
                AuthenticationSoapHeader objHeaderWsSeguranca = new AuthenticationSoapHeader
                {
                    Token = oUsuario.Result.Token,
                    User = oUsuario.Result.UserName
                };

                //Passagem da credencial para o objeto proxy do Web Service
                AuthenticationSoapHeaderControl = objHeaderWsSeguranca;

                //Alimenta o Objeto na propriedade
                ObjSecurity.AuthenticationSoapHeaderControl = AuthenticationSoapHeaderControl;
                
                return true;
            }
            else
            {
                Msg = oUsuario.Result.MensagemLogon.ToString();
                return false;
            }
        }
    }
}
