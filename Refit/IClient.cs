using System;
using System.Threading.Tasks;
using Refit;

namespace RefitDemo
{
    public interface IClient
    {
        [Post("/search")]
        public Task<ApiResponse<string>> Search();
    }
}
