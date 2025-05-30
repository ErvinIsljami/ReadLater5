﻿using Data;
using Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private ReadLaterDataContext _ReadLaterDataContext;

        public CategoryService(ReadLaterDataContext readLaterDataContext) 
        {
            _ReadLaterDataContext = readLaterDataContext;
        }

        public Category CreateCategory(Category category)
        {
            _ReadLaterDataContext.Add(category);

            _ReadLaterDataContext.SaveChanges();
            return category;
        }

        public void UpdateCategory(Category category)
        {
            _ReadLaterDataContext.Update(category);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return _ReadLaterDataContext.Categories.ToList();
        }

        public Category GetCategory(int Id)
        {
            return _ReadLaterDataContext.Categories.Where(c => c.ID == Id).FirstOrDefault();
        }

        public Category GetCategory(string Name)
        {
            return _ReadLaterDataContext.Categories.Where(c => c.Name == Name).FirstOrDefault();
        }

        public void DeleteCategory(Category category)
        {
            _ReadLaterDataContext.Categories.Remove(category);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<string> GetCategoryNames()
        {
            return _ReadLaterDataContext.Categories.Select(x => x.Name).ToList();
        }

        public List<Category> GetCategories(string ownerId)
        {
            return _ReadLaterDataContext.Categories.Where(x => x.OwnerId == ownerId).ToList();
        }

        public List<string> GetCategoryNames(string ownerId)
        {
            return _ReadLaterDataContext.Categories.Where(a => a.OwnerId == ownerId).Select(x => x.Name).ToList();
        }
    }
}
