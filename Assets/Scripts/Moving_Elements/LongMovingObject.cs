using UnityEngine;

public class LongMovingObject : MovingObject
{
    [SerializeField] private float extraIndent;
    protected override bool IsOutOfBounds(Vector3 pos)
    {
        return pos.x > boundX + extraIndent/2 + .5f || pos.x < -boundX - extraIndent/2 -.5f;
    }
}
