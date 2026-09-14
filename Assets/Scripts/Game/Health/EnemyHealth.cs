using Health;

namespace Scripts.Health
{
    public sealed class EnemyHealth : HealthComponent
    {
        public override void OnDeath()
        {
            _view.gameObject.SetActive(false);
        }
    }
}