using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservas.Data;
using Reservas.Models;

namespace Reservas.Controllers
{
    public class ParticipantesController : Controller
    {
        private readonly ReservasContext _context;
        private readonly ILogger<EventosController> _logger;

        public ParticipantesController(ReservasContext context, ILogger<EventosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Participantes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Participantes.ToListAsync());
        }

        // GET: Participantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes
                .FirstOrDefaultAsync(m => m.IdParticipante == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // GET: Participantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Participantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdParticipante,Nome,Email,Tipo")] Participante participante)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(participante);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Loga o erro se ocorrer uma exceção ao salvar
                    _logger.LogError("Erro ao salvar o participante: " + ex.Message);
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o participante.");
                }
            }
            else
            {
                // Loga detalhadamente os erros de ModelState
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError("Erro de ModelState: " + error.ErrorMessage);
                }
            }

            // Retorna à view com os eventos para seleção e os erros de validação
            ViewBag.Eventos = new SelectList(_context.Eventos, "EventoId", "Nome");
            return View(participante);
        }

        // GET: Participantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }
            return View(participante);
        }

        // POST: Participantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdParticipante,Nome,Email,Tipo")] Participante participante)
        {
            if (id != participante.IdParticipante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipanteExists(participante.IdParticipante))
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
            return View(participante);
        }

        // GET: Participantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes
                .FirstOrDefaultAsync(m => m.IdParticipante == id);
            if (participante == null)
            {
                return NotFound();
            }

            return View(participante);
        }

        // POST: Participantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante != null)
            {
                _context.Participantes.Remove(participante);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipanteExists(int id)
        {
            return _context.Participantes.Any(e => e.IdParticipante == id);
        }
    }
}
