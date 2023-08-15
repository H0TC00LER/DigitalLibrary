﻿using Microsoft.AspNetCore.Http;

namespace Service.Contracts
{
    public interface IBookLoadingService
    {
        public Task<byte[]?> LoadBookAsync(string id);
        public string GetBookFilePath(string id);
        public Task SaveBookAsync(IFormFile file, string textId);
    }
}
