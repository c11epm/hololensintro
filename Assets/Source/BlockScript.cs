using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class BlockScript : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        public bool ResetPosition { get; set; }

        public float ResetDistance { get; set; }
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (ResetPosition)
            {
                if ((transform.position - transform.parent.position).magnitude > ResetDistance)
                {
                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.angularVelocity = Vector3.zero;
                    transform.rotation = Quaternion.identity;
                    var pos = transform.parent.position + transform.parent.up * 0.8f;
                    pos.x = Random.Range(pos.x -0.3f, pos.x +0.3f);
                    pos.y = Random.Range(pos.y -0.3f, pos.y +0.3f);
                    pos.z = Random.Range(pos.z -0.3f, pos.z +0.3f);
                    transform.position = pos;
                }
            }
        }
    }
}