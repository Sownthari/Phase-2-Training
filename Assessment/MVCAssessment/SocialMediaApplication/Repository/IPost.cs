using SocialMediaApplication.Models;

namespace SocialMediaApplication.Repository
{
    public interface IPost
    {
        IEnumerable<Post> GetAllPosts();
        Post GetPostById(int id);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);
        IEnumerable<Post> GetPostsByUserId(int userId);

    }
}
