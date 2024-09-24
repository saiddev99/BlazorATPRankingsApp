using BlazorATPRankingsAPI.DTO;
using BlazorATPRankingsAPI.Models;
using BlazorATPRankingsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;

namespace BlazorATPRankingsAPI.Controllers;

[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet("")]
    public ActionResult GetPlayers()
    {
        try
        {
            return Ok(_playerService.GetPlayers());
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Service error");
        }
    }

    [HttpGet("search")]
    public ActionResult SearchPlayers(string name)
    {
        try
        {
            return Ok(_playerService.SearchPlayers(name));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Service error");
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<Player> GetPlayer(int id)
    {
        try
        {
            var result = _playerService.GetPlayer(id);
            if (result == null) return NotFound();
            return result;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Service error");
        }
    }

    [HttpPost("add")]
    public ActionResult<Player> AddPlayer(PlayerDTO player)
    {
        try
        {
            var createdPlayer = _playerService.AddPlayer(player);
            return CreatedAtAction(nameof(AddPlayer), new { id = createdPlayer.Id }, createdPlayer);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Database error");
        }
    }

    [HttpPut("edit/{id:int}")]
    public ActionResult<Player> UpdatePlayer(int id, PlayerDTO player)
    {
        try
        {
            var playerToUpdate = _playerService.GetPlayer(id);

            if (playerToUpdate == null)
            {
                return NotFound($"Player with id = {id} is not found");
            }

            return _playerService.UpdatePlayer(id, player);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error updating data");
        }
    }

    [HttpDelete("delete/{id:int}")]
    public ActionResult<Player> DeletePlayer(int id)
    {
        try
        {
            var playerToDelete = _playerService.GetPlayer(id);
            if (playerToDelete == null)
            {
                return NotFound($"Player with id = {id} is not found");
            }
            return _playerService.DeletePlayer(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error deleting data");
        }
    }
}
