namespace BLL.DTO.Filters
{
    /// <summary>
    /// Represents a collection of filter parameters for userService.
    /// </summary>
    public class UserFilterDTO
    {
        public string Search { get; set; }
        public string Role { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public string SortOrder { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
