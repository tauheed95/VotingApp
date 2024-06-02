using Microsoft.EntityFrameworkCore;
using VotingApp.Context;
using VotingApp.Models;
using VotingApp.Services;
using Xunit;

namespace VotingApp.Tests
{
    public class VotingServiceTests
    {
        private readonly VotingService _votingService;
        private readonly VotingContext _context;

        public VotingServiceTests()
        {
            var options = new DbContextOptionsBuilder<VotingContext>()
                .UseInMemoryDatabase(databaseName: "VotingAppTest")
                .Options;

            _context = new VotingContext(options);
            _votingService = new VotingService(_context);
        }

        [Fact]
        public async Task AddVoter_ShouldAddVoter()
        {
            var voter = new Voter { Name = "John Doe" };

            await _votingService.AddVoter(voter);

            var savedVoter = await _context.Voters.FirstOrDefaultAsync(v => v.Name == "John Doe");
            Assert.NotNull(savedVoter);
            Assert.Equal("John Doe", savedVoter.Name);
        }

        [Fact]
        public async Task AddCandidate_ShouldAddCandidate()
        {
            var candidate = new Candidate { Name = "Jane Doe" };

            await _votingService.AddCandidate(candidate);

            var savedCandidate = await _context.Candidates.FirstOrDefaultAsync(c => c.Name == "Jane Doe");
            Assert.NotNull(savedCandidate);
            Assert.Equal("Jane Doe", savedCandidate.Name);
        }

        [Fact]
        public async Task CastVote_ShouldIncrementVoteCount()
        {
            var voter = new Voter { Name = "John Doe", HasVoted = false };
            var candidate = new Candidate { Name = "Jane Doe", Votes = 0 };

            _context.Voters.Add(voter);
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();

            await _votingService.CastVote(voter.Id, candidate.Id);

            var updatedVoter = await _context.Voters.FindAsync(voter.Id);
            var updatedCandidate = await _context.Candidates.FindAsync(candidate.Id);

            Assert.True(updatedVoter.HasVoted);
            Assert.Equal(1, updatedCandidate.Votes);
        }
    }
}
