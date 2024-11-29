
namespace F1H43C_EEJYN9.Entities.Interfaces;

public interface IPlayer
{
    string Name { get; }
    Board Board { get; }
    
    void PlaceShips();
    bool Fire(Coordinate target);
    bool IsALlShipSunk();
    void Turn(IPlayer enemy);
}