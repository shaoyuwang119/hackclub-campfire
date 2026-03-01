using UnityEngine;

public class EelEnemy : Enemy
{
    // Update is called once per frame
    protected override void FixedUpdate()
    {
        speed = 0.6f;
        z = 5f;

        base.FixedUpdate();
        sprite.flipY = origin.x > actual_target.x;
    }
}
