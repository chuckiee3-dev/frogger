using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TurtleGroupAnimator))]
public class TurtleGroup : MonoBehaviour
{
    [SerializeField] private Vector2 safeDurationRange = new Vector2(3f,15f); 
    [SerializeField] private Vector2 unsafeDurationRange = new Vector2(1f,3f);

    private float _safeDuration;
    private float _unsafeDuration;
 
    private float _safeTimer;
    private float _unsafeTimer;
    
    private bool _isSafe;
    private bool _isAnimating;

    private WaitForSeconds _animWait;
    private WaitForSeconds _animMinusStepWait;
    private WaitForSeconds _stepWait;
    
    private TurtleGroupAnimator _animator;

    public bool IsSafe => _isSafe;
    
    public Action onStartGoingDown;
    public Action onEndGoingDown;
    public Action onStartGoingUp;
    public Action onEndGoingUp;
    private void Awake()
    {
        _animator = GetComponent<TurtleGroupAnimator>();
        _animWait = new WaitForSeconds(_animator.AnimationDuration);
        _safeDuration = Random.Range(safeDurationRange.x, safeDurationRange.y);
        _unsafeDuration = Random.Range(unsafeDurationRange.x, unsafeDurationRange.y);
        _isSafe = true;
    }

    private void Start()
    {
        _animMinusStepWait = new WaitForSeconds(_animator.AnimationDuration - _animator.StepDuration);
        _stepWait = new WaitForSeconds(_animator.StepDuration);
    }

    private void Update()
    {
        if(_isSafe)
        {
            ProgressSafeTimer();
        }
        else
        {
            ProgressUnsafeTimer();
        }
    }

    private void ProgressSafeTimer()
    {
        _safeTimer += Time.deltaTime;
        if (_safeTimer + _animator.AnimationDuration >= _safeDuration)
        {
            if (!_isAnimating)
            {
                StartGoingDown();
            }
        }
    }
    private void ProgressUnsafeTimer()
    {
        _unsafeTimer += Time.deltaTime;
        if (_unsafeTimer + _animator.AnimationDuration >= _unsafeDuration)
        {
            if (!_isAnimating)
            {
                StartGoingUp();
            }
        }
    }




    private void StartGoingDown()
    {
        _isAnimating = true;
        _animator.StartGoingDown();
        onStartGoingDown?.Invoke();
        StartCoroutine(EndGoingDownWithDelay());
    }
    private void StartGoingUp()
    {
        _isAnimating = true;
        _animator.StartGoingUp();
        onStartGoingUp?.Invoke();
        StartCoroutine(EndGoingUpWithDelay());
    }

    private IEnumerator EndGoingUpWithDelay()
    {
        yield return _stepWait;
        _isSafe = true;
        yield return _animMinusStepWait;
        onEndGoingUp?.Invoke();
        _isAnimating = false;
        _unsafeTimer = 0;
        
    }

    private IEnumerator EndGoingDownWithDelay()
    {
        yield return _animWait;
        onEndGoingDown?.Invoke();
        _isSafe = false;
        _isAnimating = false;
        _safeTimer = 0;
    }
}


