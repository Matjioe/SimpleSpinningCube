using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MaterialName : MonoBehaviour {

    public GameObject target;
    
    void Update () {
        GetComponent<Text>().text = target.GetComponent<MeshRenderer>().sharedMaterial.name;
    }
}
