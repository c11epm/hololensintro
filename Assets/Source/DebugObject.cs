using UnityEngine;

namespace Source
{
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