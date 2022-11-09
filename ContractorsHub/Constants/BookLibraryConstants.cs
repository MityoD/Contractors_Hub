namespace ContractorsHub.Constants
{
    public class BookLibraryConstants
    {
        public class AppUserConstants
        {
            public const int UserNameMaxLength = 20;
            public const int UserNameMinLength = 5;
            public const int EmailMaxLength = 60;
            public const int EmailMinLength = 10;
            public const int PasswordMaxLength = 20;
            public const int PasswordMinLength = 5;
        }
        public class BookConstants
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 10;
            public const int AuthorMaxLength = 50;
            public const int AuthorMinLength = 5;
            public const int DescriptionMaxLength = 5000; 
            public const int DescriptionMinLength = 5;
            public const string RatingMaxValue = "10.00";
            public const string RatingMinValue = "0.00";

        }
        public class CategoryConstants
        {
            public const int MaxNameLength = 50;
            public const int MinNameLength = 5;
        }
      
    }
}
