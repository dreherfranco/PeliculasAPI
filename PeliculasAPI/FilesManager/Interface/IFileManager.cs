using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.FilesManager.Interface
{
    public interface IFileManager
    {
        Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType);
        Task DeleteFile(string route, string container);
        Task<string> SaveFile(byte[] content, string extension, string container, string contentType);
    }
}
