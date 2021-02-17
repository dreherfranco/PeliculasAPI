using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs.PaginationDTOs
{
    public class MovieFilterDTO
    {
        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 10;
        public PaginationDTO Pagination
        {
            get 
            { 
                return new PaginationDTO() { Page = Page, RecordsPerPage = RecordsPerPage }; 
            }
        }
        public string Title { get; set; }
        public int GenderId { get; set; }
        public bool InTheaters { get; set; }
        public bool NextReleases { get; set; }
        public string FieldToSort { get; set; }
        public bool AscendingOrder { get; set; }
    }
}
