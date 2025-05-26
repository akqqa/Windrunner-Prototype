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
    public float jumpStrength = 10f;


    public void Init(Player player)
    {
        this.player = player;
    }

    public override void _PhysicsProcess(double delta)
    {
        targetVelocity = Vector3.Zero;

        // Get cameras forward and right vectors to base movement off of
        Vector3 direction = Vector3.Zero;
        Vector3 forward = player.GetCameraForward();
        Vector3 right = player.GetCameraRight();

        if (player.IsOnFloor())
        {
            if (Input.IsActionPressed("jump"))
            {

                targetVelocity += -player.gravityDirection * jumpStrength;
            }
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

            direction = direction.Normalized();
            direction = direction * moveSpeed;

            targetVelocity += direction;

        }
        else
        {
            targetVelocity += player.gravityDirection * player.gravityStrength * (float)delta;
        }

        player.Velocity = targetVelocity;
        player.MoveAndSlide();
    }
}
