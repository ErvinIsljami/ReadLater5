using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReadLater5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BmarksController : ControllerBase
    {
        IBookmarkService _bookmarkService;
        UserManager<IdentityUser> _userManager;
        public BmarksController(IBookmarkService bookmarkService, UserManager<IdentityUser> userManager)
        {
            _bookmarkService = bookmarkService;
            _userManager = userManager;
        }
        // GET: api/<BmarksController>
        [HttpGet]
        [Authorize]
        public IEnumerable<Bookmark> Get()
        {
            return _bookmarkService.GetBookmarks(_userManager.GetUserId(this.User));
        }

        // GET api/<BmarksController>/5
        [HttpGet("{id}")]
        public Bookmark Get(int id)
        {
            return _bookmarkService.GetBookmark(id);
        }
    }
}
