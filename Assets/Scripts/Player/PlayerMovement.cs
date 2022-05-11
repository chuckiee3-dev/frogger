using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _screenLimit;
    
    private float _movementDuration = .325f;
    private float _movementTimer;
    
    private bool _isMoving;
    private Vector3 _targetPos;
    
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
        if (_camera != null)
        {
            _screenLimit = _camera.orthographicSize -1f;
        }
        else
        {
            _screenLimit = GameData.defaultScreenLimit;
        }
    }

    private void Update()
    {
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
        if ((transform.position - _targetPos).sqrMagnitude > 0.0001f)
        {
            _movementTimer += Time.deltaTime;
            float percent = Mathf.Clamp01(_movementTimer / _movementDuration);
            transform.position = Vector3.Lerp(transform.position, _targetPos, percent);
        }
        else
        {
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
            }
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(transform.position.y > -_screenLimit){
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
            if (transform.position.x > -_screenLimit)
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
        _targetPos = transform.position + movement;
    }
}
