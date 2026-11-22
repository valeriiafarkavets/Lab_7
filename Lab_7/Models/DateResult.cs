namespace Lab_7.Models{
    public class DateResult{
        public int Number { get; set; }
        public string OriginalText { get; set; } = "";
        public string Day { get; set; } = "—";
        public string Month { get; set; } = "—";
        public string NormalizedDate { get; set; } = "—";
        public string Status { get; set; } = "Не розпізнано";
        public bool IsRecognized { get; set; }
    }
}
