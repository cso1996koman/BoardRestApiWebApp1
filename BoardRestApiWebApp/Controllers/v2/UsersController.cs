using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Datas.Repositories;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Logging;
using RestApiProject.Models;
using Services;
using WebFramework.Api;

namespace RestApiProject.Controllers.v2
{
    [ApiVersion("2")]
    public class UsersController : v1.UsersController
    {
        public UsersController(IUserRepository userRepository,ILogger<v1.UsersController> logger,IJwtService jwtService,
            UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager)
            : base(userRepository, logger, jwtService, userManager, roleManager, signInManager)
        {
        }
        public override Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            return base.Create(userDto, cancellationToken);
        }
        public override Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            return base.Delete(id, cancellationToken);
        }
        public override Task<ActionResult<List<User>>> GetAllUser(CancellationToken cancellationToken)
        {
            return base.GetAllUser(cancellationToken);
        }
        public override Task<ApiResult<User>> GetUserbyId(int id, CancellationToken cancellationToken)
        {
            return base.GetUserbyId(id, cancellationToken);
        }
        public override Task<ApiResult<User>> GetUserbyFullname(string fullname, CancellationToken cancellationToken)
        {
            return base.GetUserbyFullname(fullname, cancellationToken);
        }
        public override Task<ActionResult> Token([FromForm] TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            return base.Token(tokenRequest, cancellationToken);
        }
        public override Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            return base.Update(id, user, cancellationToken);
        }
    }
}
