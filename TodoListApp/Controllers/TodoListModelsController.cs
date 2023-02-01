using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListApp.Data;
using TodoListApp.Models;

namespace TodoListApp.Controllers
{
    public class TodoListModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TodoListModelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TodoListModels
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserId != null && User.IsInRole("User"))
            {

                var todo = from c in _context.TodoListModel
                           orderby c.Guid
                           where c.UserId == UserId
                           select c;
                return View(todo);
            }
            IQueryable<TodoListModel> items = from i in _context.TodoListModel orderby i.Guid select i;
            List<TodoListModel> todoList = await items.ToListAsync();
            return View(todoList);
        }

        // GET: TodoListModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoListModel = await _context.TodoListModel
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (todoListModel == null)
            {
                return NotFound();
            }

            return View(todoListModel);
        }

        // GET: TodoListModels/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoListModels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        
        public async Task<IActionResult> Create([Bind("Guid,Title,Content,DateCreated, UserId")] TodoListModel todoListModel)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                todoListModel.UserId = user;
                _context.Add(todoListModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoListModel);
        }

        // GET: TodoListModels/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoListModel = await _context.TodoListModel.FindAsync(id);
            if (todoListModel == null)
            {
                return NotFound();
            }
            return View(todoListModel);
        }

        // POST: TodoListModels/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, TodoListModel todoListModel)
        {

            var todo = await _context.TodoListModel.FindAsync(id);

            if (todo == null)
            {
               return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    todo.Content = todoListModel.Content != null ? todoListModel.Content : todo.Content;
                    todo.Title = todoListModel.Title != null ? todoListModel.Title : todo.Title;
                   
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoListModelExists(todoListModel.Guid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todoListModel);
        }

        // GET: TodoListModels/Delete/5
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoListModel = await _context.TodoListModel
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (todoListModel == null)
            {
                return NotFound();
            }

            return View(todoListModel);
        }

        // POST: TodoListModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoListModel = await _context.TodoListModel.FindAsync(id);
            _context.TodoListModel.Remove(todoListModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoListModelExists(int id)
        {
            return _context.TodoListModel.Any(e => e.Guid == id);
        }
    }
}
