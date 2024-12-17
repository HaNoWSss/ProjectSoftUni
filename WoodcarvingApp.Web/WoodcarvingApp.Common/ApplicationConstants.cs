namespace WoodcarvingApp.Common
{
    public static class ApplicationConstants
    {

        public const int IdMinLength = 36;
        public const int IdMaxLength = 36;

        public const int NameMaxLength = 20;
        public const int NameMinLength = 2;
        public const int UrlMaxLength = 255;
        public const int TitleMaxLength = 50;
        public const int DescriptionMaxLength = 200;

        public const int HardnessMaxLength = 20;
        public const int HardnessMinLength = 4;
        public const int ColorMaxLength = 20;
        public const int ColorMinLength = 3;

        public const int AddressMaxLength = 100;
        public const int AddressMinLength = 10;

        public const int AgeMaxLength = 120;
        public const int AgeMinLength = 1;

        public const int ReleaseYear = 2024;
        public const string PhoneNumberRegex = @"^[0-9]{3}.?[0-9]{3}.?[0-9]{4}";

        public const string AdminRoleName = "Admin";
        public const string UserRoleName = "User";
    }
}
