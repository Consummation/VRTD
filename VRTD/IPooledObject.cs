using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTD
{
    public interface IPooledObject
    {
        void ReturnToPool();
        void Initialize();
        void SetParentPool<T>(IPool<T> parent) where T : IPooledObject;
    }
}