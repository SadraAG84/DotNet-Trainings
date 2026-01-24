namespace MeetingApp.Models
{
    public class MeetingInfo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Location { get; set; }
        public DateTime Date { get; set; }
        public string? NumberOfPeople { get; set; }
    }
}
