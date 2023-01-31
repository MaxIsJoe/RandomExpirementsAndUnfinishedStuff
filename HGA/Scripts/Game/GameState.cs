using System.Collections.Generic;
using System.Linq;
using Godot;
using HellGroundArcade.Scripts.Interfaces;

namespace HellGroundArcade.Scripts.Game;

public class GameState : Node
{
    private List<IGameState> _states = new List<IGameState>();

    public override void _Ready()
    {
        base._Ready();
        var children = GetChildren();
        foreach (var state in children)
        {
            if (state is IGameState c == false) continue;
            _states.Add(c);
        }
    }

    public void StartState(string stateName)
    {
        foreach (var gameState in _states.Where(gameState => stateName == gameState.StateName))
        {
            gameState.OnStateEnter();
            return;
        }

        GD.PrintErr($"[GameState/StartState] - Unable to find {stateName}!");
    }
}