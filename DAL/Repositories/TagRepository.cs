using DAL.EFContext;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(BlogContext context)
            : base(context)
        {
        }
    }
}
