using GameZone.Models;

namespace GameZone.Services
{
	public class GamesService : IGamesService
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly string _imagesPath;
		public GamesService(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
		{
			_dbContext = dbContext;
			_webHostEnvironment = webHostEnvironment;
			_imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.imagesPath}";
		}

		public IEnumerable<Game> GetAll()
		{
			return _dbContext.Games.Include(g => g.Category)
				.Include(d => d.Devices).ThenInclude(d => d.device).AsNoTracking().ToList();
		}

		public Game? getById(int id)
		{
			return _dbContext.Games.Include(g => g.Category)
				.Include(d => d.Devices).ThenInclude(d => d.device).AsNoTracking().SingleOrDefault(g => g.Id == id);
		}
		public async Task Create(CreateGameFormViewModel model)
		{
			var coverName = await SaveCover(model.Cover);

			Game game = new()
			{
				Name = model.Name,
				Description = model.Description,
				CategoryId = model.CategoryId,
				Cover = coverName,
				Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
			};
			_dbContext.Add(game);
			_dbContext.SaveChanges();
		}

		public async Task<Game?> Update(EditGameFormViewModel model)
		{
			var game = _dbContext.Games.Include(g => g.Devices).SingleOrDefault(g => g.Id == model.Id);
			if (game is null)
				return null;

			var hasNewCover = model.Cover is not null;
			var currentCover=game.Cover;

			game.Name = model.Name;
			game.Description = model.Description;
			game.CategoryId = model.CategoryId;
			game.Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList();

			if (hasNewCover)
			{
				game.Cover = await SaveCover(model.Cover!);
			}

			var effectedRows = _dbContext.SaveChanges();
			if (effectedRows > 0)
			{
				if (hasNewCover)
				{
					DeleteCover(currentCover);
				}
				return game;
			}
			else
			{
				DeleteCover(game.Cover);

				return null;
			}

			

		}

		public bool Delete(int id)
		{
			var isDeleted = false;

			var game = _dbContext.Games.Find(id);

			if (game is null)
				return isDeleted;

			_dbContext.Remove(game);

			var effecttedRows=_dbContext.SaveChanges();
			if (effecttedRows > 0) 
			{
				isDeleted = true;
				DeleteCover(game.Cover);
			}

			return isDeleted;
		}
		private void DeleteCover(string path)
		{
			var cover = Path.Combine(_imagesPath, path);
			File.Delete(cover);
		}
		private async Task<string> SaveCover(IFormFile cover)
		{
			var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

			var path = Path.Combine(_imagesPath, coverName);

			using var stream = File.Create(path);

			await cover.CopyToAsync(stream);

			return coverName;
		}

		
	}
}
