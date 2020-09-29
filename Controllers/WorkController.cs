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
    public class WorkController : Controller
    {
        // The Context is the same at the entire App
        private readonly ClientContext _context;

        public WorkController(ClientContext context)
        {
            _context = context;
        }

        // GET: /Work
        public async Task<IActionResult> Index()
        {
        var mWorks =  _context.Works
        .Include(mw => mw.Client)
        .AsNoTracking();

        return View(await mWorks.ToListAsync());
        }

        // GET: Work/Show/5
        public async Task<IActionResult> Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mWorks =  _context.Works
            .Include(mw => mw.Client)
            .AsNoTracking();

            if (mWorks == null)
            {
                return NotFound();
            }
            return View(await mWorks.FirstOrDefaultAsync(m => m.Id == id));
        }

        // GET: Work/Create
        public async Task<IActionResult> Create()
        {
            var listClient = await _context.Clients.ToListAsync();

            var works = new ModelView();
                works.listClient = new SelectList(listClient.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name
            }).ToList(), "Value", "Text");

            return View(works);
        }

        // POST: Work/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Client, [Bind("Id,Name,Description,Price,Started_At")] mWork mWork)
        {
            if (ModelState.IsValid)
            {
                mWork.Client = await _context.Clients.FindAsync(Client);
                _context.Add(mWork);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mWork);
        }

        // GET: Work/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Select Work and Client Object
            var mWork =  _context.Works
            .Include(mw => mw.Client)
            .AsNoTracking();

            if (mWork == null)
            {
                return NotFound();
            }

            //Create The Adapter ModelView
            var works = new ModelView();
            // Store The Selected Work
                works.Work = await mWork.FirstOrDefaultAsync(m => m.Id == id);

            //Creating the Clients DropDownList
            var listClient = await _context.Clients.ToListAsync();
            var list = new SelectList(listClient.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name

            }).ToList(), "Value", "Text");

            //Select the selected Item
            list.Where(x => x.Value == works.Work.Client.Id.ToString()).First()
            .Selected = true;

            //Put the List inside the Model
            works.listClient = list;

            return View(works);
        }

        // POST: Work/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int Client,[Bind("Id,Name,Description,Price,Started_At")] mWork mWork)
        {
            if (id != mWork.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    mWork.Client = await _context.Clients.FindAsync(Client);
                    _context.Update(mWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mWorkExists(mWork.Id))
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
        var mWorks =  _context.Works
        .Include(mw => mw.Client)
        .AsNoTracking();

        return View(await mWorks.ToListAsync());
        }

        // GET: Work/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mWork = await _context.Works
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mWork == null)
            {
                return NotFound();
            }
            return View(mWork);
        }

        // GET: Work/Search
        // Return All Works with Respective Clients
        public async Task<IActionResult> Search()
        {
        var mWorks =  _context.Works
        .Include(mw => mw.Client)
        .AsNoTracking();

        return View(await mWorks.ToListAsync());
        }

        // POST: Work/Search/:searchString
        // Return All Works Filtered by searchString with Respective Clients
        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
        var Works = from cli in _context.Works 
        .Include(mw => mw.Client)
        select cli;
        if (!String.IsNullOrEmpty(searchString))
        {
            Works = Works.Where(Work => Work.Name.Contains(searchString));
        }
        return View(await Works.ToListAsync());
        }

        // POST: Work/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mWork = await _context.Works.FindAsync(id);
            _context.Works.Remove(mWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mWorkExists(int id)
        {
            return _context.Works.Any(e => e.Id == id);
        }
    }
}