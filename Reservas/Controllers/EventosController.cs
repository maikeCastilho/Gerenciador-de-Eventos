using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservas.Data;
using Reservas.Models;

namespace Reservas.Controllers
{
    public class EventosController : Controller
    {
        private readonly ReservasContext _context;
        private readonly ILogger<EventosController> _logger;
        public EventosController(ReservasContext context, ILogger<EventosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.EventoId == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoId,Nome,Descricao,DataHora,PrecoIngresso")] Evento evento)
        {
            ModelState.Remove("Reservas");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(evento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError("Erro ao salvar o evento: " + ex.Message);
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o evento.");
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError("Erro de ModelState: " + error.ErrorMessage);
                }
            }

            return View(evento);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,Nome,Descricao,DataHora,PrecoIngresso")] Evento evento)
        {
            if (id != evento.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.EventoId))
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
            return View(evento);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.EventoId == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoId == id);
        }
    }
}
