using Common;
using MovileWeb.DataAccess;
using MovileWeb.Repositories.Interfaces;
using MovileWeb.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovileWeb.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _iPedidoRepository;
        private readonly IPagamentoRepository _iPagamentoRepository;

        public PedidoService(IPedidoRepository iPedidoRepository, IPagamentoRepository iPagamentoRepository)
        {
            _iPedidoRepository = iPedidoRepository;
            _iPagamentoRepository = iPagamentoRepository;
        }

        public void SalvarPedido(Pedido Pedido)
        {
            try
            {
                Pedido.PedCli.CliDate = Pedido.PedData = DateTime.Now;
                // Recuperar o pagamento de acordo com idplano e o tpPagamento
                Pagamento Pagamento = _iPagamentoRepository.Consultar(Pedido.PedCli.IdPlano, Pedido.PedCli.TpPagamento);
                Pedido.PedPagId = Pagamento.PagId;
                // Caso haja débito em conta gravar banco e seus dados conta e agência
                DebitoEmConta NovoDebitoEmConta = new DebitoEmConta
                {
                    DebBanId = Pedido.PedCli.Banco,
                    DebAgencia = Pedido.PedCli.Agencia,
                    DebConta = Pedido.PedCli.Conta
                };
                Pedido.DebitoEmConta.Add(NovoDebitoEmConta);    

                _iPedidoRepository.Insert(Pedido);

                SendMail(Pedido.PedCli);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void SendMail(Cliente cliente)
        {
            try
            {
                Email email = new Email();

                string html = "<html>" +
                    "<h2>Seguro contratado</h2><br/>" +
                    "Plano: " + cliente.Plano + "<br/>" +
                    "<h2>Dados do segurado</h2><br/>" +
                    ((cliente.CliCpfCnpj.Replace(".", "").Replace("-", "").Length == 11) ? "Nome do segurado: " : "Razão social: ") + cliente.CliNome + "<br/>" +
                    ((cliente.CliCpfCnpj.Replace(".", "").Replace("-", "").Length == 11) ? "Cpf: " : "Cnpj: ") + cliente.CliCpfCnpj + "<br/>" +
                    "Email: " + cliente.CliEmail + "<br/>" +
                    "Telefone: " + cliente.CliTelefone + "<br/>" +
                    "<h2>Endereço</h2><br/>" +
                    "Cep: " + cliente.CliCep + "<br/>" +
                    "Estado: " + cliente.CliEndUf + "<br/>" +
                    "Cidade: " + cliente.CliEndCidade + "<br/>" +
                    "Bairro: " + cliente.CliEndBairro + "<br/>" +
                    "Endereço: " + cliente.CliEndLogradouro + "<br/>" +
                    "Número: " + cliente.CliEndNum + "<br/>" +
                    "Complemento: " + cliente.CliEndComplemento + "<br/>" +
                    "<h2>Forma de pagamento</h2><br/>" +
                    "Pagamento: " + ((cliente.TpPagamento == 1) ? "Débito em conta" : (cliente.TpPagamento == 2) ? "Cartão de crédito" : (cliente.TpPagamento == 3) ? "Boleto" : (cliente.TpPagamento == 4) ? "Boleto com primeira parcela no cartão de crédito" : string.Empty) +
                    "</html>";

                email.SendEmail("vendas-movilepay@proseg.com.br", "marco.silveira@proseg.com.br", "Dados recebidos", html);

            }
            catch { }
        }
    }
}
