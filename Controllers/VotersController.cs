using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VotingApp.Context;
using VotingApp.Models;

namespace VotingApp.Controllers
{
    public class VotersController : Controller
    {
        private readonly VotingContext _context;

        public VotersController(VotingContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Voters.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Voter voter)
        {
            var IsVoterExist = _context.Voters.FirstOrDefault(x => x.Name == voter.Name);
            if (IsVoterExist != null)
            {
                TempData["Message"] = string.Format("Sorry, The Voter has already exist.");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _context.Voters.Add(voter);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }

}
