using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _killDistance = 20f;

    private Vector3 _direction;

    private Vector3 _destroyLocation;

    private void Start()
    {   
        Vector3 cameraLocation = Camera.main.transform.position;
        _direction = cameraLocation - transform.position;

        // Destroy the bullet once it gets close to this location;
        _destroyLocation = cameraLocation + (_direction * _killDistance);
    }

    private void Update()
    {   
        transform.Translate(_direction * _bulletSpeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, _destroyLocation);

        if (distance < _bulletSpeed)
        {
            Destroy(gameObject);
        }
    }
}
