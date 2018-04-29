namespace ShoppingCart.Core.Communication.ErrorCodes
{
    public static class ErrorCodes
    {
        public static int UserNotLoggedIn = 1;
        public static int UserNotFound = 2;
        public static int DatabaseError = 3;
        public static int RecordNotFound = 4;
        public static int CredentialsAreIncomplete = 4;
        public static int EmailAddressIsNotValid = 5;
        public static int NoMatchingVoucherFound = 6;
        public static int DeliveryTypeUnknown = 7;
        public static int OrderStatusUnkown = 8;
        public static int UserAlreadyRegistered = 9;
    }
}