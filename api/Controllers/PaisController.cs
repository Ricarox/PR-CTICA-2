﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PaisController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet] 
        public IEnumerable<Pais>Get()
        {
            return context.Paises.ToList();
        }

        [HttpGet("{id}", Name = "paisCreado" )]
        public IActionResult GetById(int id)
        {
            var pais = context.Paises.FirstOrDefault(x => x.Id == id);

            if (pais == null)
            {

                return NotFound();

            }

            return Ok(pais);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Paises.Add(pais);
                context.SaveChanges();
                return new CreatedAtRouteResult("paisCreado", new { id = pais.Id }, pais);
            }
            
            return BadRequest(ModelState);
        }

      
    }
}
