using System;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private float movementSpeed;

    private Vector3 _movementVector;
    protected float boundX;
    private void Awake()
    {
        boundX = GameData.defaultScreenLimit + 2;
        CalculateMovementVector();
    }

    private void CalculateMovementVector()
    {
        _movementVector = new Vector3(movementSpeed, 0, 0);
        _movementVector = direction == Direction.Right ? _movementVector : -_movementVector;
    }

    private void Update()
    {
        transform.position += _movementVector * Time.deltaTime;
        
        var pos = transform.position;
        
        if (IsOutOfBounds(pos))
        {
            pos.x *= -1;
            //Prevents a bug where object teleports to left and right for a while
            pos.x = pos.x < 0 ? pos.x + .01f : pos.x - .01f; 
            transform.position = pos;
        }
    }

    protected virtual bool IsOutOfBounds(Vector3 pos)
    {
        return pos.x > boundX || pos.x < -boundX;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        CalculateMovementVector();
    }
#endif
}
