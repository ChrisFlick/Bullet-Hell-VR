using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour {

    [Header("Hoop Rotation")]
    [SerializeField] private Transform _outerHoopTransform;
    [SerializeField] private float _rotateSpeed = 10;

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _movementDistance = 50f;
    [SerializeField] private float _movementCooldownTime = 10f;


    private bool _isMovingUp;
    private Vector3 _moveDirection;

    private bool _isMovementActive;

    private float _distanceMoved = 0;

    private float _timeSinceMovement = 0f;

    private void Start()
    {
        // Randomly pick either 0 or 1 then set the initial direction for the Flying Saucer to move.
        int randomNumber = Random.Range(0, 2);
        

        if (randomNumber == 0)
        {
            _isMovingUp = true;
        } else
        {
            _isMovingUp = false;
        }
    }

    private void Update()
    {
        // Rotate the outer hoop to give the Saucer a little animation.
        _outerHoopTransform.Rotate(0, 0, _rotateSpeed * Time.deltaTime);

        HandleMovementCooldown();
        HandleMovement();
    }

    private void HandleMovementCooldown()
    {
        if (_isMovementActive) return;

        _timeSinceMovement += Time.deltaTime;

        if (_timeSinceMovement < _movementCooldownTime) return;

        _timeSinceMovement = 0;
        _distanceMoved = 0;

        _moveDirection = RandomMoveDirection();
    }

    private void HandleMovement()
    {
        if (!_isMovementActive) return;

        float distance = _movementSpeed * Time.deltaTime;
        _distanceMoved += distance;

        transform.Translate(_moveDirection * distance);

        if (_distanceMoved < _movementDistance) return;

        _isMovementActive = false;
    }

    // Flying Saucers can move in 6 directions.
    // This method randomly choses one of the 6 directions.
    // Directions alternate between going up and down.
    private Vector3 RandomMoveDirection()
    {
        _isMovementActive = true;

        int lowerLimit = 0;
        
        // If the Saucer is moving up set the lower limit to 4 so that the direction picked is up.
        if (_isMovingUp) 
        {   
            lowerLimit = 4;
        }

        _isMovingUp = !_isMovingUp;


        int upperLimit = lowerLimit + 3;

        int randomDirection = Random.Range(lowerLimit, upperLimit);

        switch (randomDirection)
        {
            case 0: // Down and to the left.
                return new Vector3(0, -1, -1).normalized;
            case 1: // Straight down.
                return new Vector3(0, 0, -1).normalized;
            case 2: // Down and to the right.
                return new Vector3(0, 1, -1).normalized;
            case 3: // Up and to the left.
                return new Vector3(0, -1, 1).normalized;
            case 4: // Straight up.
                return new Vector3(0, 0, 1).normalized;
            case 5: // Up and to the right.
                return new Vector3(0, 1, 1).normalized;
            default:
                Debug.Log("Flying Saucer randomDirection was out of range");

                _isMovementActive = false;

                return new Vector3 (0, 0, 0);
        }
    }
}
