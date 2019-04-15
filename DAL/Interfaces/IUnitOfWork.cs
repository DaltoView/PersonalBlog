using DAL.Identity;
using System;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAuthorRepository Authors { get; }
        ICommentRepository Comments { get; }
        IPostRepository Posts { get; }
        ITagRepository Tags { get; }
        UserManager UserManager { get; }
        RoleManager RoleManager { get; }
        void Save();
    }
}
