using GlossaryWeb.Models;
using GlossaryWeb.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GlossaryWeb.Repository
{
    public class GlossaryRepository : Repository<Glossary>, IGlossaryRepository
    {
        private readonly IHttpClientFactory clientFactory;

        public GlossaryRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            this.clientFactory = clientFactory;
        }
    }
}
