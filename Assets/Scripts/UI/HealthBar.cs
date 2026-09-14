using Health;
using Scripts.Player;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        [Inject] private PlayerView _playerView;
        
        [SerializeField] private Slider _slider;
        private HealthComponent _playerHealth;

        private CompositeDisposable _disposable = new();
        
        private void Awake()
        {
            _playerHealth = _playerView.GetComponent<PlayerInit>().Model.HealthComponent;
        }

        private void Start()
        {
            _playerHealth.CurrentHealth.Subscribe(v=>
            {
                if (v != 0)
                {
                    _slider.value = (float) v / _playerHealth.MaxHealth.Value;
                }
            }).AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable?.Clear();
        }
    }
}

