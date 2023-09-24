using Godot;

namespace McGd.world;

public partial class World : Node3D
{

	[Signal]
	public delegate void TogglePausedEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.PushWarning("Started World");
		GameManager manager = GetNode<GameManager>("/root/GameManager");
		manager.world = this;

	}

	private void onComplete()
	{
		GD.Print("On Complete");
	}
	
	private void dataHandle(string resp)
	{
		GD.Print("RESP ", resp);
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		
		if (@event.IsActionPressed("pause"))
		{
			PauseToggle();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void QuitGame()
	{
		GetTree().Quit();
	}
	
	private void ResumeGame()
	{
		PauseToggle();
	}

	private void PauseToggle()
	{
		EmitSignal(SignalName.TogglePaused);
	}
	
}
