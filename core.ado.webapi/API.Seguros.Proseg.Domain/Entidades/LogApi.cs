using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Seguros.Proseg.Domain.Entidades
{
    [Table("LogApi", Schema = "dbo")]
    public class LogApi
    {
        [Key]
        public int Log_ID { get; set; }
        public int Log_Usr_Id { get; set; }
        public int Log_Svc_Id { get; set; }
        public int Log_Sta_Id { get; set; }
        public string Log_IpConsumidor { get; set; }
        public string Log_ComplementoServico { get; set; }
        public DateTime Log_DataAcesso { get; set; }
    }
}
