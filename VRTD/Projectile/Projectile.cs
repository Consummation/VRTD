using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace VRTD
{
    public class Projectile : MonoPooled,IProjectile
    {
        [SerializeField] private float speed;
        [SerializeField] private float timeBeforeDestruction;

        private float _hitDamage;
    

        public IHitEffectSpawner HitEffectSpawner { get; set; }
    

        private void FixedUpdate()
        {
        
            transform.position = Vector3.MoveTowards(transform.position, transform.position+transform.forward,speed*Time.deltaTime);
        }

        public void Initialize(float hitDamage)
        {
            _hitDamage = hitDamage;
            StartCoroutine(Destroy());
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void ReturnToPool()
        {
            base.ReturnToPool();
            StopAllCoroutines();
        }

        private void OnTriggerEnter(Collider other)
        {
            var damageable = other.GetComponent<IDamageable>();
            if (damageable is {NpcType: NPCType.Enemy})
            {
                damageable.TakeDamage(_hitDamage);
            }
            HitEffectSpawner.SpawnHitEffect(transform);
            ReturnToPool();
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(timeBeforeDestruction);
            ReturnToPool();
        }
    }
}