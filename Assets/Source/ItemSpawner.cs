using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Subsystems;
using UnityEngine;
using UnityEngine.XR;

namespace Source
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject itemToSpawn;
        [SerializeField] private Transform resetTransform;
        [SerializeField] private float resetDistance = 10f;
        [SerializeField] private bool resetPositionForBlocks = false;
        [SerializeField] private bool autoSpawn = false;

        private Camera _mainCamera;
        private float _spawnTimer = 0;
        private float _spawnInterval = 0.4f;
        private HandsAggregatorSubsystem handAggregator;
        private HandJointPose rightPalm;
        private bool rightHandStatus;

        private void Awake()
        {
        }

        // Start is called before the first frame update
        private void Start()
        {
            _mainCamera = Camera.main;
            handAggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (autoSpawn)
            {
                rightHandStatus = handAggregator.TryGetJoint(TrackedHandJoint.Palm, XRNode.RightHand, out rightPalm);
                //print("Right palm is: " + rightHandStatus);

                var rightHandIsValid =
                    handAggregator.TryGetPalmFacingAway(XRNode.RightHand, out var isRightPalmFacingAway);

                if (_spawnTimer > _spawnInterval)
                {
                    SpawnObject(rightHandIsValid && isRightPalmFacingAway);
                    _spawnTimer = 0;
                }

                _spawnTimer += Time.deltaTime;
            }
        }

        public void SpawnObject(bool fromHand = false)
        {
            var obj = Instantiate(itemToSpawn, transform);
            var cameraTransform = _mainCamera.transform;
            var objRb = obj.GetComponent<Rigidbody>();
            var block = obj.GetComponent<BlockScript>();
            if (fromHand)
            {
                obj.transform.position = rightPalm.Position;
                objRb.AddForce(-rightPalm.Up * 0.1f, ForceMode.Impulse);
            }
            else
            {
                obj.transform.position =
                    cameraTransform.position + cameraTransform.forward * 0.4f + cameraTransform.right * 0.4f;
                objRb.AddForce(cameraTransform.forward * 0.1f, ForceMode.Impulse);
            }

            if (resetPositionForBlocks)
            {
                block.ResetDistance = resetDistance;
                block.ResetPosition = resetPositionForBlocks;
            }
        }

        public void EnableAutoSpawn(bool status)
        {
            autoSpawn = status;
            _spawnTimer = 0;
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