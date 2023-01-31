namespace HellGroundArcade.Scripts.Interfaces;

public interface IGameState
{
    public string StateName { get; set; }
    public void OnStateEnter();
    public void OnStateExit();
}