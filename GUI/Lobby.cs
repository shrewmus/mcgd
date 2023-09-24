using Godot;

namespace McGd.GUI;

public partial class Lobby : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.PushWarning("TEST");
		Button startButton = GetNode<Button>("VBoxContainer/StartButton");
		Button quitButton = GetNode<Button>("VBoxContainer/QuitButton");

		startButton.Pressed += this.startGame;
		startButton.GrabFocus();

		quitButton.Pressed += this.quitGame;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void startGame()
	{
		GetTree().ChangeSceneToFile("res://world/World.tscn");
	}

	public void quitGame()
	{
		GetTree().Quit();
	}
}
