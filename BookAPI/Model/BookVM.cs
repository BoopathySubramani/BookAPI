namespace BookAPI.Model
{
    public class BookVM
    {
        public int PublisherID { get; set; }
        public string Title { get; set; }
        public int AutherID { get; set; }
        public decimal price { get; set; }
        public int yearofPublication { get; set; }
        public int Edition { get; set; }
        public string MLACitiation { get; set; }
        public String PlaceofPublication { get; set; }
        public string URLs { get; set; }
    }
}
