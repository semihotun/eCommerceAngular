using Elastic.CommonSchema;

namespace eCommerceBase.Application.Constants
{
    /// <summary>
    /// Buradaki Bütün mesajlar translate'e key olarak verilip front ende döndürülecek
    /// </summary>
    public static class Messages
    {
        public static string OperationSuccess => "OperationSuccess";
        public static string OperationError => "OperationError";
        public static string Added => "Added";
        public static string NotFoundData => "NotFoundData";
        public static string AddedError => "AddedError";
        public static string Deleted => "Deleted";
        public static string DeletedError => "DeletedError";
        public static string Updated => "Updated";
        public static string UpdatedError => "UpdatedError";
        public static string ClaimExists => "ClaimExists";
        public static string UserNotLogged => "UserNotLogged";
        public static string AuthorizationDenied => "AuthorizationDenied";
        public static string UserNotFound => "UserNotFound";
        public static string PasswordError => "PasswordError";
        public static string NameAlreadyExist => "NameAlreadyExist";
        public static string EmailAlreadyExist => "EmailAlreadyExist";
        public static string PasswordEmpty => "PasswordEmpty";
        public static string PasswordSpecialCharacter => "PasswordSpecialCharacter";
        public static string MailInformationIsMissing => "MailInformationIsMissing";
        public static string MailSended => "MailSended";
        public static string MailNotSended => "MailNotSended";
        public static string ActivationCodeWrongOrOutOfDate => "ActivationCodeWrongOrOutOfDate";
        public static string UserHasAldreadyBeenApproved => "UserHasAldreadyBeenApproved";
        public static string AccountAlreadyConfirmed => "AccounAlreadyConfirmed";
        public static string EmailConfirmed => "EmailConfirmed";
        public static string UnsupportedImageFormat => "UnsupportedImageFormat";
    }
}
