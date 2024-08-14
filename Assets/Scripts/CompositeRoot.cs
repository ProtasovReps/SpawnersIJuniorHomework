using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private ParticleSpawner _particleSpawner;
    [SerializeField] private SpawnerStatisticsView _cubeStatisticsView;
    [SerializeField] private SpawnerStatisticsView _bombStatisticsView;

    private SpawnerStatistics _cubeStatistics;
    private SpawnerStatistics _bombStatistics;

    private void Awake()
    {
        _cubeStatistics = new SpawnerStatistics();
        _bombStatistics = new SpawnerStatistics();

        _cubeSpawner.Initialize(_cubeStatistics);
        _bombSpawner.Initialize(_bombStatistics);
        _cubeStatisticsView.Initialize(_cubeStatistics);
        _bombStatisticsView.Initialize(_bombStatistics);
        _particleSpawner.Initialize(new SpawnerStatistics());
    }
}
