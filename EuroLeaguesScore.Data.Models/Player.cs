namespace EuroLeaguesScore.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using EuroLeaguesScore.Data.Models.Enums;
    using static GCommon.EntityValidations;

    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PlayerNameMaxLength)]
        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public Position Position { get; set; }

        public int Goals { get; set; }

        public int Assists { get; set; }

        public int TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; } = null!;

        public int? ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public virtual Manager? Manager { get; set; }
    }
}
