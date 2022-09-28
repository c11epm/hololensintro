using UnityEngine;

namespace Source
{
    /// <summary>
    /// Used to "tag" objects as debug objects to only be present in editor for debugging in Play Mode
    /// </summary>
    public class DebugObject : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
#if !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}