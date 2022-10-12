using System;
using UnityEngine;

namespace Source
{
    public class SpatialMeshObject : MonoBehaviour
    {
        private MeshRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
        }

        public void SetMaterial(Material material)
        {
            if (material == null)
            {
                _renderer.enabled = false;
            }
            else
            {
                _renderer.material = material;
                _renderer.enabled = true;
            }
        }
    }
}