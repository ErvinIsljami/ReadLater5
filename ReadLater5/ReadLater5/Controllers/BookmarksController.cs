using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadLater5.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Controllers
{
    public class BookmarksController : Controller
    {
        IBookmarkService _bookmarkService;
        ICategoryService _categoryService;
        UserManager<IdentityUser> _userManager;

        public BookmarksController(IBookmarkService bookmarkService, ICategoryService categoryService, UserManager<IdentityUser> userManager)
        {
            _bookmarkService = bookmarkService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        //GET: Bookmarks
        public IActionResult Index()
        {
            List<Bookmark> bookmarks = _bookmarkService.GetBookmarks(_userManager.GetUserId(this.User));
            return View(bookmarks);
        }

        //GET: Bookmarks/Details/1
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
            
            Bookmark bookmark = _bookmarkService.GetBookmark(id);
            if(bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }

            return View(bookmark);
        }

        //GET: Bookmarks/Create
        public IActionResult Create()
        {
            List<Category> categories = _categoryService.GetCategories(_userManager.GetUserId(this.User));
            List<SelectListItem> selectLists = new List<SelectListItem>();

            categories.ForEach(x => selectLists.Add(new SelectListItem(x.Name, x.Name)));
            ViewBag.CategoryNames = selectLists;

            return View();
        }

        //POST: Bookmarks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateBookmarkModel bookmarkModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookmarkModel);
            }

            Category category = _categoryService.GetCategory(bookmarkModel.CategoryName);
            if(category == null)
            {
                _categoryService.CreateCategory(category);
            }
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            Bookmark bookmark = new Bookmark()
            {
                ShortDescription = bookmarkModel.ShortDescription,
                URL = bookmarkModel.URL,
                CreateDate = bookmarkModel.CreateDate,
                CategoryId = category.ID,
                OwnerId = _userManager.GetUserId(currentUser)
            };

            _bookmarkService.CreateBookmark(bookmark);
            return RedirectToAction("Index");
        }

        //GET: Bookmarks/Edit/1
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }

            Bookmark bookmark = _bookmarkService.GetBookmark(id);
            if(bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }

            return View(bookmark);
        }

        //POST: Bookmarks/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Bookmark bookmark)
        {
            if(ModelState.IsValid)
            {
                _bookmarkService.UpdateBookmark(bookmark);
                return RedirectToAction("Index");
            }

            return View(bookmark);
        }

        //GET: Bookmarsk/Delete/1
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }

            Bookmark bookmark = _bookmarkService.GetBookmark(id);
            if (bookmark == null)
            {
                return new StatusCodeResult(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }

            return View(bookmark);
        }

        //POST: Bookmark/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Bookmark bookmark = _bookmarkService.GetBookmark(id);
            _bookmarkService.DeleteBookmark(bookmark);

            return RedirectToAction("Index");
        }

    }
}
