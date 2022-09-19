using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class BlockScript : MonoBehaviour
    {
        private Rigidbody rigidbody;

        public bool ResetPosition { get; set; }

        public float ResetDistance { get; set; }

        public Transform ResetTransform { get; set; }

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (ResetPosition)
            {
                if ((transform.position - ResetTransform.position).magnitude > ResetDistance)
                {
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero;
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