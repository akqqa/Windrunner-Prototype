using Godot;
using System;

public partial class CameraController : Camera3D {

    private Player player;

    public void Init(Player player) {
        this.player = player;
    }
}
