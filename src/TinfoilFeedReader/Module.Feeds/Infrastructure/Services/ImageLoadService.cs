using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Module.Feeds.Infrastructure.Services
{
    public class ImageLoadService
    {
        private readonly HttpClient _http;

        public ImageLoadService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> ImageAsBase64From(string imageUrl)
        {
            var bytes = await _http.GetByteArrayAsync(imageUrl)
                .ConfigureAwait(false);

            return Convert.ToBase64String(bytes);
        }
    }
}
