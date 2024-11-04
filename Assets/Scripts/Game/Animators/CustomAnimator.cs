using Scripts.Enums;
using UnityEngine;

namespace Scripts.Animators
{
    [RequireComponent(typeof(Animator))]
    public class CustomAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetTrigger(EAnimationType type)
        {
            if(type == EAnimationType.None){ Debug.LogError("Animation is not select ");}
            _animator.SetTrigger(type.ToString());
        }

        public void SetMoveSpeed(float value)
        {
            _animator.SetFloat("Speed", value);
        }

        public void SetBool(EAnimationType type ,bool value)
        {
            if(type == EAnimationType.None){ Debug.LogError("Animation is not select ");}
            _animator.SetBool(type.ToString(), value);   
        }
    }
}