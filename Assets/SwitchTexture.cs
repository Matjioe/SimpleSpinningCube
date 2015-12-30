using UnityEngine;
using System.Collections;

public class SwitchTexture : MonoBehaviour {

    public Material[] materials;
    private int currentMaterialIdx = 0;

    public void SwitchMaterial()
    {
        currentMaterialIdx++;
        if (currentMaterialIdx == materials.Length)
            currentMaterialIdx = 0;
        GetComponent<MeshRenderer>().sharedMaterial = materials[currentMaterialIdx];
    }
	
}
