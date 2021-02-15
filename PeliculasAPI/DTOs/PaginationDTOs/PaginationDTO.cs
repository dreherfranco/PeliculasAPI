using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.PaginationDTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;

        private int recordsPerPage = 10;
        private readonly int maximunRecordsPerPage = 50;

        public int RecordsPerPage
        {
            get => recordsPerPage;
            set
            {
                recordsPerPage = (value > maximunRecordsPerPage) ? maximunRecordsPerPage : value;
            }
        }
    }
}
