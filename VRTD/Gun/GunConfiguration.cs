using UnityEngine;

namespace VRTD
{
    [CreateAssetMenu(menuName = "Configurations/SaveConfigurations/GunConfiguration", fileName = "GunConfiguration")]
    public class GunConfiguration : ScriptableObject
    {
        [SerializeField] private float hitDamage;
        [SerializeField] private float shotCooldown;
        [SerializeField] private float respawnCooldown;

        public float HitDamage => hitDamage;

        public float ShotCooldown => shotCooldown;

        public float RespawnCooldown => respawnCooldown;
    }
}