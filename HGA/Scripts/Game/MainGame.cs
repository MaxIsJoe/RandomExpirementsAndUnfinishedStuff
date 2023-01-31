using Godot;
using HellGroundArcade.Scenes.Instances.Entities;
using HellGroundArcade.Scripts.Constants;

namespace HellGroundArcade.Scripts.Game;

public class MainGame : Node
{
    public static MainGame Instance { get; private set; }
    public EntityBase CurrentPlayer = null;

    [Export] public NodePath LevelPath;
    [Export] public NodePath GameStatePath;

    public override void _Ready()
    {
        base._Ready();
        Instance = this;
        GetNode<GameState>(GameStatePath).StartState(GameStateConstants.TEST_ZONE_STATE);
    }

    public void LoadLevel(PackedScene scene)
    {
        var level = GetNode(LevelPath);
        foreach (var childOfMap in level.GetChildren())
        {
            if(childOfMap is not Node c) continue;
            c.QueueFree();
        }

        var newScene = scene.Instance();
        level.AddChild(newScene);
    }
}