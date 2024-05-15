using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _killDistance = 20f;

    [SerializeField] private Material _caughtMaterial;

    private Vector3 _direction;

    private Vector3 _destroyLocation;

    bool _isCaught = false;

    private void Start()
    {   
        Vector3 cameraLocation = Camera.main.transform.position;
        _direction = cameraLocation - transform.position;

        // Destroy the bullet once it gets close to this location;
        _destroyLocation = cameraLocation + (_direction * _killDistance);
    }

    private void Update()
    {
        if (_isCaught) return;

        transform.Translate(_direction * _bulletSpeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, _destroyLocation);

        if (distance < _bulletSpeed)
        {
            Destroy(gameObject);
        }
    }

    public void OnSelectEnter()
    {
        // Used to stop the movement when the Bullet is caught
        _isCaught = true;

        Renderer renderer = GetComponent<Renderer>();

        var materialsCopy = renderer.materials;
        materialsCopy[0] = _caughtMaterial;
        GetComponent<Renderer>().materials = materialsCopy;
    }
}
