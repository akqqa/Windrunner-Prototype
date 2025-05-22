using Godot;

//https://www.reddit.com/r/godot/comments/16r2i1m/developing_fps_controller_in_godot_using_c_need/
// This is just a floating camera. edit to add gravity etc.
public partial class Player : CharacterBody3D
{
	private Node3D head;
	private Camera3D view;

	private float cameraAngle = 0f;
	private float mouseSens = 0.1f;
	private float moveSpeed = 20f;

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
		head = GetNode<Node3D>("Head");
		view = GetNode<Camera3D>("Head/PlayerCamera");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_cancel"))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Walk();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is not InputEventMouseMotion motion) return;

		head.RotateY(Mathf.DegToRad(-motion.Relative.X * mouseSens));
		float change = -motion.Relative.Y * mouseSens;

		if (!((change + cameraAngle) < 90F) || !((change + cameraAngle) > -90F)) return;

		view.RotateX(Mathf.DegToRad(change));
		cameraAngle += change;
	}

	private void Walk()
	{
		Vector3 direction = new();
		Basis aim = view.GlobalTransform.Basis;

		if (Input.IsActionPressed("move_forward"))
			direction -= aim.Z;

		if (Input.IsActionPressed("move_back"))
			direction += aim.Z;

		if (Input.IsActionPressed("move_left"))
			direction -= aim.X;

		if (Input.IsActionPressed("move_right"))
			direction += aim.X;

		Velocity = direction.Normalized() * moveSpeed;
		MoveAndSlide();
	}
}



// Old movment

// public partial class Player : CharacterBody3D
// {
// 	// How fast the player moves in meters per second.
// 	[Export]
// 	public int Speed { get; set; } = 14;
// 	// The downward acceleration when in the air, in meters per second squared.
// 	[Export]
// 	public int FallAcceleration { get; set; } = 75;

// 	// Will need to have a gravity direction vector, as well as a gravity strength scalar. - -defaults to natural gravity. added to target velocity per frame
// 	// A list to store current lashings.
// 	// Will need a custom is on floor implementation to account for gravity vector
// 	// Create a basic first person camera, then implement the gravity rotation on top of it with more understanding

// 	private Vector3 _targetVelocity = Vector3.Zero;

// 	public override void _PhysicsProcess(double delta)
// 	{
// 		var direction = Vector3.Zero;

// 		if (Input.IsActionPressed("move_right"))
// 		{
// 			direction.X += 1.0f;
// 		}
// 		if (Input.IsActionPressed("move_left"))
// 		{
// 			direction.X -= 1.0f;
// 		}
// 		if (Input.IsActionPressed("move_back"))
// 		{
// 			direction.Z += 1.0f;
// 		}
// 		if (Input.IsActionPressed("move_forward"))
// 		{
// 			direction.Z -= 1.0f;
// 		}

// 		if (direction != Vector3.Zero)
// 		{
// 			direction = direction.Normalized();
// 			GetNode<Node3D>("Pivot").Basis = Basis.LookingAt(direction);
// 		}

// 		// Ground velocity
// 		_targetVelocity.X = direction.X * Speed;
// 		_targetVelocity.Z = direction.Z * Speed;

// 		// Vertical velocity
// 		if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
// 		{
// 			_targetVelocity.Y -= FallAcceleration * (float)delta;
// 		}

// 		// Moving the character
// 		Velocity = _targetVelocity;
// 		MoveAndSlide();
// 	}
// }
