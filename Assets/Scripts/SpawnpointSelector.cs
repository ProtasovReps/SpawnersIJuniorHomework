using UnityEngine;

[RequireComponent(typeof(ISpawnpointGettable))]
public class SpawnpointSelector : MonoBehaviour
{
    private ISpawnpointGettable _spawnpointGettable;

    private void Awake()
    {
        _spawnpointGettable = GetComponent<ISpawnpointGettable>();
    }

    public Vector3 GetRandomPosition()
    {
        float positionX = _spawnpointGettable.Position.x;
        float scaleX = _spawnpointGettable.Scale.x;
        float floorStart = positionX - (scaleX / 2);
        float floorEnd = positionX + (scaleX / 2);
        float RandomXPosition = Random.Range(floorStart, floorEnd);

        return new Vector3(RandomXPosition, _spawnpointGettable.Position.y, _spawnpointGettable.Position.z);
    }
}
