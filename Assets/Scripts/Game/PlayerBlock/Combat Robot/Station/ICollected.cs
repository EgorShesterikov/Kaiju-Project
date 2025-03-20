using System;
using UnityEngine;

namespace Kaiju
{
    public interface ICollected
    {
        bool IsTacked { get; }

        void Collected(Vector3 target, Action<float> callBack);
        void Drop();
    }
}