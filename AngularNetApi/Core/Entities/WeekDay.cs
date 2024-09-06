using System.ComponentModel.DataAnnotations;

namespace AngularNetApi.Core.Entities
{
    public class WeekDay
    {
        [Key]
        public int WeekDayId { get; set; }

        [Required]
        public int WeekDayCode { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        public string WeekDayName { get; set; }
    }
}
