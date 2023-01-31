using Godot;
using HellGroundArcade.Scripts.Constants;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scripts.Game.GameStates;

public class TestingZoneState : Node, IGameState
{
    public string StateName { get; set; } = GameStateConstants.TEST_ZONE_STATE;
    [Export(PropertyHint.File)] public PackedScene TestingZoneScene { get; set; }
    
    public void OnStateEnter()
    {
        MainGame.Instance.LoadLevel(TestingZoneScene);
    }

    public void OnStateExit()
    {
        throw new System.NotImplementedException();
    }
}