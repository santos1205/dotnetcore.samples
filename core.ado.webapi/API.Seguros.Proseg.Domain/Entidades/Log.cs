using System;

namespace API.Seguros.Proseg.Domain.Entidades
{
    public class Log
    {
        public int Id { get; set; }
        public string LogEndpoint { get; set; }
        public string LogLoggedUser { get; set; }
        public string LogParam { get; set; }
        public DateTime LogDate { get; set; }
    }
}
