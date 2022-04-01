using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRTD
{
    public class ProjectileSpawner :  IProjectileSpawner, IHitEffectSpawner
    {
        private IPool<MonoPooled> _poolProjectiles;
        private IPool<MonoPooled> _poolTurretProjectiles;
        private IPool<TemporaryMonoPooled> _poolHitEffects;

        public void Initialize(Transform parentTransform, Projectile prefab, Projectile turretPrefab,TemporaryMonoPooled hitEffectPrefab, int amountProjectile)
        {
            var factoryProjectile = new FactoryMonoObject<MonoPooled>(prefab.gameObject,parentTransform);
            _poolProjectiles = new Pool<MonoPooled>(factoryProjectile, amountProjectile);
            
            var factoryTurretProjectile = new FactoryMonoObject<MonoPooled>(turretPrefab.gameObject,parentTransform);
            _poolTurretProjectiles = new Pool<MonoPooled>(factoryTurretProjectile, amountProjectile);

            var factoryHitEffect = new FactoryMonoObject<TemporaryMonoPooled>(hitEffectPrefab.gameObject, parentTransform);
            _poolHitEffects = new Pool<TemporaryMonoPooled>(factoryHitEffect, amountProjectile);
        }
        public IProjectile SpawnProjectile(Transform spawnPoint)
        {
            var newBullet = _poolProjectiles.Pull();
            var transformProjectile = newBullet.transform;
            transformProjectile.position = spawnPoint.position;
            transformProjectile.rotation = spawnPoint.rotation;
            var projectile = newBullet.GetComponent<IProjectile>();
            projectile.HitEffectSpawner = this;
            return projectile;
        }

        public IProjectile SpawnTurretProjectile(Transform spawnPoint)
        {
            var newBullet = _poolTurretProjectiles.Pull();
            var transformProjectile = newBullet.transform;
            transformProjectile.position = spawnPoint.position;
            transformProjectile.rotation = spawnPoint.rotation;
            var projectile = newBullet.GetComponent<IProjectile>();
            projectile.HitEffectSpawner = this;
            return projectile;
        }

        public void SpawnHitEffect(Transform spawnPoint)
        {
            var newEffect = _poolHitEffects.Pull().transform;
            if (Physics.Raycast(spawnPoint.position, spawnPoint.forward,out RaycastHit hit, 1f))
            {
                newEffect.position = hit.point;
                newEffect.rotation = Quaternion.LookRotation(-hit.normal);
                newEffect.position -= newEffect.forward * 0.01f;
            }
            else
            {
                newEffect.position = spawnPoint.position;
                newEffect.rotation = spawnPoint.rotation;
                newEffect.position -= newEffect.forward * 0.04f;
            }
        }
    }
}
