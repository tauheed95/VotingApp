using System.Collections.Generic;

namespace VotingApp.Models
{
    public class VotingViewModel
    {
        public List<Voter> Voters { get; set; }
        public List<Candidate> Candidates { get; set; }
    }

}
