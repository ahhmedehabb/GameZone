﻿
namespace GameZone.Services
{
	public class CategoriesService(ApplicationDbContext dbContext) : ICategoriesService
	{
		public IEnumerable<SelectListItem> GetSelectList()
		{
			return dbContext.Categories
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
				.OrderBy(c => c.Text)
				.AsNoTracking()
				.ToList();
		}
	}
}
