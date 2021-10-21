using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;

namespace Services
{
    public interface IBookmarkService
    {
        Bookmark CreateBookmark(Bookmark bookmark);
        List<Bookmark> GetBookmarks();
        List<Bookmark> GetBookmarks(string ownerId);
        List<Bookmark> GetBookmarksByCategory(int categoryId, string ownerId);
        Bookmark GetBookmark(int? id);
        void UpdateBookmark(Bookmark bookmark);
        void DeleteBookmark(Bookmark bookmark);
        void DeleteBookmark(int id);
    }
}
