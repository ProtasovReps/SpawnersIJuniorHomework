using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolReturnable<T>
{
    [SerializeField] private T _poolObjectPrefab;

    private ObjectPool<T> _pool;
    private SpawnerStatistics _spawnerStatistics;

    public event Action<Vector3> Released;

    public virtual void Initialize(SpawnerStatistics spawnerStatistics)
    {
        _spawnerStatistics = spawnerStatistics;
        CreatePool();
    }

    protected virtual void OnGet(T poolObject) => poolObject.gameObject.SetActive(true);

    protected virtual void OnWorkedOut(T poolObject) => Release(poolObject);

    protected void Get()
    {
        _pool.Get();
        _spawnerStatistics.IncreaseTotalAmount();
        _spawnerStatistics.SetActiveAmount(_pool.CountActive);
    }

    protected void Release(T poolObject)
    {
        _pool.Release(poolObject);
        _spawnerStatistics.SetActiveAmount(_pool.CountActive);
        Released?.Invoke(poolObject.transform.position);
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => Create(),
        actionOnGet: (poolObject) => OnGet(poolObject),
        actionOnRelease: (poolObject) => poolObject.gameObject.SetActive(false),
        actionOnDestroy: (poolObject) => Destroy(poolObject),
        maxSize: 5);
    }

    private T Create()
    {
        T newPoolObject = Instantiate(_poolObjectPrefab);

        _spawnerStatistics.IncreaseCreatedAmount();
        newPoolObject.WorkedOut += OnWorkedOut;
        return newPoolObject;
    }

    private void Destroy(T poolObject)
    {
        poolObject.WorkedOut -= OnWorkedOut;
        Destroy(poolObject);
    }
}
