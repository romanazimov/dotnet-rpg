global using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;
namespace dotnet_rpg.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private static List<Character> characters = new()
    {
        new Character(),
        new Character(){Id = 1, Name = "Sam"}
    };

    [HttpGet("GetAll")]
    public ActionResult<List<Character>> Get()
    {
        return Ok(characters);
    }
    
    [HttpGet("{id}")]
    public ActionResult<List<Character>> GetSingle(int id)
    {
        return Ok(characters[id]);
    }
    
    [HttpPost]
    public ActionResult<List<Character>> AddCharacter(string Name, int HitPoints, int Strength, int Defence, int Intelligence, RpgClass rpgClass)
    {
        var character = new Character()
        {
            Id = characters.Count,
            Name = Name,
            HitPoints = HitPoints,
            Strength = Strength,
            Defence = Defence,
            Intelligence = Intelligence,
            Class = rpgClass
        };
        characters.Add(character);
        return Ok("Character has been created.");
    }

    [HttpDelete]
    public ActionResult<List<Character>> DeleteCharacter(int id)
    {
        if (characters.Any(character => character.Id == id))
        {
            return Ok($"Character with Id {id} has been deleted.");
        }
        return NotFound($"Character with Id {id} was not found.");
    }
}