using TMPro;
using UnityEngine;

public class SpawnerStatisticsView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private SpawnerStatistics _statistics;

    private void OnEnable()
    {
        if (_statistics != null)
            _statistics.CountChanged += ShowStatistics;
    }

    private void OnDisable()
    {
        _statistics.CountChanged -= ShowStatistics;
    }

    public void Initialize(SpawnerStatistics statistics)
    {
        _statistics = statistics;
        _statistics.CountChanged += ShowStatistics;
        ShowStatistics();
    }

    private void ShowStatistics()
    {
        string statistics = $"����� ����������: {_statistics.TotalSpawnedCount}" +
                            $"\n����� �������: {_statistics.CreatedCount}" +
                            $"\n�������� ��������: {_statistics.ActiveCount}";

        _text.text = statistics;
    }
}
