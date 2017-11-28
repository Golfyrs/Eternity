using UnityEngine;

namespace Eternity.Unity.Common.Components.Camera
{
    public class CameraFollowing : MonoBehaviour
    {
        public UnityEngine.Camera Camera;

        public GameObject Target;
        public int Speed;

        private void Update() =>
            transform.position = transform.position.Lerp(
                transform.position.WithXY(Target.transform.position),
                Speed * Time.deltaTime);
                    
    }
}