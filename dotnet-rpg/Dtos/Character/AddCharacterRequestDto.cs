namespace dotnet_rpg.Dtos.Character;

public class AddCharacterRequestDto
{
    public string Name { get; set; } = "Roman";
    public int HitPoints { get; set; } = 100;
    public int Strength { get; set; } = 10;
    public int Defence { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public RpgClass Class { get; set; } = RpgClass.Warrior;
}