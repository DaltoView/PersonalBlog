using DAL.EFContext;
using DAL.Entities;
using DAL.Entities.Filters;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(BlogContext context)
            : base(context)
        {
        }

        public void CreatePost(Post post)
        {
            var tags = post.Tags.ToArray();

            for (int i = 0; i < tags.Length; i++)
            {
                tags[i].Name = tags[i].Name.ToLower();
                var tagName = tags[i].Name;
                var tag = context.Tags.FirstOrDefault(p => p.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase));

                if (tag != null)
                {
                    tags[i] = tag;
                }
            }

            post.Tags = tags;

            context.Posts.Add(post);
        }

        public IEnumerable<Post> GetAllPosts(PostFilter postFilter)
        {
            var posts = context.Posts.AsQueryable();

            if(postFilter != null)
            {
                if(postFilter.Tags != null && postFilter.Tags.Any())
                {
                    var postsAll = posts;
                    posts = posts.Where(p => p.Tags.Any(t => t.Name.ToLower() == postFilter.Tags.FirstOrDefault().ToLower()));
                    foreach (var tag in postFilter.Tags.Skip(1))
                    {
                        posts = posts.Union(postsAll.Where(p => p.Tags.Any(t => t.Name.ToLower() == tag.ToLower())));
                    }
                }

                if (!string.IsNullOrEmpty(postFilter.Search))
                {
                    var postsAll = posts;
                    posts = posts.Where(p => p.Title.ToLower().Contains(postFilter.Search.ToLower()));
                    posts = posts.Union(postsAll.Where(p => p.Article.ToLower().Contains(postFilter.Search.ToLower())));
                }

                if (postFilter.PostFrom.HasValue)
                    posts = posts.Where(p => p.PostDate >= postFilter.PostFrom);

                if (postFilter.PostTo.HasValue)
                    posts = posts.Where(p => p.PostDate <= postFilter.PostTo);

                if(postFilter.SortOrder != null)
                {
                    switch (postFilter.SortOrder.ToLower())
                    {
                        case "title_asc":
                            posts = posts.OrderBy(p => p.Title);
                            break;
                        case "title_desc":
                            posts = posts.OrderByDescending(p => p.Title);
                            break;
                        case "postdate_asc":
                            posts = posts.OrderBy(p => p.PostDate);
                            break;
                        case "postdate_desc":
                            posts = posts.OrderByDescending(p => p.PostDate);
                            break;
                        default:
                            posts = posts.OrderByDescending(p => p.PostDate);
                            break;
                    }
                }

                if (postFilter.Skip.HasValue)
                    posts = posts.Skip(postFilter.Skip.Value);

                if (postFilter.Take.HasValue)
                    posts = posts.Take(postFilter.Take.Value);
            }

            return posts.ToList();
        }

        public void EditPost(Post post)
        {
            var existingPost = context.Posts.Find(post.Id);

            if (existingPost != null)
            {

                existingPost.Title = post.Title;
                existingPost.Article = post.Article;
                existingPost.EditDate = post.EditDate;

                // Delete children
                foreach (var existingTag in existingPost.Tags.ToList())
                {
                    if (!post.Tags.Any(c => c.Name.Equals(existingTag.Name, StringComparison.CurrentCultureIgnoreCase)))
                        existingPost.Tags.Remove(existingTag);
                }

                foreach (var tag in post.Tags)
                {
                    var existingTag = existingPost.Tags.FirstOrDefault(p => p.Name.Equals(tag.Name, StringComparison.CurrentCultureIgnoreCase));

                    if (existingTag != null)
                    {
                        // Update child
                        //existingTag.Name = tag.Name;
                    }
                    else
                    {
                        // Insert child
                        var existingTagGlobal = context.Tags.FirstOrDefault(p => p.Name.Equals(tag.Name, StringComparison.CurrentCultureIgnoreCase));
                        if (existingTagGlobal != null)
                        {
                            existingPost.Tags.Add(existingTagGlobal);
                        }
                        else
                        {
                            existingPost.Tags.Add(tag);
                        }
                    }
                }
            }
        }
    }
}