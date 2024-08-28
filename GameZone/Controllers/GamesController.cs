
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

		public IActionResult Details(int id)
		{
			var game=gamesServices.getById(id);
			if(game == null)
				return NotFound();

			return View(game);
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
		[HttpGet]
		public IActionResult Edit(int id)
		{
			var game = gamesServices.getById(id);
			if (game == null)
				return NotFound();

			EditGameFormViewModel viewModel = new()
			{
				Id= id,
				Name= game.Name,
				Description= game.Description,
				CategoryId= game.CategoryId,
				SelectedDevices=game.Devices.Select(d=>d.DeviceId).ToList(),
				Categories=CategoriesServices.GetSelectList(),
				Devices=devicesServices.GetSelectList(),
				currentCover=game.Cover
			};
			return View(viewModel);
		}
	}
}
