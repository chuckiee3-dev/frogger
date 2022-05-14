using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _screenLimit;
    
    private float _movementDuration = .325f;
    private float _movementTimer;
    
    private bool _isMoving;
    private Vector3 _targetPos;
    
    private Camera _camera;
    private Vector3 _startPosition;
    private bool _canMove;
    private void Awake()
    {
        _camera = Camera.main;
        _startPosition = transform.position;
        if (_camera != null)
        {
            _screenLimit = _camera.orthographicSize -1f;
        }
        else
        {
            _screenLimit = GameData.defaultScreenLimit;
        }

        _canMove = true;
    }

    private void Update()
    {
        if(!_canMove) return;
        if (_isMoving)
        {
            ProcessMovement();
        }
        if(!_isMoving){
            ProcessMovementInput();
        }
    }

    private void ProcessMovement()
    {
        if ((transform.localPosition - _targetPos).sqrMagnitude > 0.0001f)
        {
            _movementTimer += Time.deltaTime;
            float percent = Mathf.Clamp01(_movementTimer / _movementDuration);
            transform.localPosition = Vector3.Lerp(transform.localPosition, _targetPos, percent);
        }
        else
        {
            transform.localPosition = _targetPos;
            PlayerActions.MovementEnd();
            _isMoving = false;
        }
    }

    private void ProcessMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.position.y < _screenLimit)
            {
                RotateUp();
                SetTargetPos(Vector2.up);
                PlayerActions.GoUp();
            }
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            //+1 to account for timer & life UI
            if(transform.position.y > -_screenLimit + 1f){
                RotateDown();
                SetTargetPos(Vector2.down);
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x < _screenLimit)
            {
                RotateRight();
                SetTargetPos(Vector2.right);
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x > -_screenLimit )
            {
                RotateLeft();
                SetTargetPos(Vector2.left);
            }
        }
    }

    private void RotateUp()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    private void RotateDown()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180);
    }
    private void RotateRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }
    private void RotateLeft()
    {
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void SetTargetPos(Vector3 movement)
    {
        PlayerActions.MovementStart();
        _movementTimer = 0f;
        _isMoving = true;
        var pos = transform.localPosition;
        if(transform.parent != null){
            pos.y = 0;
        }
        _targetPos = pos + movement;
    }

    private void ResetToStartState()
    {
        transform.parent = null;
        transform.position = _startPosition;
        RotateUp();
    }
    private void OnEnable()
    {
        GameActions.onPlayerFillSlot += ResetToStartState;
        GameActions.onTimeOver += ResetToStartState;
        GameActions.onPlayerLoseLife += DisableMovement;
        GameActions.onPlayerRevive += EnableMovement;
    }

    private void OnDisable()
    {
        GameActions.onPlayerFillSlot -= ResetToStartState;
        GameActions.onTimeOver -= ResetToStartState;
        GameActions.onPlayerLoseLife -= DisableMovement;
        GameActions.onPlayerRevive -= EnableMovement;
    }
    private void DisableMovement()
    {
        _canMove = false;
    }
    private void EnableMovement()
    {
        ResetToStartState();
        _canMove = true;
    }


}
