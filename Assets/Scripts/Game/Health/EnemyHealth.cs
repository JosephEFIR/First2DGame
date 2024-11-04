using Health;

namespace Scripts.Health
{
    public sealed class EnemyHealth : HealthComponent
    {
        public override void OnDeath()
        {
            gameObject.SetActive(false);
        }
    }
}