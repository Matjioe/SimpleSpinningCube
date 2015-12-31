using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

    public GameObject prefab;
    public int poolSize = 2048;
    public int enabledObjectsCount = 1;
    public GameObject enabledObjectCountUI;
    public GameObject materialNameUI;
    public GameObject objectNameUI;

    public Material[] materials;
    private int currentMaterialIdx = -1;
    private int currentObjectIdx = -1;

    GameObject [] instances;

    Vector3 forward;
    Vector3 currentPosition;
    int spawnCount = 0;
    
    void Start ()
    {
        instances = new GameObject[poolSize];
        forward = Vector3.right * 1.5f;
        currentPosition = Vector3.zero;
        SnaleSpawn();
        EnableObjects(enabledObjectsCount);
        SwitchObject();
        SwitchMaterial();
    }

    void SnaleSpawn()
    {
        instances[spawnCount] = Instantiate(prefab);
        instances[spawnCount].transform.position = currentPosition;
        spawnCount++;

        // Algorithm:
        // 1. Go snaleSize forward
        // 2. Turn right and go snaleSize forward
        // 3. Turn, increase snaleSize, go to 1.
        int snaleSize = 1;
        Quaternion rotateRight = Quaternion.Euler(0f, 90f, 0f);
        while (spawnCount < poolSize)
        {
            SpawnForwardNTimes(snaleSize);
            forward = rotateRight * forward;
            SpawnForwardNTimes(snaleSize);
            forward = rotateRight * forward;
            snaleSize++;
        }
    }

    void SpawnForwardNTimes(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            instances[spawnCount] = Instantiate(prefab);
            currentPosition += forward;
            instances[spawnCount].transform.position = currentPosition;
            spawnCount++;

            if (spawnCount >= poolSize)
                return;
        }
    }

    public void EnableObjects(float percentage)
    {
        if (percentage == 0f)
            EnableObjects(0);
        else if (percentage == 1f)
            EnableObjects(1);
        else if (percentage == 2f)
            EnableObjects(2);
        else if (percentage == 3f)
            EnableObjects(4);
        else if (percentage == 4f)
            EnableObjects(9);
        else if (percentage == 5f)
            EnableObjects(16);
        else if (percentage == 6f)
            EnableObjects(25);
        else if (percentage == 7f)
            EnableObjects(36);
        else if (percentage == 8f)
            EnableObjects(49);
        else if (percentage == 9f)
            EnableObjects(100);
        else
        {
            float count = percentage * (float)poolSize * 0.01f;
            EnableObjects((int)count);
        }
    }

    public void EnableObjects(int count)
    {
        if (count > poolSize)
            count = poolSize;

        for (int i = 0; i < count; ++i)
        {
            instances[i].SetActive(true);
        }
        for (int i = count; i < poolSize; ++i)
        {
            instances[i].SetActive(false);
        }

        if (enabledObjectCountUI != null)
        {
            enabledObjectCountUI.GetComponent<UnityEngine.UI.Text>().text = count.ToString();
        }
    }

    public void IncreaseEnabledObjects()
    {
        if (enabledObjectsCount >= poolSize)
            return;

        enabledObjectsCount++;
        EnableObjects(enabledObjectsCount);
    }

    public void DecreaseEnabledObjects()
    {
        if (enabledObjectsCount == 0)
            return;

        enabledObjectsCount--;
        EnableObjects(enabledObjectsCount);
    }

    public void SwitchMaterial()
    {
        currentMaterialIdx++;
        if (currentMaterialIdx == materials.Length)
            currentMaterialIdx = 0;

        if (materialNameUI != null)
            materialNameUI.GetComponent<UnityEngine.UI.Text>().text = materials[currentMaterialIdx].name;

        for (int i = 0; i < poolSize; ++i)
        {
            RecursiveSwitchMaterial(instances[i]);
        }
    }

    private void SwitchMaterial(GameObject go)
    {
        Renderer renderer = go.GetComponent<Renderer>();
        if (renderer != null)
            renderer.sharedMaterial = materials[currentMaterialIdx];
    }

    private void RecursiveSwitchMaterial(GameObject go)
    {
        SwitchMaterial(go);
        for (int c = 0; c < go.transform.childCount; ++c)
        {
            RecursiveSwitchMaterial(go.transform.GetChild(c).gameObject);
        }
    }

    public void EnableAnimation(bool enable)
    {
        for (int i = 0; i < poolSize; ++i)
        {
            for (int c = 0; c < instances[i].transform.childCount; ++c)
            {
                instances[i].transform.GetChild(c).gameObject.GetComponent<Animator>().enabled = enable;
            }
        }
    }

    public void SwitchObject()
    {
        currentObjectIdx++;
        if (currentObjectIdx == instances[0].transform.childCount)
            currentObjectIdx = 0;

        if (objectNameUI != null)
            objectNameUI.GetComponent<UnityEngine.UI.Text>().text = instances[0].transform.GetChild(currentObjectIdx).gameObject.name;

        for (int i = 0; i < poolSize; ++i)
        {
            for (int c = 0; c < instances[i].transform.childCount; ++c)
            {
                if (c == currentObjectIdx)
                    instances[i].transform.GetChild(c).gameObject.SetActive(true);
                else
                    instances[i].transform.GetChild(c).gameObject.SetActive(false);
            }
        }
    }
}
