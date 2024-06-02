using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VotingApp.Context;
using VotingApp.Models;

namespace VotingApp.Controllers
{

    public class CandidatesController : Controller
    {
        private readonly VotingContext _context;

        public CandidatesController(VotingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Candidates.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Candidate candidate)
        {
            var IsCandidateExist = _context.Candidates.FirstOrDefault(x => x.Name == candidate.Name);
            if (IsCandidateExist != null)
            {
                TempData["Message"] = string.Format("Sorry, The Candidate has already exist.");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _context.Candidates.Add(candidate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
