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

    [HttpGet("Search")]
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

    [HttpPost("Add")]
    public ActionResult<Player> AddPlayer(Player contact)
    {
        try
        {
            var createdContact = _playerService.AddPlayer(contact);
            return CreatedAtAction(nameof(AddPlayer), new { id = createdContact.Id }, createdContact);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Database error");
        }
    }

    [HttpPut("edit/{id:int}")]
    public ActionResult<Player> UpdatePlayer(int id, Player player)
    {
        try
        {
            var playerToUpdate = _playerService.GetPlayer(id);

            if (playerToUpdate == null)
            {
                return NotFound($"Contact with id = {id} is not found");
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
    public ActionResult<Player> DeleteContact(int id)
    {
        try
        {
            var contactToDelete = _playerService.GetPlayer(id);
            if (contactToDelete == null)
            {
                return NotFound($"Contact with id = {id} is not found");
            }
            return _playerService.DeleteContact(id);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
            "Error deleting data");
        }
    }
}
