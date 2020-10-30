using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    public DataManager dataManager;

    [SerializeField]
    Mesh[] mMeshes;

    [SerializeField]
    Material[] materials;

    private MeshFilter mFilter;
    private MeshRenderer mRenderer;

    void Start()
    {
        mFilter = GetComponent<MeshFilter>();
        mRenderer = GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (dataManager.data.currentCharacter == "GlassBallCharacter")
        {
            mFilter.mesh = mMeshes[0];
            mRenderer.material = materials[0];
        }
        else if (dataManager.data.currentCharacter == "MagnetCharacter")
        {
            mFilter.mesh = mMeshes[1];
            mRenderer.materials = new Material[] { materials[1], materials[2] };
        }
        else if (dataManager.data.currentCharacter == "TimeCharacter")
        {
            mFilter.mesh = mMeshes[2];
            mRenderer.materials = new Material[] { materials[3], materials[4] };
        }
        return;
    }
}
