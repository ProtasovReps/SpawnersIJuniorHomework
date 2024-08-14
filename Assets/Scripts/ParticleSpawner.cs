using UnityEngine;

public class ParticleSpawner : Spawner<ParticlePlayer>
{
    [SerializeField] private BombSpawner _bombSpawner;

    private Vector3 _spawnPoint;

    private void OnEnable()
    {
        _bombSpawner.Released += OnReleased;
    }

    private void OnDisable()
    {
        _bombSpawner.Released -= OnReleased;
    }

    protected override void OnGet(ParticlePlayer particle)
    {
        base.OnGet(particle);
        particle.transform.position = _spawnPoint;
        particle.StartPlayingDelayed();
    }

    private void OnReleased(Vector3 spawnPoint)
    {
        _spawnPoint = spawnPoint;
        Get();
    }
}
