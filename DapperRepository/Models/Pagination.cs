namespace DapperRepository.Models
{
    public class Pagination
    {
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
