using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private float _minReleaseDelay;
    [SerializeField] private float _maxReleaseDelay;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _spawnPoint;
    private float _startTransparancy = 1f;
    private float _endTransparancy = 0f;

    private void OnEnable()
    {
        _cubeSpawner.Released += OnReleased;
    }

    private void OnDisable()
    {
        _cubeSpawner.Released -= OnReleased;
    }

    protected override void OnGet(Bomb poolObject)
    {
        float transparancyChangeTime = Random.Range(_minReleaseDelay, _maxReleaseDelay);

        base.OnGet(poolObject);
        poolObject.transform.position = _spawnPoint;
        poolObject.StartSettingTransparancy(transparancyChangeTime, _startTransparancy, _endTransparancy);
    }

    protected override void OnWorkedOut(Bomb poolObject)
    {
        poolObject.Blow();
        base.OnWorkedOut(poolObject);
    }

    private void OnReleased(Vector3 spawnPoint)
    {
        _spawnPoint = spawnPoint;
        Get();
    }
}