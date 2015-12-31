using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

    public float zoom = 2.0f;
    
    void Start()
    {
        SetZoom(zoom);
    }

    public void SetZoom(float zoom)
    {
        transform.position = Vector3.up * 1.5f - transform.forward * zoom * zoom;
    }
}
