using UnityEngine;

namespace Unity.Core.Components.Moving
{
    [RequireComponent(typeof(Move))]
    public class KeyboardMove : MonoBehaviour
    {
        private Move _move;

        private void Awake()
        {
            _move = GetComponent<Move>();
        }

        private void Update()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            var delta = new Vector2(horizontal * 5, vertical * 5);
            
            _move.By(delta);
        }
    }
}