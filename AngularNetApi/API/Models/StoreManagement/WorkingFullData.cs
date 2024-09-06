using System.ComponentModel.DataAnnotations;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.API.Models.StoreManagement
{
    public class WorkingDaysFullData
    {
        public int StoreId { get; set; }
        public int WeekDayCode { get; set; }

        public int Order { get; set; }

        public string WeekDayName { get; set; }

        public ICollection<WorkingHoursData> WorkingHours { get; set; }
    }
}
