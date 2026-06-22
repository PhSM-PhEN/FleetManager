namespace FleetManager.Communication.Responses
{
    public class ResponsePaginatedJson<T>
    {
        public List<T> Data { get; set; } = [];
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)PageNumber / TotalCount);
    }
}
