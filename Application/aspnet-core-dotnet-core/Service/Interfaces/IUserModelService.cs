using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestTechAwsLogin.Models;

namespace aspnet_core_dotnet_core.Service.Interfaces
{
    public interface IUserModelService
    {
        List<UserModel> Get();
        UserModel Get(string id);
        UserModel Create(UserModel user);
        void Update(string id, UserModel userToUpdate);
        void Remove(string userId);
    }
}
