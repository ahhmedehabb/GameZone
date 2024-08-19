namespace GameZone.IServices
{
    public interface ICategoriesServices
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
