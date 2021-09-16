namespace Business.Models
{
    public class SidebarMenu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }

        public SidebarMenu(int Id, string title, string link)
        {
            this.Id = Id;
            this.Title = title;
            this.Link = link;
        }
    }
}
