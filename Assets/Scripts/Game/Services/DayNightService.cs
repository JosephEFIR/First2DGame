using DG.Tweening;
using UnityEngine;

namespace Scripts.Services
{
    public class DayNightService : MonoBehaviour
    {
        [SerializeField] private Transform _dayPoint;
        [SerializeField] private Transform _nightPoint;

        public bool IsDay { get; private set; }


        private void Start()
        {
            SetDay();
        }

        private void Update()
        {
            if (gameObject.transform.position == _dayPoint.position)
            {
                DayCycle();
            }
            else if(gameObject.transform.position == _nightPoint.position)
            {
                NightCycle();
            }
        }
        public void SetDay()
        {
            gameObject.transform.position = _dayPoint.position;
            IsDay = true;
        }

        public void SetNight()
        {
            gameObject.transform.position = _nightPoint.position;
            IsDay = false;
        }

        private void DayCycle()
        {
            transform.DOMove(_nightPoint.position, 200);
        }

        private void NightCycle()
        {
            transform.DOMove(_dayPoint.position, 200);
        }
    }
}