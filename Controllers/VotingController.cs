using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VotingApp.Context;
using VotingApp.Models;

namespace VotingApp.Controllers
{
    public class VotingController : Controller
    {
        private readonly VotingContext _context;

        public VotingController(VotingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new VotingViewModel
            {
                Voters = await _context.Voters.ToListAsync(),
                Candidates = await _context.Candidates.ToListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Vote(int voterId, int candidateId)
        {
            var voter = await _context.Voters.FindAsync(voterId);
            if (voter == null || voter.HasVoted)
            {
                TempData["Message"] = string.Format("Sorry, The Voter has already voted.");
                return RedirectToAction(nameof(Index));
            }

            var candidate = await _context.Candidates.FindAsync(candidateId);
            if (candidate == null)
            {
                TempData["Message"] = string.Format("Sorry, The Candidate is not exist.");
                return RedirectToAction(nameof(Index));
            }

            voter.HasVoted = true;
            candidate.Votes++;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}
