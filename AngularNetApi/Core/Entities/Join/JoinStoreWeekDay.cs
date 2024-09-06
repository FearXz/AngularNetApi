using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularNetApi.Core.Entities.Join
{
    public class JoinStoreWeekDay
    {
        [Key]
        public int JoinStoreWeekDayId { get; set; }

        [Required]
        [ForeignKey("Store")]
        public int StoreId { get; set; }

        [Required]
        [ForeignKey("WeekDay")]
        public int WeekDayId { get; set; }

        // NAVIGATION PROPERTY

        public virtual WeekDay WeekDay { get; set; }
        public virtual ICollection<WorkingHour> WorkingHour { get; set; }
    }
}
