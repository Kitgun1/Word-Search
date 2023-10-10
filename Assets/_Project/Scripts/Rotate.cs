using UnityEngine;

namespace _Project.Scripts
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private float _speed;

        private void Update()
        {
            _rect.rotation = Quaternion.Euler(0, 0, _rect.rotation.eulerAngles.z + _speed * Time.deltaTime);
        }
    }
}