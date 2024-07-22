﻿using Microsoft.AspNetCore.Http;

namespace eCommerceBase.Insfrastructure.Utilities.Extensions
{
    public static class FileExtension
    {
        public static string ConvertImageToBase64(this IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return "";
            }
            return "data:image/jpeg;base64," + file.ToBase64();
        }
        private static string ToBase64(this IFormFile file)
        {
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            byte[] fileBytes = ms.ToArray();
            return Convert.ToBase64String(fileBytes);
        }
    }
}
