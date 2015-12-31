using UnityEngine;
using System.Collections;

public class LightConfigurator : MonoBehaviour {

	public void SetShadows(int shadow)
    {
        GetComponent<Light>().shadows = (LightShadows) shadow;
    }
}
