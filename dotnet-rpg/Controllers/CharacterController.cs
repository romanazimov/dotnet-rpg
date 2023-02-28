global using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;
namespace dotnet_rpg.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;
    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> Get()
    {
        return Ok(await _characterService.GetAllCharacters());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> GetSingle(int id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }

    [HttpDelete]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> DeleteCharacter(int id)
    {
        return Ok(await _characterService.DeleteCharacter(id));
    }
    
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> UpdateCharacter(AddCharacterRequestDto character)
    {
        var response = await _characterService.AddCharacter(character);
        if (response.Data is null) return NotFound(response);
        return Ok(response);
    }
    
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> UpdateCharacter(UpdateCharacterRequestDto character)
    {
        var response = await _characterService.UpdateCharacter(character);
        if (response.Data is null) return NotFound(response);
        return Ok(response);
    }
}