using UnityEngine;

namespace Scripts.Effects
{
    public class ParalaxEffect : MonoBehaviour
    {
        [SerializeField]private Camera _camera;
        
        private const float Coef = 0.99F;
        private Transform _background;
        
        private Vector3 _backgroundStartPosition;
        private Vector3 _cameraStartPosition;


        private void Awake()
        {
            _background = GetComponent<Transform>();
        }

        private void Start()
        {
            _cameraStartPosition = _camera.transform.position;
            gameObject.transform.position = new Vector3(_cameraStartPosition.x, _cameraStartPosition.y, gameObject.transform.position.z);
            _backgroundStartPosition = _background.position;
        }

        private void Update()
        {
            _background.position = _backgroundStartPosition + (_camera.transform.position - _cameraStartPosition) * Coef;
        }
    }
}