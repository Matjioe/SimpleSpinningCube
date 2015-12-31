using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UINavigationDefault : MonoBehaviour {

	void Start () {
        GetComponent<Selectable>().Select();
	}
}
