namespace WoodcarvingApp.Common
{
    public static class ApplicationConstants
    {

        public const int NameMaxLength = 50;
        public const int NameMinLength = 2;
        public const int UrlMaxLength = 255;
        public const int TitleMaxLength = 50;
        public const int DescriptionMaxLength = 1000;

        public const int HardnessMaxLength = 20;
        public const int HardnessMinLength = 4;
        public const int ColorMaxLength = 20;
        public const int ColorMinLength = 3;

        public const int AddressMaxLength = 100;
        public const int AddressMinLength = 10;

        public const int ReleaseYear = 2024;
        public const string PhoneNumberRegex = @"^\+(\d{1,3})[-.\s]?(\d{1,4}[-.\s]?)*\d{4,15}$";
    }
}
