using UnityEngine;

namespace Source
{
    public class Rotation : MonoBehaviour
    {
        private void Start()
        {
        }

        private void Update()
        {
            transform.RotateAround(transform.position, Vector3.up, 20f * Time.deltaTime);
        }
    }
}