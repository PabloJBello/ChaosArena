using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (prefab == null)
        {
            Debug.LogWarning("No prefab provided to spawn!");
            return null;
        }

        GameObject obj = Instantiate(prefab, position, rotation);
        return obj;
    }
}