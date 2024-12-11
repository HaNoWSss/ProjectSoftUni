using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static WoodcarvingApp.Common.ApplicationConstants;


namespace WoodcarvingApp.Web.ViewModels.Woodcarver
{
    public class WoodcarverCreateViewModel
    {
        [Required(ErrorMessage = "Please enter the first name of the woodcarver")]
        [StringLength(NameMaxLength, ErrorMessage = $"Woodcarver first name cannot exceed 50 characters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the last name of the woodcarver")]
        [StringLength(NameMaxLength, ErrorMessage = $"Woodcarver last name cannot exceed 50 characters")]
        public string LastName { get; set; } = null!;

        [Required]
        public Guid CityId { get; set; }

        [BindNever]
        public SelectList CityList { get; set; } = null!;

        [Required(ErrorMessage = "Please enter the phone number of the woodcarver")]
        [Range(1, 120, ErrorMessage = "Come on man, we all know that's not the right age")]
        public int Age { get; set; }

        [Required]
        [RegularExpression(PhoneNumberRegex, ErrorMessage = "Phone Number must with a valid country code and not more than 12 digits. Example correct format(Without the spaces): '+359 88 275 2840'")]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(UrlMaxLength, ErrorMessage = "Image URL cannot exceed 255 characters")]
        public string? ImageUrl { get; set; }
    }
}
