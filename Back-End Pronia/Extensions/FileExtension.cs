using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Back_End_Pronia.Extensions
{
    public static class FileExtension
    {
        public static bool IsContain(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }
        public static bool IsGreater(this IFormFile file,int mb)
        {
            return file.Length > mb * 1024 * 1024;
        }

        public static bool IsValid(this IFormFile file,int mb)
        {
            return file.ContentType.Contains("image") && file.Length < mb * 1024 * 1024;
        }
    }
}
