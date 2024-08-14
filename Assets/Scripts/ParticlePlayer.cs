using System;
using System.Collections;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour, IPoolReturnable<ParticlePlayer>
{
    [SerializeField] private ParticleSystem _effect;

    private WaitForSeconds _delay;
    private Coroutine _coroutine;

    public event Action<ParticlePlayer> WorkedOut;

    private void Awake()
    {
        float totalDuration = _effect.main.duration + _effect.main.startLifetime.constant;
        
        _delay = new WaitForSeconds(totalDuration);
    }

    public void StartPlayingDelayed()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(PlayDelayed());
    }

    private IEnumerator PlayDelayed()
    {
        _effect.Play();
        yield return _delay;

        _effect.Stop();
        WorkedOut?.Invoke(this);
        _coroutine = null;
    }
}
