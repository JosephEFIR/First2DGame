using System.Collections.Generic;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Managers
{
    [System.Serializable]
    public class ScreenEntry
    {
        [SerializeField] private EScreenType _type;
        [SerializeField] private Scripts.UI.Screen _screen; // явно указываем ваш Screen

        public EScreenType Type => _type;
        public Scripts.UI.Screen Screen => _screen;
    }

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private EScreenType _startScreen;
        [SerializeField] private List<ScreenEntry> _screenEntries = new();

        private Dictionary<EScreenType, Scripts.UI.Screen> _screens;
        private EScreenType _currentScreen;

        public Dictionary<EScreenType, Scripts.UI.Screen> Screens => _screens;

        private void Awake()
        {
            _screens = new Dictionary<EScreenType, Scripts.UI.Screen>();
            foreach (var entry in _screenEntries)
            {
                if (entry.Screen != null && !_screens.ContainsKey(entry.Type))
                    _screens.Add(entry.Type, entry.Screen);
            }
        }

        private void Start()
        {
            ChangeScreen(_startScreen);
        }

        public void ChangeScreen(EScreenType type)
        {
            if (!_screens.TryGetValue(type, out var newScreen))
            {
                Debug.LogError($"Screen of type {type} not found");
                return;
            }

            if (_screens.TryGetValue(_currentScreen, out var currentScreen))
                currentScreen.gameObject.SetActive(false);

            newScreen.gameObject.SetActive(true);
            _currentScreen = type;
        }
    }
}