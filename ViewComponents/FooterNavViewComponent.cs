using Microsoft.AspNetCore.Mvc;

namespace divar.ViewComponents
{
    public class FooterNavViewComponent : ViewComponent
    {
        public List<FooterNav> footerItems = [];

        public FooterNavViewComponent()
        {
            footerItems = FooterNav.Items();
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = footerItems;
            return await Task.FromResult((IViewComponentResult)View("FooterNavView", model));
        }
    }


    public class FooterNav
    {
        public string Title { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        
        public static List<FooterNav> Items()
        {
            List<FooterNav> itemList =
            [
                new FooterNav() { Title = "تماس با ما", Action = "ContactUs", Controller = "Home" },
                new FooterNav() { Title = "استعلام", Action = "Index", Controller = "Inquiry" },
            ];
            return itemList;
        }
    }
}