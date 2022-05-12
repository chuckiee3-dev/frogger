using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private LayerMask dangerLayerMask;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask waterLayerMask;
    [SerializeField] private float collisionSize;

    private TurtleGroup _currentTurtleGroup;
    private void CheckForCollisions()
    {
        if (Physics2D.OverlapBox(transform.position, Vector2.one * collisionSize, 0,dangerLayerMask))
        {
            PlayerActions.PlayerLoseLife();
        }

        Collider2D hitPlatform = Physics2D.OverlapBox(transform.position, Vector2.one * collisionSize, 0,platformLayerMask);
        Collider2D hitWater = Physics2D.OverlapBox(transform.position, Vector2.one * collisionSize, 0,waterLayerMask);
        if (hitPlatform)
        {
            AttachPlayerToPlatform(hitPlatform.transform);
            Debug.Log("Attached to "+hitPlatform.transform.name);
        }
        else if(hitWater)
        {
            UnsubscribeFromPlatformIfValid();
            transform.parent = null;
            PlayerActions.PlayerLoseLife();
        }
        else
        {
            UnsubscribeFromPlatformIfValid();
            transform.parent = null;
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
        PlayerActions.PlayerLoseLife();
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
