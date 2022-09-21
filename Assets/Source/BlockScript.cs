using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class BlockScript : MonoBehaviour
    {
        private Rigidbody rigidbodyComponent;

        public bool ResetPosition { get; set; }

        public float ResetDistance { get; set; }

        public Transform ResetTransform { get; set; }

        private void Start()
        {
            rigidbodyComponent = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (ResetPosition)
            {
                if ((transform.position - ResetTransform.position).magnitude > ResetDistance)
                {
                    rigidbodyComponent.velocity = Vector3.zero;
                    rigidbodyComponent.angularVelocity = Vector3.zero;
                    transform.rotation = Quaternion.identity;
                    var pos = ResetTransform.position + ResetTransform.up * 0.8f;
                    pos.x = Random.Range(pos.x -0.3f, pos.x +0.3f);
                    pos.y = Random.Range(pos.y -0.3f, pos.y +0.3f);
                    pos.z = Random.Range(pos.z -0.3f, pos.z +0.3f);
                    transform.position = pos;
                }
            }
        }
    }
}