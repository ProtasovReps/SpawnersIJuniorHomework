using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour, IPoolReturnable<Cube>
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private Color _defaultColor;
    private bool _isColored;

    public event Action<Cube> WorkedOut;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.rotation = Quaternion.identity;
        _isColored = false;
        SetDefaultColor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isColored == false)
        {
            if (collision.gameObject.TryGetComponent<Platform>(out _))
            {
                SetRandomColor();
                _isColored = true;
                WorkedOut?.Invoke(this);
            }
        }
    }

    private void SetDefaultColor() => _renderer.material.color = _defaultColor;

    private void SetRandomColor() => _renderer.material.color = Random.ColorHSV();
}
