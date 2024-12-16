namespace WoodcarvingApp.Web.ViewModels.Exhibition
{
    public class WoodcarvingCheckboxViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string WoodcarverName { get; set; } = null!;
        public bool IsSelected { get; set; }
    }
}
