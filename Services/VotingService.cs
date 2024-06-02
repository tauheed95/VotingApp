using System.Threading.Tasks;
using VotingApp.Context;
using VotingApp.Models;

namespace VotingApp.Services
{
    public class VotingService
    {
        private readonly VotingContext _context;

        public VotingService(VotingContext context)
        {
            _context = context;
        }

        public async Task AddVoter(Voter voter)
        {
            await _context.Voters.AddAsync(voter);
            await _context.SaveChangesAsync();
        }

        public async Task AddCandidate(Candidate candidate)
        {
            await _context.Candidates.AddAsync(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task CastVote(int voterId, int candidateId)
        {
            var voter = await _context.Voters.FindAsync(voterId);
            var candidate = await _context.Candidates.FindAsync(candidateId);

            if (voter != null && candidate != null && !voter.HasVoted)
            {
                voter.HasVoted = true;
                candidate.Votes += 1;
                await _context.SaveChangesAsync();
            }
        }
    }
}
