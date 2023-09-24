using Godot;
using System;
using McGd.GUI;
using McGd.world;

public partial class GameManager : Node
{
	private World _world;
	private GlobalEventService _globalEventService;
	private PauseMenu _pauseMenu;

	public World world
	{
		get { return _world;  }
		set
		{
			GD.Print("[TEST] set world");
			_world = value;
			_world.TogglePaused += () =>
			{
				var isPaused = GetTree().Paused;
				if (!isPaused)
				{
					OnGamePause();
				}
			};
			CreatePauseMenu();
		}
	}
	
	public override void _Ready()
	{
		_globalEventService = GetNode<GlobalEventService>("/root/GlobalEventService");
	}

	public override void _Process(double delta)
	{
		
	}

	private void OnGamePause()
	{
		GetTree().Paused = true;
		Input.MouseMode = Input.MouseModeEnum.Visible;
		_pauseMenu.Show();
	}

	private void OnGameUnpause()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		_pauseMenu.Hide();
		GetTree().Paused = false;
	}

	private void OnGameQuit()
	{
		GetTree().Quit();
	}

	private void CreatePauseMenu()
	{
		var menuScene = GD.Load<PackedScene>("res://GUI/PauseMenu.tscn");
		_pauseMenu = menuScene.Instantiate<PauseMenu>();
		GD.Print(_pauseMenu.Visible);
		world.GetNode("UI").AddChild(_pauseMenu);
		_pauseMenu.Hide();
		_pauseMenu.PauseMenuResult += (result) =>
		{
			switch (result)
			{
				case PauseMenu.ResponsesEnum.Quit:
					OnGameQuit();
					break;
				case PauseMenu.ResponsesEnum.Resume:
					OnGameUnpause();
					break;
			}
		};
	}
}
