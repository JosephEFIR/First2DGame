using Scripts.Enums;
using UnityEngine;

namespace Scripts.Animators
{
    public class CustomAnimator
    {
        private Animator _animator;

        public void Init(Animator animator)
        {
            _animator = animator;
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