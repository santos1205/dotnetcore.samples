using Common;
using MovileWeb.DataAccess;
using MovileWeb.Repositories.Interfaces;
using MovileWeb.Services.Interfaces;
using System;

namespace MovileWeb.Services
{
    public class ProdutosService : IProdutosService
    {

        private readonly IClienteRepository _iClienteRepository;        

        public ProdutosService(IClienteRepository iClienteRepository)
        {
            _iClienteRepository = iClienteRepository;            
        }

        public void SalvarCliente(Cliente Cliente)
        {
            try
            {
                Cliente.CliDate = DateTime.Now;
                _iClienteRepository.Insert(Cliente);

                //SendMail(Cliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }        

    }
}
