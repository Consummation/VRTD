
namespace VRTD
{
    public interface IProjectile
    {
        public void Initialize(float hitDamage);
        public IHitEffectSpawner HitEffectSpawner { set; }
    }
}