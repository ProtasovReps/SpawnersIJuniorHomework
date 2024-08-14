using System;
using UnityEngine;

public interface IPoolReturnable<T> where T : MonoBehaviour
{
    public event Action<T> WorkedOut;
}
