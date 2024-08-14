using System;

public class SpawnerStatistics
{
    public event Action CountChanged;
    
    public float TotalSpawnedCount {  get; private set; }
    public float CreatedCount { get; private set; }
    public float ActiveCount {  get; private set; }

    public void IncreaseTotalAmount()
    {
        TotalSpawnedCount++;
        CountChanged?.Invoke();
    }

    public void IncreaseCreatedAmount()
    {
        CreatedCount++;
        CountChanged?.Invoke();
    }

    public void SetActiveAmount(float activeAmount)
    {
        ActiveCount = activeAmount;
        CountChanged?.Invoke();
    }
}
