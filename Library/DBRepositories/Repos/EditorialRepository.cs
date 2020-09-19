using Library.DBRepositories.Interfaces;
using Library.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Library.DBRepositories.Repos
{
    public class EditorialRepository : IEditorialRepository
    {
        private readonly DatabaseContext Context;

        public EditorialRepository(DatabaseContext context)
        {
            this.Context = context;
        }

        public Editorial Update(Editorial editorial)
        {
            Context.Editorials.Update(editorial);
            Context.SaveChanges();
            return editorial;
        }

        public void Delete(string id)
        {
            Editorial editorial = FindById(id);
            if (editorial != null)
            {
                Context.Editorials.Remove(editorial);
                Context.SaveChanges();
            }
        }

        public Editorial Create(Editorial editorial)
        {
            if (FindById(editorial.Id) == null)
            {
                Context.Editorials.Add(editorial);
                Context.SaveChanges();
            }
            return editorial;
        }

        public Editorial FindById(string idEditorial)
        {
            return Context.Editorials
                .FirstOrDefault(e => e.Id.Equals(idEditorial));
        }

        public List<Editorial> GetAll()
        {
            return Context.Editorials.ToList();
        }
    }
}