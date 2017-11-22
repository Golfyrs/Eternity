using UnityEngine;

namespace Unity.Core.Components.Moving
{
    public class Move : MonoBehaviour
    {
        [SerializeField]
        private int _speed;
        
        [SerializeField]
        private GameObject _target;

        public void By(Vector3 delta) =>
            _target.transform.position = _target.transform.position
                .Lerp(_target.transform.position + delta, _speed * Time.deltaTime);
    }
}