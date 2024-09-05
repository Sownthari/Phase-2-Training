using Microsoft.EntityFrameworkCore;
using SocialMediaApplication.Models;
using SocialMediaApplication.Repository;

namespace SocialMediaApplication.Services
{
    public class PostService : IPost
    {
        private readonly SocialMediaContext _context;

        public PostService(SocialMediaContext context)
        {
            _context = context;
        }

        public void AddPost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.Include(u => u.user).ToList();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.Include(u => u.user).FirstOrDefault( x => x.PostId == id) ?? new Post();
        }

        public IEnumerable<Post> GetPostsByUserId(int userId)
        {
            return _context.Posts.Where(post => post.UserId == userId).ToList();
        }

        public void UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
    }
}
