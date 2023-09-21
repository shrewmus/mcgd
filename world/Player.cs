using Godot;
using System;

public partial class Player : CharacterBody3D
{
	// move stuff
	public float currentSpeed = 5.0f;
	public const float walkingSpeed = 5.0f;
	public const float sprintingSpeed = 10.0f;
	public const float croachingSpeed = 3.0f;
	public const float jumpVelocity = 5.5f;
	public const float mouseSensitivity = 0.25f;
	public const float lerpSpeed = 10.0f;
	public Vector3 direction = Vector3.Zero;
	
	public Node3D head;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		base._Ready();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		head = GetNode("Head") as Node3D;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		
		if (@event is InputEventMouseMotion motion)
		{
			RotateY(Mathf.DegToRad(- motion.Relative.X) * mouseSensitivity);
			head.RotateX(Mathf.DegToRad(-motion.Relative.Y) * mouseSensitivity);
			
			// limit mouse rotation (prevent full circle rotation) 
			var rotation = head.Rotation;
			rotation.X = Mathf.Clamp(head.Rotation.X, Mathf.DegToRad(-89), Mathf.DegToRad(89));
			head.Rotation = rotation;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		if (Input.IsActionPressed("spint"))
		{
			currentSpeed = sprintingSpeed;
		}
		else
		{
			currentSpeed = walkingSpeed;
		}
			
		
		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = jumpVelocity;

		// get direction vector
		Vector2 inputDir = Input.GetVector("left", "right", "forward", "backward");
		
		// calculate
		var newDirection = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		direction = direction.Lerp(newDirection, Convert.ToSingle(delta * lerpSpeed));
		
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * currentSpeed;
			velocity.Z = direction.Z * currentSpeed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, currentSpeed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, currentSpeed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
