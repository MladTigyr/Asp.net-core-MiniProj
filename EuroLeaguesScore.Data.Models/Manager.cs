namespace EuroLeaguesScore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static GCommon.EntityValidations;

    public class Manager
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ManagerNameMaxLength)]
        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public int? TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public virtual Team? Team { get; set; }
    }
}