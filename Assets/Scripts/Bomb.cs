using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour, IPoolReturnable<Bomb>
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private Renderer _renderer;
    private Color _defaultColor;
    private Coroutine _coroutine;

    public event Action<Bomb> WorkedOut;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnEnable()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _renderer.material.color = _defaultColor;
    }

    public void StartSettingTransparancy(float targetTime, float startTransparancy, float endTransparancy)
    {
        _coroutine = StartCoroutine(SetTransparancySmoothly(targetTime, startTransparancy, endTransparancy));
    }

    public void Blow()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }
    }

    private IEnumerator SetTransparancySmoothly(float targetTime, float startTransparancy, float endTransparancy)
    {
        float elapsedTime = 0f;
        Color newTransparancyColor = _renderer.material.color;

        while (elapsedTime < targetTime)
        {
            newTransparancyColor.a = Mathf.Lerp(startTransparancy, endTransparancy, elapsedTime / targetTime);
            _renderer.material.color = newTransparancyColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        WorkedOut?.Invoke(this);
        _coroutine = null;
    }
}
