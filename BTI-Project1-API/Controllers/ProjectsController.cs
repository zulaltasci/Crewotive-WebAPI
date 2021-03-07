using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BTI_Project1_API.Context;
using BTI_Project1_API.Models;
using BTI_Project1_API.Attributes;

namespace BTI_Project1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<_Project>>> GetProject()
        {
            return await Helper.Convert.DbToProjectListAsync(_context);
        }

        // GET: api/Projects/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<_Project>>> GetAllProject()
        {
            return await Helper.Convert.DbToProjectListAsync(_context, true);
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<_Project>> GetProject(int id)
        {
            var project = await _context.Project.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            if (!project.IsActive)
            {
                return NotFound();
            }

            return await Helper.Convert.DbToProjectAsync(project, _context);
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            Helper.PutMethod.Project(_context, project);

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.Project.Add(Helper.Convert.ProjectToDb(project, _context));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Project.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            foreach (var person in _context.Person)
            {
                if (project.Id.ToString().Contains(person.ProjectIds))
                {
                    List<string> projectIds = person.ProjectIds.Split('-').ToList();
                    projectIds.Remove(project.Id.ToString());
                    person.ProjectIds = projectIds.Count == 1 ? projectIds[0] : String.Join('-', projectIds);
                }
            }

            project.IsActive = false;

            _context.Entry(project).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
