namespace FiremanWpf.Model
{
    public class EventLogRecord
    {

        public EventLogRecord()
        {
            
        }

        public EventLogRecord(Fire fire, string message)
        {
            Fire = fire;
            Message = message;
        }

        public int Id { get; set; }

        public Fire Fire { get; set; }

        public string Message { get; set; }

    }
}
