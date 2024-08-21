namespace GameZone.IServices
{
	public interface IGamesService
	{
		Task Create(CreateGameFormViewModel model);

		IEnumerable<Game> GetAll();
	}
}
