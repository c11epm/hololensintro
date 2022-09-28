using UnityEngine;

namespace Source
{
    /// <summary>
    /// Sample script of a simple function to rotate an object around it's y-axis
    /// </summary>
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