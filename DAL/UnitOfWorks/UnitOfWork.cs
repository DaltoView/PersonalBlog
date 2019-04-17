using DAL.EFContext;
using DAL.Identity;
using DAL.Interfaces;
using DAL.Repositories;
using System;

namespace DAL.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _context;
        private IAuthorRepository _authorRepository;
        private ICommentRepository _commentRepository;
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;
        private UserManager _userManager;
        private RoleManager _roleManager;

        public UnitOfWork(string connectionString)
        {
            _context = new BlogContext(connectionString);
        }

        public IAuthorRepository Authors
        {
            get { return _authorRepository ?? (_authorRepository = new AuthorRepository(_context)); }
        }

        public ICommentRepository Comments
        {
            get { return _commentRepository ?? (_commentRepository = new CommentRepository(_context)); }
        }

        public IPostRepository Posts
        {
            get { return _postRepository ?? (_postRepository = new PostRepository(_context)); }
        }

        public ITagRepository Tags
        {
            get { return _tagRepository ?? (_tagRepository = new TagRepository(_context)); }
        }

        public UserManager UserManager
        {
            get { return _userManager ?? (_userManager = UserManager.Create(_context)); }
        }

        public RoleManager RoleManager
        {
            get { return _roleManager ?? (_roleManager = RoleManager.Create(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
