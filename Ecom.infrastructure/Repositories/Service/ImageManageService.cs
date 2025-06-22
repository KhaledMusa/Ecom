using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories.Service
{
    internal class ImageManageService : IImageManageService
    {
        private readonly IFileProvider FileProvider;
        public ImageManageService(IFileProvider fileProvider)
        {
            FileProvider = fileProvider;
        }

       public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images",src);
            if (!Directory.Exists(ImageDirectory))
            {
                Directory.CreateDirectory(ImageDirectory);
            }
            foreach (var item in files)
            {
                if (item.Length == 0)
                {
                    continue; 
                }
                var ImageName = item.FileName;
                var ImagePath = Path.Combine(ImageDirectory, ImageName);
                using (var stream = new FileStream(ImagePath, FileMode.Create))
                {
                  await  item.CopyToAsync(stream);
                }
                SaveImageSrc.Add(Path.Combine("Images", src, ImageName));
            }
            return SaveImageSrc;
        }

      public void DeleteImageAsync(string src)
        {
            var info = FileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
