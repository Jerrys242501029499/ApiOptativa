using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using dominio.Modelo;
using MajoApi.Data;

namespace MajoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonajeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonajeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Response<PersonajeResponse>> CrearPersonaje([FromBody] PersonajeResponse i)
        {
            Personaje pers = new Personaje();

            pers.Nombre = i.Nombre;
            pers.Poder = i.Poder;
            pers.Color = i.Color;
            pers.FkGenero = i.FkGenero;

            var result = _context.personajes.Add(pers);
            await _context.SaveChangesAsync();
            return new Response<PersonajeResponse>(i, "agregado");
        }

        [HttpGet]
        public async Task<Response<List<Personaje>>> GetPersonajes()
        {
            var personaje = await _context.personajes.Include(x => x.genero).ToListAsync();

            return new Response<List<Personaje>>(personaje, "Obteniendo");
        }

        [HttpPut("{id}")]
        public async Task<Response<PersonajeResponse>> Update(int? id, [FromBody] PersonajeResponse request)
        {
            //try
            //{
            Personaje personaje = new Personaje();

            personaje = _context.personajes.Find(id);

            personaje.Nombre = request.Nombre;
            personaje.Poder = request.Poder;
            personaje.Color = request.Color;
            personaje.FkGenero = request.FkGenero;

            _context.Entry(personaje).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new Response<PersonajeResponse>(request, "Se actualizo");

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Personaje personaje = new Personaje();
                personaje = _context.personajes.Find(id);

                //if (personaje == null)
                //{
                //    return ;
                //}
                _context.personajes.Remove(personaje);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception("Ha ocurrido un error: " + ex.Message);
            }
        }

    }
}