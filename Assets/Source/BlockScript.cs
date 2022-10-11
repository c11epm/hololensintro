using System.Xml.Schema;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source
{
    public class BlockScript : MonoBehaviour
    {
        public bool ResetPosition { get; set; } = true;
        public float ResetDistance { get; set; } = 10;
        private Rigidbody _rigidbody;
        
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
                    //Reset the speed attributes of the rigidbody to move it
                    _rigidbody.velocity = Vector3.zero;
                    _rigidbody.angularVelocity = Vector3.zero;
                    var pos = transform.parent.position + transform.parent.up * 0.8f;
                    //Make the position to seem somewhat randomized in the reset
                    pos.x = Random.Range(pos.x -0.3f, pos.x +0.3f);
                    pos.y = Random.Range(pos.y -0.3f, pos.y +0.3f);
                    pos.z = Random.Range(pos.z -0.3f, pos.z +0.3f);
                    transform.position = pos;
                }
            }
        }
    }
}