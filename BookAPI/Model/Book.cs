namespace BookAPI.Model
{
    public class Book
    {
        public int BookID { get; set; }
        public string Publisher { get; set; }
        public string Title { get; set; }
        public string AutherFirstName { get; set; }
        public string AutherLastName { get; set; }
        public decimal price { get; set; }
        public int yearofPublication { get; set; }
        public int Edition { get; set; }
        public string MLACitiation {  get; set; }
        public string PlaceofPublication { get; set; }
        public string URLs { get; set; }
    }
}
