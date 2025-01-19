using UnityEngine;

namespace Business.Extensions
{
    public static class TransformExtensions
    {
        public static void ClampInCameraBounds(this Transform target, Camera camera)
        {
            Vector3 position = target.position;

            float distanceFromCamera = position.z - camera.transform.position.z;

            Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, distanceFromCamera));
            Vector3 topRight = camera.ViewportToWorldPoint(new Vector3(1, 1, distanceFromCamera));

            position.x = Mathf.Clamp(position.x, bottomLeft.x, topRight.x);
            position.z = Mathf.Clamp(position.z, bottomLeft.z, topRight.z);

            target.position = position;
        }
    }
}