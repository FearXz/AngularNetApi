using System.ComponentModel.DataAnnotations;

namespace AngularNetApi.Core.Entities
{
    public class WeekDay
    {
        [Key]
        public int WeekDayId { get; set; }

        [Required]
        public int WeekDayNumber { get; set; }

        [Required]
        public string WeekDayName { get; set; }

        //public TimeSpan? OpeningTime { get; set; }
        //public TimeSpan? ClosingTime { get; set; }
        //public TimeSpan? OpeningTime2 { get; set; }
        //public TimeSpan? ClosingTime2 { get; set; }
    }
}
