using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera _camera;
    private float _screenLimit;
    private void Awake()
    {
        _camera = Camera.main;
        if (_camera != null)
        {
            _screenLimit = _camera.orthographicSize -1f;
        }
        else
        {
            //Defaults to 8 camera size
            _screenLimit = 6.5f;
        }
    }

    private void Update()
    {
        ProcessMovementInput();
    }

    private void ProcessMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (transform.position.y < _screenLimit)
            {
                RotateUp();
                Move(Vector2.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(transform.position.y > -_screenLimit){
                RotateDown();
                Move(Vector2.down);
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x < _screenLimit)
            {
                RotateRight();
                Move(Vector2.right);
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x > -_screenLimit)
            {
                RotateLeft();
                Move(Vector2.left);
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

    private void Move(Vector3 movement)
    {
        transform.position += movement;
    }
}
