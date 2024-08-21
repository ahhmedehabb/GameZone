
namespace GameZone.Controllers
{
	public class GamesController(IDevicesService devicesServices,
		ICategoriesService CategoriesServices,
		IGamesService gamesServices) : Controller
	{
        public IActionResult Index()
        {
            return View(gamesServices.GetAll());
        }

        [HttpGet]
		public IActionResult Create()
		{

			CreateGameFormViewModel viewModel = new()
			{
				Categories = CategoriesServices.GetSelectList(),
				Devices = devicesServices.GetSelectList()
			};
			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateGameFormViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = CategoriesServices.GetSelectList();
				model.Devices = devicesServices.GetSelectList();
				return View(model);
			}
			await gamesServices.Create(model);


			return RedirectToAction(nameof(Index));
		}
	}
}
