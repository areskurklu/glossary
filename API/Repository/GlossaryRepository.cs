using API.Data;
using API.Models;
using API.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class GlossaryRepository : IGlossaryRepository
    {
        private readonly ApplicationDbContext _db;

        //dependency injection
        public GlossaryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool GlossaryItemExists(string def)
        {
            bool value = _db.GlossaryList.Any(a => a.Term.ToLower().Trim() == def.ToLower().Trim());
            return value;
        }

        public bool GlossaryItemExists(int id)
        {
            return _db.GlossaryList.Any(a => a.Id == id);
        }

        public bool CreateGlossary(Glossary g)
        {
            _db.Add(g);
            return Save();
        }

        public bool HardDeleteGlossary(Glossary g)
        {
            _db.GlossaryList.Remove(g);
            return Save();
        }
        public bool SoftDeleteGlossary(Glossary g)
        {
            g.isDeleted = true;
            _db.GlossaryList.Update(g);
            return Save();
        }

        public Glossary GetGlossaryItem(int id)
        {
            return _db.GlossaryList.FirstOrDefault(a => a.Id == id);
            //return new Glossary();
        }

        public ICollection<Glossary> GetGlossaryList()
        {
            return _db.GlossaryList.OrderBy(a => a.Term).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateGlossary(Glossary g)
        {
            _db.GlossaryList.Update(g);
            //save
            return Save();
        }
    }
}
