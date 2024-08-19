



using GameZone.IServices;

namespace GameZone.Controllers
{
	public class GamesController(IDevicesServices devicesServices,ICategoriesServices CategoriesServices) : Controller
	{
		public IActionResult Index()
		{
			return View();
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
		public IActionResult Create(CreateGameFormViewModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = CategoriesServices.GetSelectList();
				model.Devices = devicesServices.GetSelectList();
				return View(model);
			}
			//save game to database 

			//save cover to server 


			return RedirectToAction(nameof(Index));
		}
	}
}
