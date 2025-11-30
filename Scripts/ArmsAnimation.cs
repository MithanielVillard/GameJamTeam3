using Godot;
using System;

public partial class ArmsAnimation : AnimatedSprite2D
{
    
    private PlayerMovement _playerMovement;
	
    private Player _player;
    
    public override void _Ready()
    {
        _playerMovement = GetParent().GetParent<PlayerMovement>();
        _player = _playerMovement.GetParent<Player>();
        
        _playerMovement.MovementDirection += HandleDirection;
        _player.FireProjectile += OnFire;
        
		AnimationFinished += Stop;
        
    }
    
    private void HandleDirection(Vector2 dir)
    {
        switch (dir)
        {
            case (0,-1)://up
                OnUp();
                break;
            case (0,1)://down
                OnDown();
                break;
            case (1,0)://right
                OnRight();
                break;
            case (-1,0)://left
                OnLeft();
                break;
        }
    }
    
    public void OnFire()
    {
        Play();
    }
    public void OnUp()
    {
        PlayAnim("Up");
    }
    public void OnDown()
    {
        PlayAnim("Down");
    }
    public void OnRight()
    {
        PlayAnim("Right");
    }
    public void OnLeft()
    {
        PlayAnim("Left");
    }
	
    public void OnIdle()
    {
        Position = Vector2.Zero;
    }
	
    private void PlayAnim(string animationName)
    {
        if (animationName == null)
            return;
		
        if(Animation.ToString() == animationName)
            return;
		
        Play(animationName);
        Stop();
    }
}