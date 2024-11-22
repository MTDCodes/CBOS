using Microsoft.AspNetCore.Mvc;

namespace CBOS.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class GuildController(ILogger<GuildController> logger) : ControllerBase
{ }
