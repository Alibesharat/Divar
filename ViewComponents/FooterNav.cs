namespace divar.ViewComponents
{
    public class FooterNav
    {
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

         public static List<FooterNav> Items()
    {
        List<FooterNav> itemList = new ();
        itemList.Add(new FooterNav(){Title="تماس با ما",Action="ContactUs",Controller="Home"});
        itemList.Add(new FooterNav(){Title="درباره ما",Action="AboutUs",Controller="Home"});
        itemList.Add(new FooterNav(){Title="پایگاه دانش",Action="KnowledgeBase",Controller="Home"});
        itemList.Add(new FooterNav(){Title="استعلام",Action="Inquiry",Controller="Home"});
        return itemList;
    }
    }
}