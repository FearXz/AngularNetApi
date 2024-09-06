namespace AngularNetApi.API.Models.StoreManagement
{
    public class WorkingHoursData
    {
        public int ShiftOrder { get; set; } = 1;

        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
    }
}
