namespace SignalR.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime SendDate { get; set; } = DateTime.Now;
        public string TextMessage { get; set; } = string.Empty;

    }
}
