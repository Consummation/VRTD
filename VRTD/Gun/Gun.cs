using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTD.Input;
using Zenject;

namespace VRTD
{
    public class Gun : MonoClickable, ISelectable, IRespawnable
    {
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private Transform respawnPoint;

        private float _cooldown;
        private Rigidbody _rigidbody;
        private IGunAnimator _gunAnimator;
        private IProjectileSpawner _projectileSpawner;
        private IAudioController _currentAudioController;
        private IController _currentController;
        private GunConfiguration _gunConfiguration;
        private IPlayerConfiguration _playerConfiguration;
        public bool IsSelected { get; private set; }
        public IndicatedType IndicatedType { get; }
        public HandAnimationType HandAnimationType => HandAnimationType.TakeWeapon;
        public Vector3 IndicatedPosition => transform.position;
        [Inject]
        private void Construct(IProjectileSpawner projectileSpawner,IPlayerConfiguration playerConfiguration)
        {
            _playerConfiguration = playerConfiguration;
            _projectileSpawner = projectileSpawner;
           UpdateGunConfiguration();
        }

        private void Awake()
        {
            _currentAudioController = GetComponent<AudioController>();
            _gunAnimator = GetComponent<IGunAnimator>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_cooldown <= 0)
                return;
            _cooldown -= Time.deltaTime;
        }

        private void UpdateGunConfiguration()
        {
            _gunConfiguration = _playerConfiguration.GetCurrentGunConfiguration();
        }

        private void OnEnable()
        {
            _playerConfiguration.OnUpdateHand += UpdateGunConfiguration;
        }

        private void OnDisable()
        {
            _playerConfiguration.OnUpdateHand -= UpdateGunConfiguration;
        }

        public override void Click(IController controller)
        {
            if (!(_cooldown <= 0) || IsSelected == false) return;
            _currentController.SetVibration(_currentAudioController.PlayOneShot());
            controller.HandAnimatorController.PlayAnimation(HandAnimationTriggerType.Fire);
            var projectile = _projectileSpawner.SpawnProjectile(projectileSpawnPoint);
            projectile.Initialize(_gunConfiguration.HitDamage);
            _cooldown = _gunConfiguration.ShotCooldown;
            _playerConfiguration.AddScore(StatisticType.Shot);
            _gunAnimator.PlayFire();
        }

        public void Select(IController controller)
        {
            OnSelected?.Invoke(controller, true);
            StopAllCoroutines();
            _currentController = controller;
            IsSelected = true;
            _rigidbody.isKinematic = true;
            controller.Attach(transform);
            transform.localRotation = Quaternion.Euler(0, 90, 0);
            transform.localPosition = Vector3.zero;
        }

        public void Release(IController controller, Vector3 velocity)
        {
            OnSelected?.Invoke(controller, false);
            IsSelected = false;
            _rigidbody.isKinematic = false;
            controller.Detach(transform);
            _rigidbody.velocity = velocity;
            StartCoroutine(RespawnGun(_gunConfiguration.RespawnCooldown));
        }

        public event Action<IController, bool> OnSelected;

        IEnumerator RespawnGun(float cooldownAmount)
        {
            yield return new WaitForSeconds(cooldownAmount);
            Respawn();
        }

        public void Respawn()
        {
            transform.rotation = respawnPoint.rotation;
            transform.position = respawnPoint.position;
        }
    }
}