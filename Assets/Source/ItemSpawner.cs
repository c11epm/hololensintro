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
        private HandsAggregatorSubsystem _handAggregator;
        private HandJointPose _rightPalm;
        private bool _rightHandStatus;
        private GameObject _cube;

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
            _handAggregator = XRSubsystemHelpers.GetFirstRunningSubsystem<HandsAggregatorSubsystem>();
            slider.SliderValue = 0.0f;
        }

        // Update is called once per frame
        private void Update()
        {
            if (autoSpawn)
            {
                _rightHandStatus = _handAggregator.TryGetJoint(TrackedHandJoint.Palm, XRNode.RightHand, out _rightPalm);

                var rightHandIsValid =
                    _handAggregator.TryGetPalmFacingAway(XRNode.RightHand, out var isRightPalmFacingAway);

                if (_spawnTimer > _spawnInterval && rightHandIsValid && isRightPalmFacingAway)
                {
                    SpawnObject();
                    _spawnTimer = 0;
                }

                _spawnTimer += Time.deltaTime;
            }
        }

        public void ToggleHandMaterial()
        {
            HandMeshController[] handMeshControllers = FindObjectsOfType<HandMeshController>();
            foreach (var handMeshController in handMeshControllers)
            {
                handMeshController.ToggleMaterial();
            }
        }

        /// <summary>
        /// Spawns the cube object
        /// </summary>
        public void SpawnCube()
        {
            _cube = Instantiate(cubePrefab);
            var cameraTransform = _mainCamera.transform;
            _cube.transform.position = cameraTransform.position + cameraTransform.forward;
        }

        /// <summary>
        /// Deletes the Cube obejct
        /// </summary>
        public void DeleteCube()
        {
            if (_cube != null)
            {
                Destroy(_cube);
            }

            _cube = null;
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
        public void SpawnObject()
        {
            //Instantiate Block Object from prefab and get references to components
            var obj = Instantiate(itemToSpawn, transform);
            var cameraTransform = _mainCamera.transform;
            var objRb = obj.GetComponent<Rigidbody>();
            var block = obj.GetComponent<BlockScript>();

            //Spawn from hand position + normal from Palm (which is -rightPalm.Up), 30 cm.
            obj.transform.position = _rightPalm.Position + -_rightPalm.Up * 0.3f;
            objRb.AddForce(-_rightPalm.Up * 0.1f, ForceMode.Impulse);

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