using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _readLaterDataContext;

        public BookmarkService(ReadLaterDataContext readLaterDataContext)
        {
            _readLaterDataContext = readLaterDataContext;
        }

        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            _readLaterDataContext.Add(bookmark);
            _readLaterDataContext.SaveChanges();

            return bookmark;
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _readLaterDataContext.Remove(bookmark);
            _readLaterDataContext.SaveChanges();
        }

        public void DeleteBookmark(int id)
        {
            Bookmark targetBookmark = _readLaterDataContext.Bookmark.FirstOrDefault(x => x.ID == id);
            if(targetBookmark == null)
            {
                throw new KeyNotFoundException("The given id does not match any bookmark in database.");
            }

            _readLaterDataContext.Remove(targetBookmark);
        }

        public Bookmark GetBookmark(int? id)
        {
            return _readLaterDataContext.Bookmark.Include(a => a.Category).FirstOrDefault(x => x.ID == id);
        }

        public List<Bookmark> GetBookmarks()
        {
            return _readLaterDataContext.Bookmark.Include(x => x.Category).ToList();
        }

        public List<Bookmark> GetBookmarks(string ownerId)
        {
            return _readLaterDataContext.Bookmark.Where(x => x.OwnerId == ownerId).Include(a => a.Category).ToList();
        }

        public List<Bookmark> GetBookmarksByCategory(int categoryId)
        {
            return _readLaterDataContext.Bookmark.Where(x => x.CategoryId == categoryId).ToList();
        }

        public List<Bookmark> GetBookmarksByCategory(int categoryId, string ownerId)
        {
            return _readLaterDataContext.Bookmark.Where(x => x.CategoryId == categoryId && x.OwnerId == ownerId).ToList();
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _readLaterDataContext.Update(bookmark);
            _readLaterDataContext.SaveChanges();
        }
    }
}
