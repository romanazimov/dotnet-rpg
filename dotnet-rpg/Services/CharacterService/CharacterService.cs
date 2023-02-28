namespace dotnet_rpg.Services.CharacterService;

public class CharacterService : ICharacterService
{
    // private static List<Character> characters = new()
    // {
    //     new Character(),
    //     new Character(){Id = 1, Name = "Sam"}
    // };

    private readonly IMapper _mapper;
    private readonly DataContext _context;
    
    public CharacterService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
    {
        var dbCharacters = await _context.Characters.ToListAsync();
        var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>
        {
            Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList()
        };
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
        var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(dbCharacter);
        return serviceResponse;

    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharacter(AddCharacterRequestDto newCharacter)
    {

        var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
        
        var character = _mapper.Map<Character>(newCharacter);
        character.Id = _context.Characters.Max(c => c.Id) + 1;
        _context.Characters.Add(_mapper.Map<Character>(character));
        await _context.SaveChangesAsync();
        
        serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterResponseDto>>();
        var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

        if (dbCharacter is null) throw new ArgumentException($"Character with ID {id} does not exist");
    
        _context.Characters.Remove(dbCharacter);
        await _context.SaveChangesAsync();
        serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(
        UpdateCharacterRequestDto updatedCharacter)
    {
        var serviceResponse = new ServiceResponse<GetCharacterResponseDto>();
        try
        {
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            if (dbCharacter is null) throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");

            dbCharacter.Name = updatedCharacter.Name;
            dbCharacter.HitPoints = updatedCharacter.HitPoints;
            dbCharacter.Intelligence = updatedCharacter.Intelligence;
            dbCharacter.Strength = updatedCharacter.Strength;
            dbCharacter.Defence = updatedCharacter.Defence;
            dbCharacter.Class = updatedCharacter.Class;
            await _context.SaveChangesAsync();

            serviceResponse.Message = "Your character has been saved.";
            serviceResponse.Data = _mapper.Map<GetCharacterResponseDto>(dbCharacter);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        } 
        return serviceResponse;
    }
}