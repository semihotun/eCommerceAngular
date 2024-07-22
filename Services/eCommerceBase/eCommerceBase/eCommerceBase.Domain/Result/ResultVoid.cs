using eCommerceBase.Domain.Resources;

namespace eCommerceBase.Domain.Result
{
    /// <summary>
    /// Ardalis Result
    /// </summary>
    public class Result : Result<Result>
    {
        public static Result SuccessResult(string message)
        {
            return new Result() { Message = LanguageException.GetKey(message), Success = true};
        }
        public static Result ErrorResult(string message)
        {
            return new Result() { Message = LanguageException.GetKey(message), Success = false };
        }
        public static Result SuccessResult()
        {
            return new Result() { Success = true };
        }
        public static Result ErrorResult()
        {
            return new Result() { Success = false };
        }
        public static Result<T> SuccessDataResult<T>(T value, string successMessage) => new(value, true, LanguageException.GetKey(successMessage));
        public static Result<T> SuccessDataResult<T>(T value) => new(value, true);
        public static Result<T> SuccessDataResult<T>() => new(true);
        public static Result<T> ErrorDataResult<T>(T value, string successMessage) => new(value, false, LanguageException.GetKey(successMessage));
        public static Result<T> ErrorDataResult<T>(T value) => new(value, false);
        public static Result<T> ErrorDataResult<T>() => new(false);
        public static Result<T> ErrorDataResult<T>(string message) => new(false, LanguageException.GetKey(message));
    }
}