using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Service.Models
{
    public class PageResponse
    {
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="page">страница</param>
        /// <param name="pageSize">размер страницы</param>
        /// <param name="count">количество страниц</param>
        public PageResponse(int page, int pageSize, int count)
        {
            Page = page;
            PageSize = pageSize;
            Count = count;

        }
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public PageResponse() { }
        /// <summary>
        /// страница
        /// </summary>
        public int Page { get; set; } //номер страницы
        /// <summary>
        /// размер страницы
        /// </summary>
        public int PageSize { get; set; }// количество записей на странице
        /// <summary>
        /// количество листов
        /// </summary>
        public int Count { get; set; } // общее количество записей всего

    }
}
