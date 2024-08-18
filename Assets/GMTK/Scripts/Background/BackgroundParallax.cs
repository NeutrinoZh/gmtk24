using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private Transform _targetObject;
    [SerializeField, Range(0f, 1f)] private float _scrollSpeed;

    void Update()
    {
        transform.position = new Vector3(_targetObject.position.x * (1 - _scrollSpeed), _targetObject.position.y * (1 - _scrollSpeed));
    }
}
