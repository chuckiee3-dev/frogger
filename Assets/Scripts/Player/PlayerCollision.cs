using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private LayerMask dangerLayerMask;
    [SerializeField] private float collisionSize;
    private void CheckForCollisions()
    {
        if (Physics2D.OverlapBox(transform.position, Vector2.one * collisionSize, 0,dangerLayerMask))
        {
            PlayerActions.PlayerLoseLife();
            Debug.Log("Lose life");
        }
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
