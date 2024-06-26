using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour {

    public static event EventHandler OnAnyHit;

    [Header("Hoop Rotation")]
    [SerializeField] private Transform _outerHoopTransform;
    [SerializeField] private float _rotateSpeed = 10;

    [Header("Movement")]
    [SerializeField] private float _movementSpeed = 20f;
    [SerializeField] private float _movementDistance = 10f;
    [SerializeField] private float _movementCooldownTime = 3f;

    [Header("Shooting")]
    [SerializeField] private Transform _bulletPrefab;
    [SerializeField] private float _firingCooldownTime = 1f;
    

    [Header("Sound")]
    [SerializeField] private AudioSource _blasterSFX;
    [SerializeField] private AudioSource _explosionSFX;

    [Header("Juice")]
    [SerializeField] private Transform _explosionVFXPrefab;


    private bool _isMovingUp;
    private Vector3 _moveDirection;

    private bool _isMovementActive;

    private float _distanceMoved = 0;

    private float _timeSinceMovement = 0f;
    private float _timeSinceFired = 0f;

    private void Start()
    {
        // Randomly pick either 0 or 1 then set the initial direction for the Flying Saucer to move.
        int randomNumber = UnityEngine.Random.Range(0, 2);
        

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
        HandleFiring();
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check to see if a bullet collides with this object then destroy it.
        if (other.gameObject.tag != "Bullet") return;


        Bullet bullet = other.gameObject.GetComponent<Bullet>();

        if (bullet == null) return;
        if (!bullet.IsCaught()) return;

        Debug.Log($"{this} Hit by bullet");

        Transform explosionVFX = Instantiate(_explosionVFXPrefab, this.transform);
        explosionVFX.parent = null;

        _explosionSFX.Play();
        _explosionSFX.transform.parent = null;

        OnAnyHit?.Invoke(this, EventArgs.Empty);

        GameObject.Destroy(bullet.gameObject);
        GameObject.Destroy(gameObject);
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

    private void HandleFiring()
    {
        _timeSinceFired += Time.deltaTime;

        if (_timeSinceFired < _firingCooldownTime) return;

        _timeSinceFired = 0;

        Instantiate(_bulletPrefab, transform.position, Quaternion.identity);

        _blasterSFX.Play();
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


        int upperLimit = lowerLimit + 2;

        int randomDirection = UnityEngine.Random.Range(lowerLimit, upperLimit);

        switch (randomDirection)
        {
            case 0: // Down and to the left.
                return new Vector3(0, -1, -1);
            case 1: // Straight down.
                return new Vector3(0, 0, -1);
            case 2: // Down and to the right.
                return new Vector3(0, 1, -1);
            case 3: // Up and to the left.
                return new Vector3(0, -1, 1);
            case 4: // Straight up.
                return new Vector3(0, 0, 1);
            case 5: // Up and to the right.
                return new Vector3(0, 1, 1);
            default:
                Debug.Log("Flying Saucer randomDirection was out of range");

                _isMovementActive = false;

                return new Vector3 (0, 0, 0);
        }
    }
}
