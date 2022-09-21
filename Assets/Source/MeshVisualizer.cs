using Microsoft.MixedReality.Toolkit.Environment;
using TMPro;
using UnityEngine;

namespace Source
{
    public class MeshVisualizer : MonoBehaviour
    {
        private GenericSpatialMeshVisualizer meshVisualizer;

        [SerializeField] private TextMeshPro buttonText;
        // Start is called before the first frame update
        void Start()
        {
            meshVisualizer = GetComponent<GenericSpatialMeshVisualizer>();
            buttonText.text = meshVisualizer.DisplayOption.ToString();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void UseNextSpatialAwarenessMeshDisplayOption()
        {
            meshVisualizer.DisplayOption = GetNextDisplayOptions(meshVisualizer.DisplayOption);
            buttonText.text = meshVisualizer.DisplayOption.ToString();
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
