using Godot;

namespace McGd.GUI;

public partial class PauseMenu : Control
{

	public enum ResponsesEnum
	{
		Resume,
		Quit
	}
	
	[Signal]
	public delegate void PauseMenuResultEventHandler(ResponsesEnum result);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.PushWarning("Pause menu");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnQuitButtonPressed()
	{
		EmitResult(ResponsesEnum.Quit);
	}
	
	
	public void OnResumeButtonPressed()
	{
		EmitResult(ResponsesEnum.Resume);
	}

	private void EmitResult(ResponsesEnum result)
	{
		EmitSignal(SignalName.PauseMenuResult, (int)result);
	}
}
