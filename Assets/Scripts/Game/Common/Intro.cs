using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common
{
    public class Intro : MonoBehaviour
    {
        public int waitTime;

        private void Start()
        {
            StartCoroutine(WaitForMenu()); //bruh
        }

        IEnumerator WaitForMenu()
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene(1);
        }
    }
}