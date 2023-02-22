using System.Text.Json.Serialization;

namespace dotnet_rpg.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum RpgClass
{
    Archer = 1,
    Mage = 2,
    Warrior = 3
}