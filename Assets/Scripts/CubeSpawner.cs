using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField, Min(1)] private float _spawnDelay;
    [SerializeField, Min(1)] private float _minReleaseDelay;
    [SerializeField, Min(1)] private float _maxReleaseDelay;
    [SerializeField] private SpawnpointSelector _spawnpointSelector;

    public override void Initialize(SpawnerStatistics statistics)
    {
        base.Initialize(statistics);
        StartCoroutine(GetDelayed());
    }

    protected override void OnGet(Cube cube)
    {
        cube.transform.position = _spawnpointSelector.GetRandomPosition();
        base.OnGet(cube);
    }

    protected override void OnWorkedOut(Cube cube)
    {
        StartCoroutine(ReleaseDelayed(cube));
    }

    private IEnumerator GetDelayed()
    {
        var delay = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            Get();
            yield return delay;
        }
    }

    private IEnumerator ReleaseDelayed(Cube cube)
    {
        var delay = new WaitForSeconds(Random.Range(_minReleaseDelay, _maxReleaseDelay));

        yield return delay;

        Release(cube);
    }
}
