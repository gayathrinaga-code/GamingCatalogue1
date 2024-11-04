using GamingCatalogue.Core.Model;
using GamingCatalogue.Services.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GamingCatalogue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        public readonly IGameDetailService _gameDetailService;

        public GamesController(ILogger<GamesController> logger, IGameDetailService gameDetailService)
        {
            _logger = logger;
            _gameDetailService = gameDetailService;
        }

        /// <summary>
        /// Get the list of Games
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetGamesList()
        {
            _logger.LogInformation("Start GetGamesList call");
            var gamesDetailsList = await _gameDetailService.GetAllGamesAsync();
            if (gamesDetailsList == null)
            {
                return NotFound();
            }

            _logger.LogInformation("GetGamesList call success responce" + '-' + gamesDetailsList.ToList());
            _logger.LogInformation("End GetGamesList call");
            return Ok(gamesDetailsList);

        }

        /// <summary>
        /// Get Game by id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetGameById([FromRoute] int id)
        {
            _logger.LogInformation("Start GetGameById call");
            var gamesDetails = await _gameDetailService.GetGameByIdAsync(id);

            if (gamesDetails != null)
            {

                _logger.LogInformation("GetGameById call success responce" + '-' + gamesDetails);
                _logger.LogInformation("End GetGameById call");
                return Ok(gamesDetails);
            }
            else
            {
                _logger.LogInformation("GetGameById call Bad request" + '-' + gamesDetails);
                _logger.LogInformation("End GetGameById call");
                return BadRequest();
            }
        }

        /// <summary>
        /// Add a new Game
        /// </summary>
        /// <param name="gameDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateGame(GameDetail gameDetails)
        {
            _logger.LogInformation("Start CreateGame call");
            var isGameCreated = await _gameDetailService.CreateGameAsync(gameDetails);

            if (isGameCreated)
            {
                _logger.LogInformation("End CreateGame call success");
                return CreatedAtAction(nameof(GetGameById), new { id = gameDetails.Id }, gameDetails);
            }
            else
            {
                _logger.LogInformation("End CreateGame call  Bad request");
                return BadRequest();
            }
        }

        /// <summary>
        /// Update the game
        /// </summary>
        /// <param name="gameDetails"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateGame(GameDetail gameDetails)
        {
            _logger.LogInformation("Start UpdateGame call");
            if (gameDetails != null)
            {
                var isGameUpdated = await _gameDetailService.UpdateGameAsync(gameDetails);
                if (isGameUpdated)
                {
                    _logger.LogInformation("End UpdateGame call success");
                    return Ok(isGameUpdated);
                }
                _logger.LogInformation("End CreateGame call  Bad request");
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete game by id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {
            _logger.LogInformation("Start DeleteGame call");
            var isDeleted = await _gameDetailService.DeleteGameAsync(id);

            if (isDeleted)
            {
                _logger.LogInformation("End DeleteGame call success");
                return Ok(isDeleted);
            }
            else
            {
                _logger.LogInformation("End DeleteGame call  Bad request");
                return BadRequest();
            }
        }

    }
}
