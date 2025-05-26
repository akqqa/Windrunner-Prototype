using Godot;
using System;
using System.Runtime.CompilerServices;

// Basic movement. Not acceleration based - check previous notes on best way of implementing player acceleration and movement.

public partial class MovementController : Node3D
{
    private Player player;
    private Vector3 targetVelocity = Vector3.Zero;

    [Export]
    public float moveSpeed = 10f;
    [Export]
    public float jumpStrength = 4f;
    [Export]
    public float airAcceleration = 1f;


    public void Init(Player player)
    {
        this.player = player;
    }

    public override void _PhysicsProcess(double delta)
    {
        float dt = (float)delta;

        // Get cameras forward and right vectors to base movement off of
        Vector3 direction = Vector3.Zero;
        Vector3 forward = player.GetCameraForward();
        Vector3 right = player.GetCameraRight();

        // Get directional input
        if (Input.IsActionPressed("move_forward"))
        {
            direction += forward;
        }
        if (Input.IsActionPressed("move_back"))
        {
            direction -= forward;
        }
        if (Input.IsActionPressed("move_right"))
        {
            direction += right;
        }
        if (Input.IsActionPressed("move_left"))
        {
            direction -= right;
        }

        direction = direction.Normalized() * moveSpeed;

        // Get current Velocities - horizonal and vertical with respect to the gravity direction
        Vector3 currentVelocity = player.Velocity;
        Vector3 horizontalVelocity = currentVelocity.Slide(player.gravityDirection);
        Vector3 verticalVelocity = currentVelocity.Project(player.gravityDirection);


        if (player.IsOnFloor()) // In future will have to ensure no air control with lashings? or minimal. idk. Will also need to override this method based on gravity direction.
        {
            if (Input.IsActionPressed("jump"))
            {
                verticalVelocity = -player.gravityDirection * jumpStrength; // Applies a jump impulse
            }

            horizontalVelocity = horizontalVelocity.Lerp(direction, player.gravityStrength * dt);
        } else {
            verticalVelocity += player.gravityDirection * player.gravityStrength * dt;

            horizontalVelocity = horizontalVelocity.Lerp(direction, airAcceleration * dt); // Applies gravity to weaken the jump impulse
        }

        player.Velocity = horizontalVelocity + verticalVelocity;
        player.MoveAndSlide();
    }
}
