using AntDesign;

namespace Ui_OCSS.Components.Layout
{
    public class MenuService
    {
        public List<NavItem> MenuItems { get; set; } = [];
    }
    public class NavItem
    {
        public required string Href { get; set; }
        public required string Name { get; set; }
        public required string Icon { get; set; }
    }
}
