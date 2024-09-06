using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularNetApi.Core.Entities
{
    public class WorkingHour
    {
        [Key]
        public int WorkingHourId { get; set; }

        [Required]
        [ForeignKey("JoinStoreWeekDay")]
        public int JoinStoreWeekDayId { get; set; }

        [Required]
        public int ShiftOrder { get; set; } = 1;

        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
    }
}
