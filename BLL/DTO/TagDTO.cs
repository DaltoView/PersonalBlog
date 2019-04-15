using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class TagDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<PostDTO> Posts { get; set; }
    }
}
