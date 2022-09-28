using Microsoft.MixedReality.Toolkit.Environment;
using TMPro;
using UnityEngine;

namespace Source
{
    public class MeshVisualizer : MonoBehaviour
    {
        private GenericSpatialMeshVisualizer _meshVisualizer;

        [SerializeField] private TextMeshPro buttonText;
        // Start is called before the first frame update
        void Start()
        {
            _meshVisualizer = GetComponent<GenericSpatialMeshVisualizer>();
            buttonText.text = _meshVisualizer.DisplayOption.ToString();
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// Cycles through the mesh visualization modes and updates button text status
        /// </summary>
        public void UseNextSpatialAwarenessMeshDisplayOption()
        {
            _meshVisualizer.DisplayOption = GetNextDisplayOptions(_meshVisualizer.DisplayOption);
            buttonText.text = _meshVisualizer.DisplayOption.ToString();
        }

        private SpatialAwarenessMeshDisplayOptions GetNextDisplayOptions(SpatialAwarenessMeshDisplayOptions option)
        {
            return option switch
            {
                SpatialAwarenessMeshDisplayOptions.None => SpatialAwarenessMeshDisplayOptions.Visible,
                SpatialAwarenessMeshDisplayOptions.Visible => SpatialAwarenessMeshDisplayOptions.Occlusion,
                _ => SpatialAwarenessMeshDisplayOptions.None
            };
        }
    }
}
