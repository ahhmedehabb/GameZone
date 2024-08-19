namespace GameZone.Services
{
	public class DevicesService(ApplicationDbContext dbContext) : IDevicesService
	{
		public IEnumerable<SelectListItem> GetSelectList()
		{
			return dbContext.Devices
				.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
				.OrderBy(d => d.Text)
				.AsNoTracking()
				.ToList();
		}
	}
}
