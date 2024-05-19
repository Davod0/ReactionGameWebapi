namespace WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
[ApiController]
[Route("highscores")]
public class LogicController : ControllerBase
{
    private readonly MyDbContext dBContext;
    private readonly ILogger _logger;
    public LogicController(MyDbContext myDbContext, ILogger<LogicController> logger)
    {
        dBContext = myDbContext;
        _logger = logger;
    }

    [HttpPost("")]
    public IActionResult PostHighscore(Highscore hS)
    {
        if (hS.PlayerName != null)
        {
            try
            {
                hS.CreatedAt = DateTime.Now;
                dBContext.Highscore.Add(hS);
                dBContext.SaveChanges();
                return Created("/highscore", $"{hS.Id}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error");
            }
        }
        return BadRequest("Failed to save the highscore");
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeleteAllHighscores()
    {
        try
        {
            dBContext.Highscore.RemoveRange(dBContext.Highscore);
            await dBContext.SaveChangesAsync();
            return Ok("All highscores are deleted");
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal Server Error");
        }

    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllHighscores()
    {
        try
        {
            return Ok(await dBContext.Highscore.OrderByDescending(Highscore => Highscore.Score).ToListAsync());
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal Server Error");
        }
    }

    [HttpGet("top10")]
    public async Task<IActionResult> GetTopTenHighscores()
    {
        try
        {
            return Ok(await dBContext.Highscore.OrderByDescending(highscore => highscore.Score).Take(10).ToListAsync());
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal Server Error");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHighscoreById(int id)
    {
        try
        {
            var highscore = await dBContext.Highscore.FindAsync(id);
            if (highscore == null)
            {
                return NotFound($"The highscore with ID {id} does not exist");
            }
            return Ok(highscore);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal Server Error");
        }
    }

    [HttpGet("byUserName/{name}")]
    public async Task<IActionResult> GetHighscoresByPlayerName(string name)
    {
        try
        {
            var highscores = await dBContext.Highscore.Where(highscore => highscore.PlayerName == name).ToListAsync();
            if (highscores == null || highscores.Count == 0)
            {
                return NotFound($"No highscores found for player: {name}");
            }
            return Ok(highscores);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal Server Error");
        }
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetHighscoreStatistics()
    {
        var totalHighscore = await dBContext.Highscore.CountAsync();

        var mostCommonDate = await dBContext.Highscore.GroupBy(highscore => highscore.CreatedAt)
                                                      .OrderByDescending(group => group.Count())
                                                      .Select(group => group.Key).FirstOrDefaultAsync();

        var mostCommonGame = await dBContext.Highscore.GroupBy(highscore => highscore.GameName)
                                                      .OrderByDescending(group => group.Count())
                                                      .Select(group => group.Key)
                                                      .FirstOrDefaultAsync();

        var statistics = new
        {
            TotalHighscore = totalHighscore,
            MostCommonDate = mostCommonDate,
            MostCommonGame = mostCommonGame
        };
        return Ok(statistics);
    }


}





