using MovileWeb.DataAccess;
using MovileWeb.Repositories.Interfaces;
using MovileWeb.Services.Interfaces;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace MovileWeb.Services
{
    public class CommonService : ICommonService
    {

        private readonly IEnderecoRepository _iEnderecoRepository;

        CEPContext _dbContext;
        public CommonService(CEPContext dbContext, IEnderecoRepository iEnderecoRepository)
        {
            _dbContext = dbContext;
            _iEnderecoRepository = iEnderecoRepository;
        }

        public Endereco ConsultarEnderecoPorCep(string Cep)
        {
            try
            {
                Cep = Regex.Replace(Cep, "[^0-9,]", "");
                return _iEnderecoRepository.ConsultarEnderecoPorCep(Cep);
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
    }
}
