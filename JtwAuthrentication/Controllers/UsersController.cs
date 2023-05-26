// 生成一个获取用户列表的ApiController
using JtwAuthrentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JtwAuthrentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("name")]
        [Authorize()]
        public string GetName()
        {
            return "admin";
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public Task<User[]> Get()
        {
            return Task.FromResult(MockUsers(10));
        }

        // 使用Mock数据返回10个User
        private User[] MockUsers(int count)
        {
            var users = new User[count];
            for (int i = 0; i < count; i++)
            {
                users[i] = new User
                {
                    Id = i,
                    Username = $"user{i}",
                    Password = $"password{i}",
                    Email = $"user{i}@mail.com",
                };
            }
            return users;
        }
    }
}