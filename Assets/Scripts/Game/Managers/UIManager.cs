using System;
using System.Collections.Generic;
using Scripts.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Screen = Scripts.UI.Screen;


namespace Scripts.Managers
{
    public class UIManager : SerializedMonoBehaviour
    {
        [LabelText("Стартовый экран")]
        [SerializeField] private EScreenType _startScreen;
        [LabelText("Экраны")]
        [SerializeField] private Dictionary<EScreenType, Screen> _screens = new();

        private EScreenType _currentScreen;

        public Dictionary<EScreenType, Screen> Screens => _screens;

        private void Start()
        {
            _currentScreen = _startScreen;
            ChangeScreen(_startScreen);
        }

        public void ChangeScreen(EScreenType type)
        {
            _screens[_currentScreen]?.gameObject.SetActive(false);
            _screens[type].gameObject.SetActive(true);
        }
    }
}