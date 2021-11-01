using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Dtos;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlossaryController : Controller
    {
        private IGlossaryRepository repo;
        private readonly IMapper mapper;

        public GlossaryController(IGlossaryRepository iRepo, IMapper iMapper)
        {
            repo = iRepo;
            mapper = iMapper;
        }

        //https://localhost:44361/api/Glossary/
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<GlossaryDto>))]
        public IActionResult GetGlossaryList()
        {
            //expose dtp
            var objList = repo.GetGlossaryList();
            var objDto = new List<GlossaryDto>();
            foreach (var obj in objList)
            {
                objDto.Add(mapper.Map<GlossaryDto>(obj));
            }
            return Ok(objDto);
        }

        //https://localhost:44361/api/Glossary/1
        [HttpGet("{id:int}", Name = "GetGlossaryItem")]
        [ProducesResponseType(200, Type = typeof(GlossaryDto))]
        [ProducesResponseType(404)]
        //[Authorize]
        [ProducesDefaultResponseType]
        public IActionResult GetGlossaryItem (int id)
        {
            var obj = repo.GetGlossaryItem(id);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<GlossaryDto>(obj);
            return Ok(objDto);

        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(GlossaryDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateGlossaryItem([FromBody] GlossaryDto dto)
        {
            if (dto == null)
            {
                return BadRequest(ModelState);
            }
            if (repo.GlossaryItemExists(dto.Term))
            {
                ModelState.AddModelError("", "Exists");
                return StatusCode(404, ModelState);
            }
            var obj = mapper.Map<Glossary>(dto);
            if (!repo.CreateGlossary(obj))
            {
                ModelState.AddModelError("", $"Error");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetGlossaryItem", new { Id = obj.Id }, obj);
        }


        [HttpPatch("{id:int}", Name = "UpdateGlossaryItem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateGlossaryItem(int id, [FromBody] GlossaryDto dto)
        {
            if (dto == null || id != dto.Id)
            {
                return BadRequest(ModelState);
            }

            var obj = mapper.Map<Glossary>(dto);
            if (!repo.UpdateGlossary(obj))
            {
                ModelState.AddModelError("", $"");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{id:int}", Name = "DeleteGlossaryItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteGlossaryItem(int id)
        {
            if (!repo.GlossaryItemExists(id))
            {
                return NotFound();
            }

            var obj = repo.GetGlossaryItem(id);
            if (!repo.HardDeleteGlossary(obj))
            {
                ModelState.AddModelError("", $"Error");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }



    }
}