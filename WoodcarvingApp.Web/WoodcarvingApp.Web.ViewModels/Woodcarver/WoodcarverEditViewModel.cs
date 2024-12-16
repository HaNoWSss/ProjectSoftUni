using System.ComponentModel.DataAnnotations;
using static WoodcarvingApp.Common.ApplicationConstants;

namespace WoodcarvingApp.Web.ViewModels.Woodcarver
{
	public class WoodcarverEditViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Please enter the first name of the woodcarver")]
		[StringLength(NameMaxLength, ErrorMessage = "One letter, really?", MinimumLength = NameMinLength)]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Please enter the last name of the woodcarver")]
		[StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "One letter, really?")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Please select a city")]
		public Guid? CityId { get; set; }

		public virtual IEnumerable<CityViewModel>? Cities { get; set; }

		[Required(ErrorMessage = "Please enter an age")]
		[Range(1, 120, ErrorMessage = "Come on man, we all know that's not the right age")]
		public int? Age { get; set; }

		[Required(ErrorMessage = "Please enter the phone number of the woodcarver")]
		[RegularExpression(PhoneNumberRegex, ErrorMessage = "Phone Number must start with a valid country code and be not more than 12 digits. Example correct format(Without the spaces): '+359 88 275 2840'")]
		public string? PhoneNumber { get; set; }

		[StringLength(UrlMaxLength, ErrorMessage = "Image URL cannot exceed 255 characters")]
		public string? ImageUrl { get; set; }

	}

}
