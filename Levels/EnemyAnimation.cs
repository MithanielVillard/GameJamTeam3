using Godot;
using System;

public partial class EnemyAnimation : AnimatedSprite2D
{
	private EnemyMovement _enemyMovement;
    
    public override void _Ready()
    {
        _enemyMovement = GetParent<EnemyMovement>();
		
        _enemyMovement.MovementDirectionEnemy += HandleDirection;

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
