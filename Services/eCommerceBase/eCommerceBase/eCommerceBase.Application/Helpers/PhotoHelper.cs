using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.Result;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
namespace eCommerceBase.Application.Helpers
{
    public static class PhotoHelper
    {
        public static string PhotoPath => "/img/";

        public static string GetPhotoPath(Guid id) => PhotoPath + $"{id}.webp";
        public static List<string> SupportedMediaTypeList => ["jpeg", "png", "webp"];
        public static async Task<Result<string>> SaveBase64ImageAsWebP(Guid id, string base64String)
        {
            try
            {
                var isSupportedMedia = false;
                foreach(var imageExtension in SupportedMediaTypeList)
                {
                    if (base64String.StartsWith($"data:image/{imageExtension};base64,", StringComparison.OrdinalIgnoreCase))
                    {
                        base64String = base64String.Replace($"data:image/{imageExtension};base64,", "");
                        isSupportedMedia = true;
                    }
                }
                if (!isSupportedMedia)
                {
                    return Result.ErrorDataResult(Messages.UnsupportedImageFormat);
                }
                base64String = base64String.Replace("\n", "").Replace("\r", "");
                byte[] imageBytes = Convert.FromBase64String(base64String);
                using var image = Image.Load(imageBytes);
                string filePath = $"/app/wwwroot/img/{id}.webp";
                var webpEncoder = new WebpEncoder
                {
                    Quality = 75
                };
                await image.SaveAsync(filePath, webpEncoder);
                return Result.SuccessDataResult(GetPhotoPath(id), Messages.OperationSuccess);
            }
            catch (Exception)
            {
                return Result.ErrorDataResult(Messages.OperationError);
            }
        }
        public static Result DeleteImage(Guid id)
        {
            string filePath = $"/app/wwwroot/img/{id}.jpg";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Result.SuccessResult();
            }
            return Result.ErrorResult();
        }
    }
}
