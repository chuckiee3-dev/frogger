using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private static readonly int MoveAnimKey = Animator.StringToHash("Move");
    private static readonly int DeathAnimKey = Animator.StringToHash("Death");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void PlayDeathAnim()
    {
        _animator.SetTrigger(DeathAnimKey);
    }
    private void PlayMoveAnim()
    {
        _animator.SetTrigger(MoveAnimKey);
    }

    private void OnEnable()
    {
        GameActions.onPlayerLoseLife += PlayDeathAnim;
        PlayerActions.onMovementStart += PlayMoveAnim;
    }


    private void OnDisable()
    {
        GameActions.onPlayerLoseLife -= PlayDeathAnim;
        PlayerActions.onMovementStart -= PlayMoveAnim;
    }
}
