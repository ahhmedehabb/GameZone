namespace GameZone.IServices
{
    public interface ICategoriesService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
