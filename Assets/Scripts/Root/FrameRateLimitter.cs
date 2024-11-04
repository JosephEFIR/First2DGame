using System;
using UnityEngine;

namespace Scripts.Root
{
    public class FrameRateLimitter : MonoBehaviour
    {
        [SerializeField] private int frameRate;
        private void Awake()
        {
            Application.targetFrameRate = frameRate;
        }

        private void OnValidate()
        {
            if (frameRate < 30)
            {
                frameRate = 30;
            }
        }
    }
}