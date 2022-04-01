using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTD
{
    public class GunEffectSpawner : MonoBehaviour
    {
        [Header("Trigger effect")]
        [SerializeField] private GameObject triggerEffect;
        [SerializeField] private Transform triggerSpawnPoint;

        [Header("Muzzle flash effect")] 
        [SerializeField] private GameObject muzzleFlashEffect;
        [SerializeField] private Transform muzzleFlashSpawnPoint;

        private IPool<TemporaryMonoPooled> _poolTrigger;
        private IPool<TemporaryMonoPooled> _poolMuzzleFlash;

        private const int AMOUNT_EFFECT = 10;

        private void Start()
        {
            _poolTrigger = GetNewPool(triggerEffect, transform);
            _poolMuzzleFlash = GetNewPool(muzzleFlashEffect, transform);
        }

        public void SpawnTriggerEffect()
        {
            var trigger = _poolTrigger.Pull();
            SetSpawnPoint(trigger.transform, triggerSpawnPoint);
        }

        public void SpawnMuzzleEffect()
        {
            var muzzle = _poolMuzzleFlash.Pull();
            SetSpawnPoint(muzzle.transform, muzzleFlashSpawnPoint);
        }

        private void SetSpawnPoint(Transform main, Transform parent)
        {
            var transformMain = main.transform;
            transformMain.parent = transform;
            transformMain.position = parent.position;
            transformMain.rotation = parent.rotation;
        }

        private Pool<TemporaryMonoPooled> GetNewPool(GameObject prefab, Transform parent)
        {
            var factory = new FactoryMonoObject<TemporaryMonoPooled>(prefab, parent);
            var pool = new Pool<TemporaryMonoPooled>(factory, AMOUNT_EFFECT);
            return pool;
        }
    }
}