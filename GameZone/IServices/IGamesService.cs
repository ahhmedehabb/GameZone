namespace GameZone.IServices
{
	public interface IGamesService
	{
		IEnumerable<Game> GetAll();

		Game? getById(int id);
		Task Create(CreateGameFormViewModel model);

		Task<Game?> Update(EditGameFormViewModel model);


		

	
	}
}
