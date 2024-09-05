using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SocialMediaApplication.Models;
using SocialMediaApplication.Repository;

namespace SocialMediaApplication.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;

        private readonly IUser _userService;

        public PostController(IPost postService, IUser userService)
        {
            _postService = postService;
            _userService = userService;
        }


        // GET: PostController
        public ActionResult Index()
        {
            
            return View(_postService.GetAllPosts());
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            
            return View(_postService.GetPostById(id));
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            ViewBag.Users = new SelectList(_userService.GetAllUsers(), "UserId", "UserName");
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            try
            {
                _postService.AddPost(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Users = new SelectList(_userService.GetAllUsers(), "UserId", "UserName");
            
            return View(_postService.GetPostById(id));
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post post)
        {
            try
            {
                //ViewBag.Users = new SelectList(_userService.GetAllUsers(), "UserId", "UserName", post.UserId);
                _postService.UpdatePost(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_postService.GetPostById(id));
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                _postService.DeletePost(post);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
