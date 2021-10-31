using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.IRepository
{
    public interface IGlossaryRepository
    {
        ICollection<Glossary> GetGlossaryList();
        Glossary GetGlossaryItem(int id);
        bool CreateGlossary(Glossary g);

        bool GlossaryItemExists(string def);

        bool GlossaryItemExists(int id);

        bool UpdateGlossary(Glossary g);
        bool HardDeleteGlossary(Glossary g);

        bool SoftDeleteGlossary(Glossary g);

        bool Save();
    }
}
