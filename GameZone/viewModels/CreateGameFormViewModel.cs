
namespace GameZone.ViewModels
{
	public class CreateGameFormViewModel:GameFormViewModel
	{
		

		[AllowedExtensions(FileSettings.AllowedExtenstions)]
		[MaxFileSize(FileSettings.maxFileSizaInBytes)]
		public IFormFile Cover { get; set; } = default!;
	}
}
