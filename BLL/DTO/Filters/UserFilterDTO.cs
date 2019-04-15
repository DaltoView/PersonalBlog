using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Filters
{
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
