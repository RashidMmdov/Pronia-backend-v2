using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Back_End_Pronia.Utilities
{
    public static class FileUtilities
    {
        public static async Task<string> PathFiles(this IFormFile file,string root,string folder)
        {
            string fileName = Guid.NewGuid() + file.FileName;
            string filePath = Path.Combine(root,folder);
            string fullPath = Path.Combine(filePath, fileName);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }

        public static void FileDelete(string root,string path,string fileName)
        {
            string fullPath = Path.Combine(root,path,fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

        }
    }
}
