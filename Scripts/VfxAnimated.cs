using Godot;
using System;

public partial class VfxAnimated : AnimatedSprite2D
{
    public override void _Ready()
    {
        Play();
        AnimationFinished += QueueFree;
    }
    
}