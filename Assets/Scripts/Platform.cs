using UnityEngine;

public class Platform : MonoBehaviour, ISpawnpointGettable
{
    public Vector3 Position => transform.position;
    public Vector3 Scale => transform.lossyScale;
}
