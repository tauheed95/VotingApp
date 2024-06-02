namespace VotingApp.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int VoterId { get; set; }
        public int CandidateId { get; set; }
    }
}
