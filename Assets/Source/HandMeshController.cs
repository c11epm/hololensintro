using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMeshController : MonoBehaviour
{
    [SerializeField] private Material transparent;
    [SerializeField] private Material filled;

    [SerializeField] private SkinnedMeshRenderer renderer;

    // Start is called before the first frame update
    private bool isFilled;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ToggleMaterial()
    {
        var materials = renderer.materials;
        isFilled = !isFilled;
        if (isFilled)
        {
            materials[1] = filled;
        }
        else
        {
            materials[1] = transparent;
        }

        renderer.materials = materials;
    }
}