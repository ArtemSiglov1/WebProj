using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Service.Models
{
    public class BaseResponse
    {
        /// <summary>
        /// успешно
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// соо об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public BaseResponse() { }
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="isSuccess">успех</param>
        /// <param name="errorMessage">соо об ошибке </param>
        public BaseResponse(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }
}
