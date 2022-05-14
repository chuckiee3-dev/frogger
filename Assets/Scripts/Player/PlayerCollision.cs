using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private LayerMask dangerLayerMask;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask waterLayerMask;
    [SerializeField] private LayerMask frogSlotLayerMask;
    [SerializeField] private float collisionSize;

    private TurtleGroup _currentTurtleGroup;
    private void CheckForCollisions()
    {
        if (Physics2D.OverlapBox(transform.position, Vector2.one * collisionSize, 0,dangerLayerMask))
        {
            GameActions.PlayerLoseLife();
        }
        else{
            Collider2D hitFrogSlot = Physics2D.OverlapBox(transform.position, Vector2.one * collisionSize, 0,frogSlotLayerMask);
            Collider2D hitPlatform = Physics2D.OverlapBox(transform.position, Vector2.one * collisionSize, 0,platformLayerMask);
            Collider2D hitWater = Physics2D.OverlapBox(transform.position, Vector2.one * collisionSize, 0,waterLayerMask);
            if (hitFrogSlot)
            {
                FrogSlot slot;
                if (hitFrogSlot.TryGetComponent(out slot))
                {
                    slot.TryFillSlot();
                }
            }
            else if (hitPlatform)
            {
                AttachPlayerToPlatform(hitPlatform.transform);
            }
            else if(hitWater)
            {
                UnsubscribeFromPlatformIfValid();
                transform.parent = null;
                GameActions.PlayerLoseLife();
            }
            else
            {
                UnsubscribeFromPlatformIfValid();
                transform.parent = null;
            }
        }
    }

    private void AttachPlayerToPlatform(Transform platform)
    {
        transform.parent = platform;
        UnsubscribeFromPlatformIfValid();
        StartListenToTurtleEventIfValid(platform);
    }

    private void StartListenToTurtleEventIfValid(Transform platform)
    {
        if (platform.TryGetComponent(out _currentTurtleGroup))
        {
            _currentTurtleGroup.onEndGoingDown += LoseLife;
        }
    }

    private void UnsubscribeFromPlatformIfValid()
    {
        if (_currentTurtleGroup)
        {
            _currentTurtleGroup.onEndGoingDown -= LoseLife;
        }
    }

    private void LoseLife()
    {
        transform.parent = null;
        GameActions.PlayerLoseLife();
    }

    private void OnEnable()
    {
        PlayerActions.onMovementEnd += CheckForCollisions;
    }


    private void OnDisable()
    {
        PlayerActions.onMovementEnd -= CheckForCollisions;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position, Vector3.one * collisionSize);
    }
#endif
}
