using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;

using Pebbles.Models;
using Pebbles.Services;
using Pebbles.Repositories;


[ApiController]

public class DefaultController: ControllerBase
{
    private readonly IConfiguration _configuration;

    public DefaultController(IAnswerService answerService, IConfiguration configuration, IOptionService optionService)
    {
        _configuration = configuration;
    }


}
