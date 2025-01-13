using UnityEngine;

namespace Mono.UI
{
    public class RotateImage : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private void Update()
        {
            transform.Rotate(Vector3.back * _speed * Time.deltaTime);
        }
    }
}
