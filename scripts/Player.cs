using Godot;

//https://www.reddit.com/r/godot/comments/16r2i1m/developing_fps_controller_in_godot_using_c_need/
// This is just a floating camera. edit to add gravity etc.
public partial class Player : CharacterBody3D
{
	[Export]
	public float mouseSens = 0.1f;
	[Export]
	public Vector3 gravityDirection = Vector3.Down;
	[Export]
	public float gravityStrength = 9.8f;



	public CameraController Camera { get; private set; }
	public MovementController Movement { get; private set; }

	public override void _Ready()
	{
		Camera = GetNode<CameraController>("Head/PlayerCamera");
		Movement = GetNode<MovementController>("Pivot");

		Camera.Init(this);
		Movement.Init(this);

		UpDirection = -gravityDirection;
	}

	public Vector3 GetCameraForward()
	{
		return -Camera.GlobalTransform.Basis.Z;
	}

	public Vector3 GetCameraRight()
	{
		return Camera.GlobalTransform.Basis.X;
	}

}