using UnityEngine;
using VRTD;

public interface IProjectileSpawner
{
    public IProjectile SpawnProjectile(Transform spawnPoint);
    public IProjectile SpawnTurretProjectile(Transform spawnPoint);
}