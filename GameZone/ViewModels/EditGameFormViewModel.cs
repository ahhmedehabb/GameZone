namespace GameZone.ViewModels
{
	public class EditGameFormViewModel : GameFormViewModel
	{

		public int Id { get; set; }

		public string? currentCover  { get; set; }

		[AllowedExtensions(FileSettings.AllowedExtenstions)]
		[MaxFileSize(FileSettings.maxFileSizaInBytes)]
		public IFormFile? Cover { get; set; } = default!;
	}
}
