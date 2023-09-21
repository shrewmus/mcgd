using Godot;
using System;

enum BlockType
{
	AIR,
	DIRT,
	GRASS,
	STONE,
	LOG1,
	LEAVES1,
	WOOD1,
	LOG2,
	LEAVES2,
	WOOD2,
	GLASS,
	STUMP, // some special block
}

public partial class WorldGeneration : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
