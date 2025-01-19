using UnityEngine;

namespace Business.Math
{
    public static class ProjectMath
    {
        public static Vector3 GetPointInCircle(Transform center, float min, float max)
        {
            float angle = Random.Range(0, Mathf.PI * 2);
            float radius = Mathf.Sqrt(Random.Range(min * min, max * max));

            float x = center.position.x + radius * Mathf.Cos(angle);
            float z = center.position.z + radius * Mathf.Sin(angle);
            float y = center.position.y;

            return new Vector3(x, y, z);
        }
    }
}