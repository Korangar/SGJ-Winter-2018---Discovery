using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwitch : MonoBehaviour
{
    public Vector2 texOffset;
    
    // Start is called before the first frame update
    void Awake()
    {
        SkinnedMeshRenderer mr = GetComponent<SkinnedMeshRenderer>();
        mr.material.mainTextureOffset = texOffset;
    }
}
