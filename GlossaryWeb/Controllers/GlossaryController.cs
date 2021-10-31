using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlossaryWeb.Models;
using GlossaryWeb.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlossaryWeb.Controllers
{
    public class GlossaryController : Controller
    {
        private readonly Repository.IRepository.IGlossaryRepository glossaryRepo;

        public GlossaryController(IGlossaryRepository glossaryRepo)
        {
            this.glossaryRepo = glossaryRepo;
        }

        public IActionResult Index()
        {
            return View(new Glossary() { });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await glossaryRepo.Delete(Config.GlossaryApi, id, "");
            if (status)
            {
                return Json(new { success = true, message = "Successful" });
            }
            return Json(new { success = false, message = "Error" });
        }

        public IActionResult UpdateCreate(int? id)
        {

            return View(new Glossary() { });
        }

        public async Task<IActionResult> GetAllGlossaryItems()
        {
            var js = Json(new { data = await glossaryRepo.GetAll(Config.GlossaryApi, "") });
            
            return js;
        }

        public async Task<IActionResult> UpddateCreate(int? id)
        {
            Glossary obj = new Glossary();

            if (id == null)
            {
                return View(obj);
            }

            obj = await glossaryRepo.Get(Config.GlossaryApi, id.GetValueOrDefault(),"");
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }


        [HttpPost]
        public async Task<IActionResult> UpddateCreate (Glossary obj)
        {
            if (ModelState.IsValid)
            {
              
                var objFromDb = await glossaryRepo.Get(Config.GlossaryApi, obj.Id, "");
                
                if (obj.Id == 0)
                {
                    await glossaryRepo.Create(Config.GlossaryApi, obj, "");
                }
                else
                {
                    await glossaryRepo.Update(Config.GlossaryApi + obj.Id, obj, "");
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(Glossary obj)
        {
       
             await glossaryRepo.Create(Config.GlossaryApi, obj, "");
                

                return RedirectToAction(nameof(Index));
        }

    }
}