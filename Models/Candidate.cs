using System.ComponentModel.DataAnnotations;

namespace VotingApp.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int Votes { get; set; }
    }
}
