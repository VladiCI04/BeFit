using System.Dynamic;

namespace BeFit.Common
{
    public static class EntityValidationConstants
    {
        public static class Event
        {
            // Title
            public const int EventTitleMinLength = 5;
            public const int EventTitleMaxLength = 40;

            // Description
            public const int EventDescriptionMinLength = 15;
            public const int EventDescriptionMaxLength = 250;

            // Tax
            public const double EventTaxMin = 0.00;
            public const double EventTaxMax = 50.00;

            // Address
            public const int EventAddressMinLength = 5;
            public const int EventAddressMaxLength = 200;
        }

        public static class EventCategory
        {
            // Name
            public const int EventCategoryNameMinLength = 1;
            public const int EventCategoryNameMaxLength = 30;
        }


        public static class Coach
        {
            // Age
            public const int CoachAgeMin = 18;
            public const int CoachAgeMax = 70;

            // Height
            public const double CoachHeightMin = 1.00;
            public const double CoachHeightMax = 3.00;

            // Weight
            public const double CoachWeightMin = 60.00;
            public const double CoachWeightMax = 150.00;

            // PhoneNumber
            public const int CoachPhoneNumberMinLength = 7;
            public const int CoachPhoneNumberMaxLength = 15;

            // Description
            public const int CoachDescriptionMaxLength = 1000;
        }

        public static class CoachCategory
        {
            // Name
            public const int CoachCategoryNameMinLength = 1;
            public const int CoachCategoryNameMaxLength = 30;
        }

        public static class User
        {
            public const int UserFirstNameMinLength = 1;
            public const int UserFirstNameMaxLength = 15;

            public const int UserLastNameMinLength = 1;
            public const int UserLastNameMaxLength = 15; 
        }
    }
}
