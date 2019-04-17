using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Filters
{
    /// <summary>
    /// Represents a collection of filter parameters for postService.
    /// </summary>
    public class PostFilterDTO
    {
        public IEnumerable<string> Tags { get; set; }
        public string Search { get; set; }
        public DateTime? PostFrom { get; set; }
        public DateTime? PostTo { get; set; }
        public string SortOrder { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}