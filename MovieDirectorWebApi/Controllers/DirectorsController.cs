﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDirectorWebApi;
using MovieDirectorWebApi.DTO;

namespace MovieDirectorWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DirectorsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Directors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectorDTO>>> GetDirectors()
        {
            return await 
                    _context.Directors.Select(dirObj => new DirectorDTO
                    {
                        DirId=dirObj.DirId,
                        Name = dirObj.Name,
                        Movies = dirObj.Movies
                    .Select(mov => new MovieDTO
                    {
                        Title = mov.Title,
                        MovieId = mov.MovieId
                    }).ToList()
                    })
                    .ToListAsync();
        }

        // GET: api/Directors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Director>> GetDirector(int id)
        {
            var director = await _context.Directors.FindAsync(id);

            if (director == null)
            {
                return NotFound();
            }

            return director;
        }

        // PUT: api/Directors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDirector(int id, Director director)
        {
            if (id != director.DirId)
            {
                return BadRequest();
            }

            _context.Entry(director).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Directors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Director>> PostDirector(DirectorDTO dir)
        {
            Director director = new Director
            {
                Name = dir.Name,
                Movies = new List<Movie>()
            };
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDirector", new { id = director.DirId }, director);
        }

        // DELETE: api/Directors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirector(int id)
        {
            var director = await _context.Directors
                                         .Include(d => d.Movies)
                                         .FirstOrDefaultAsync(d => d.DirId == id);

            if (director == null)
            {
                return NotFound();
            }

            // Optional: delete related movies or detach them
            // _context.Movies.RemoveRange(director.Movies); // if you want to delete movies
            foreach (var movie in director.Movies)
            {
                movie.Director.Remove(director); // if it's a many-to-many
            }

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.DirId == id);
        }
    }
}
