using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class PrefabCounter : MonoBehaviour
{
    public GameObject[] prefabs; 
    public TextMeshProUGUI textMeshProUGUI;

    private Dictionary<string, int> prefabCounts = new Dictionary<string, int>();

    void Update()
    {
        prefabCounts.Clear();

        foreach (var prefab in prefabs)
        {
            prefabCounts[prefab.name] = 0;
        }

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            foreach (var prefab in prefabs)
            {
                if (obj.name.StartsWith(prefab.name))
                {
                    prefabCounts[prefab.name]++;
                }
            }
        }

        textMeshProUGUI.text = "";
        foreach (var prefab in prefabs)
        {
            textMeshProUGUI.text += prefab.name + ": " + prefabCounts[prefab.name] + "\n";
        }
    }
}
