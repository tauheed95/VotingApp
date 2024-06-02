using System.ComponentModel.DataAnnotations;

namespace VotingApp.Models
{
    public class Voter
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public bool HasVoted { get; set; }
    }
}
