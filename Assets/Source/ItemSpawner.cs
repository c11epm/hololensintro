using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Subsystems;
using Microsoft.MixedReality.Toolkit.UX;
using UnityEngine;
using UnityEngine.XR;

namespace Source
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject itemToSpawn;
        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private float resetDistance = 10f;
        [SerializeField] private bool resetPositionForBlocks = false;
        [SerializeField] private bool autoSpawn = false;
        [SerializeField] private Slider slider;

        private Camera _mainCamera;
        private float _spawnTimer = 0;
        private float _spawnInterval = 0.4f;
        private HandsAggregatorSubsystem handAggregator;
        private HandJointPose rightPalm;
        private bool rightHandStatus;
        private GameObject cube;

        private void Awake()
        {
        }

        private void OnEnable()
        {
            slider.OnValueUpdated.AddListener(OnSliderValueUpdate);
        }

        private void OnDisable()
        {
            slider.OnValueUpdated.RemoveListener(OnSliderValueUpdate);
        }

        // Start is called before the first frame update
        private void Start()
        {
            _mainCamera = Camera.main;
            handAggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
            slider.SliderValue = 0.0f;
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

        /// <summary>
        /// Spawns the cube object
        /// </summary>
        public void SpawnCube()
        {
            cube = Instantiate(cubePrefab);
            var cameraTransform = _mainCamera.transform;
            cube.transform.position = cameraTransform.position + cameraTransform.forward;
        }

        /// <summary>
        /// Deletes the Cube obejct
        /// </summary>
        public void DeleteCube()
        {
            if (cube != null)
            {
                Destroy(cube);
            }
            cube = null;
        }

        //Slider for auto update spawn rate event function
        private void OnSliderValueUpdate(SliderEventData data)
        {
            if (data.NewValue == 0)
            {
                _spawnInterval = float.MaxValue;
            }
            else
            {
                var clampValue = Mathf.Lerp(0, 0.8f, Mathf.InverseLerp(0, 1, data.NewValue));
                _spawnInterval = 1 - clampValue;
            }
        }

        /// <summary>
        /// Spawns an Object (Block) from head or hand depending on the fromHand attribute
        /// </summary>
        /// <param name="fromHand">boolean telling if to spawn from right hand or not</param>
        public void SpawnObject(bool fromHand = false)
        {
            //Instantiate Block Object from prefab and get references to components
            var obj = Instantiate(itemToSpawn, transform);
            var cameraTransform = _mainCamera.transform;
            var objRb = obj.GetComponent<Rigidbody>();
            var block = obj.GetComponent<BlockScript>();

            if (fromHand)
            {
                //Spawn from hand position + normal from Palm (which is -rightPalm.Up), 30 cm.
                obj.transform.position = rightPalm.Position + -rightPalm.Up * 0.3f;
                objRb.AddForce(-rightPalm.Up * 0.1f, ForceMode.Impulse);
            }
            else
            {
                //Spawn from Head (camera position) + forward direction 40cm + right direction 40cm
                obj.transform.position =
                    cameraTransform.position + cameraTransform.forward * 0.4f + cameraTransform.right * 0.4f;
                objRb.AddForce(cameraTransform.forward * 0.1f, ForceMode.Impulse);
            }

            //Set reset properties if the blocks should reset their position
            if (resetPositionForBlocks)
            {
                block.ResetDistance = resetDistance;
                block.ResetPosition = resetPositionForBlocks;
            }
        }

        /// <summary>
        /// Enables/Disables the Auto Spawn function
        /// </summary>
        /// <param name="status">Status to enable/disable</param>
        public void EnableAutoSpawn(bool status)
        {
            autoSpawn = status;
            _spawnTimer = 0;
        }

        /// <summary>
        /// Removes all spawned objects, not the Blocks included in the scene
        /// </summary>
        public void RemoveSpawns()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}