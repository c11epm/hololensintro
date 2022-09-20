using UnityEngine;

namespace Source
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject itemToSpawn;

        [SerializeField] private Transform resetTransform;

        [SerializeField] private float resetDistance = 10f;

        [SerializeField] private bool resetPositionForBlocks = false;

        [SerializeField] private bool autoSpawn = false;

        private Camera mainCamera;

        private float spawnTimer = 0;

        private float spawnInterval = 0.4f;

        // Start is called before the first frame update
        private void Awake()
        {
        }

        private void Start()
        {
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            if (autoSpawn)
            {
                if (spawnTimer > spawnInterval)
                {
                    SpawnObject();
                    spawnTimer = 0;
                }

                spawnTimer += Time.deltaTime;
            }
        }

        public void SpawnObject()
        {
            var obj = Instantiate(itemToSpawn, transform);
            var cameraTransform = mainCamera.transform;
            obj.transform.position =
                cameraTransform.position + cameraTransform.forward * 0.3f + cameraTransform.right * 0.3f;
            var objRb = obj.GetComponent<Rigidbody>();
            objRb.AddForce(cameraTransform.forward * 0.15f, ForceMode.Impulse);
            var block = obj.GetComponent<BlockScript>();

            if (resetPositionForBlocks)
            {
                block.ResetTransform = resetTransform;
                block.ResetDistance = resetDistance;
                block.ResetPosition = resetPositionForBlocks;
            }
        }

        public void EnableAutoSpawn(bool status)
        {
            autoSpawn = status;
            spawnTimer = 0;
        }

        public void RemoveSpawns()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}