using Health;
using Scripts.Managers;
using Scripts.UI;
using Zenject;


namespace Scripts.Health
{
    public sealed class PlayerHealth : HealthComponent
    {
        [Inject] private UIManager _uiManager;
        
        public override void OnDeath()
        {
            _uiManager.ChangeScreen(EScreenType.Defeat);
        }
    }
}