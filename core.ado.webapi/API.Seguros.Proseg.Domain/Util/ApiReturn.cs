using System;

namespace API.Seguros.Proseg.Domain.Util
{
    public class ApiReturn
    {
        public static object ApiReturnObjectException(bool sucess, string errorCode, string errorMessage, Exception inner)
        {
            return new
            {
                Sucess = sucess,
                ErrorCode = errorCode,
                ErrorMessage = errorMessage,
                InnerException = inner
            };
        }
        public static object ApiReturnObject(bool sucess, string errorCode, string errorMessage, string campo = null)
        {
            return new
            {
                Sucess = sucess,
                Code = errorCode,
                Message = campo != null ? string.Format(errorMessage, campo) : errorMessage
            };
        }
    }
}
