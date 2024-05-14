using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSaucer : MonoBehaviour {

    [SerializeField] private Transform _outerHoopTransform;
    [SerializeField] private float _rotateSpeed = 10;

    private void Update()
    {
        // Rotate the outer hoop to give the Saucer a little animation.
        _outerHoopTransform.Rotate(0 , 0 , _rotateSpeed * Time.deltaTime);
    }
}
