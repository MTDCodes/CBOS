using Microsoft.AspNetCore.Mvc;

namespace CBOS.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DiscordUserController(ILogger<DiscordUserController> logger) : ControllerBase
{ }
