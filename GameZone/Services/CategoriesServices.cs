
using GameZone.IServices;

namespace GameZone.Services
{
	public class CategoriesServices(ApplicationDbContext dbContext) : ICategoriesServices
	{
		public IEnumerable<SelectListItem> GetSelectList()
		{
			return dbContext.Categories
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
				.OrderBy(c => c.Text)
				.ToList();
		}
	}
}
