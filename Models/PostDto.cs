namespace divar.Models
{


    public class PostData
    {
        public string Token { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
        public string BrandModel { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int ExpireDays { get; set; }
        public List<string> Images { get; set; }
        public string InternalStorage { get; set; }
        public int NewPrice { get; set; }
        public string Originality { get; set; }
        public string PrefilledTitle { get; set; }
        public Price Price { get; set; }
        public string RamMemory { get; set; }
        public string SimCardSlot { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
    }

    public class Price
    {
        public string Mode { get; set; }
        public long Value { get; set; }
    }

}