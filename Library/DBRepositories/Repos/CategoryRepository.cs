using Library.DBRepositories.Interfaces;
using Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Library.DBRepositories.Repos
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext Context;

        public CategoryRepository(DatabaseContext contexto)
        {
            Context = contexto;
        }

        public void Create(Category category)
        {
            if (FindById(category.Id) == null && category.Editorial != null)
            {
                Context.Categories.Add(category);
                Context.SaveChanges();
            }
        }

        public void Update(Category category)
        {
            if (category.Editorial != null)
            {
                Context.Categories.Update(category);
                Context.SaveChanges();
            }
        }

        public void Delete(string idCategory)
        {
            Category category = FindById(idCategory);
            if (category != null)
            {
                Context.Categories.Remove(category);
                Context.SaveChanges();
            }
        }

        public Category FindById(string idCategory)
        {
            return Context.Categories
                .Where(g => g.Id.Equals(idCategory))
                .FirstOrDefault();
        }

        public List<Category> FindByEditorial(string idEditorial)
        {
            return Context.Categories
                .Where(sb => sb.Editorial.Id.Equals(idEditorial))
                .ToList();
        }
    }
}