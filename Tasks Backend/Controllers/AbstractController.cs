using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Domain.Models;
using Repositories.Interfaces;

namespace Tasks_Backend.Controllers;


public abstract class AbstractController : Controller
{
    [NonAction]
    public User GetUserFromContext()
    {
        return HttpContext.Items.Single(x => x.Key == "User").Value as User;
    }
}