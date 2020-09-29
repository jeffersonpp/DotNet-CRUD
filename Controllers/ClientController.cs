using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Data;
using Client.Models;

namespace Client.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientContext _context;

        public ClientController(ClientContext context)
        {
            _context = context;
        }

        // GET: /Client
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Client/Show/5
        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mClient = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mClient == null)
            {
                return NotFound();
            }

            return View(mClient);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Email,Address")] mClient mClient)
        {
            if (ModelState.IsValid)
            {
                mClient.Created_At = DateTime.Now; //Manually set Created_at value
                _context.Add(mClient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mClient);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var mClient = await _context.Clients.FindAsync(id);
            if (mClient == null)
            {
                return NotFound();
            }
            return View(mClient);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Email,Address")] mClient mClient)
        {
            if (id != mClient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mClient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mClientExists(mClient.Id))
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
            return View(mClient);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mClient = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mClient == null)
            {
                return NotFound();
            }

            return View(mClient);
        }

        // GET: Client/Search
        public async Task<IActionResult> Search()
        {
            return View(await _context.Clients.ToListAsync()); // Return All clients
        }

        // POST: Client/Search/:searchString
        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
        var clients = from cli in _context.Clients 
        select cli;
        if (!String.IsNullOrEmpty(searchString))
        {
            clients = clients.Where(client => client.Name.Contains(searchString));
        }
        return View(await clients.ToListAsync());
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mClient = await _context.Clients.FindAsync(id);
            _context.Clients.Remove(mClient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mClientExists(int id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
