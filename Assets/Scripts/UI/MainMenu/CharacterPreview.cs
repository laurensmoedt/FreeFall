using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    public DataManager dataManager;

    [SerializeField]
    Mesh[] mMeshes = null;

    [SerializeField]
    Material[] materials = null;

    private MeshFilter mFilter;
    private MeshRenderer mRenderer;

    private void Awake()
    {
        dataManager.Load();

        mFilter = GetComponent<MeshFilter>();
        mRenderer = GetComponent<MeshRenderer>();

        //Check which character is active and change the object properties accordingly
        if (dataManager.data.currentCharacter == "GlassCubeCharacter")
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
