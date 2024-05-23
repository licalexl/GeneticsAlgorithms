using UnityEngine;

public class PrefabCounter : MonoBehaviour
{
    public GameObject[] prefab; // Asigna el prefab en el inspector
    public int countConejo = 0;
    public int countFox = 0;
    public int countRino = 0;
    public int countIguana = 0;
    public int countOso = 0;
    public int countTigre = 0;
    void Update()
    {
        // Verificar que el prefab está asignado
        if (prefab == null)
        {
            Debug.LogError("Prefab no asignado en el inspector.");
            return;
        }

        string prefabName = prefab[0].name;
        string prefabName1 = prefab[1].name;
        string prefabName2 = prefab[2].name;
        string prefabName3 = prefab[3].name;
        string prefabName4 = prefab[4].name;
        string prefabName5 = prefab[5].name;


        // Encontrar todos los objetos en la escena
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Contar las instancias del prefab
        countConejo = 0;
        countFox = 0;
        countRino = 0;
        countIguana = 0;
        countOso = 0;
        countTigre = 0;

        foreach (GameObject obj1 in allObjects)
        {
            if (obj1.name == prefabName)
            {
                countConejo++;
            }

        }

        foreach (GameObject obj2 in allObjects)
        {
            if (obj2.name == prefabName1)
            {
                countFox++;
            }
        }

        foreach (GameObject obj3 in allObjects)
        {
            if (obj3.name == prefabName2)
            {
                countRino++;
            }
        }

        foreach (GameObject obj4 in allObjects)
        {
            if (obj4.name == prefabName3)
            {
                countRino++;
            }
        }

        // Imprimir la cantidad de instancias encontradas
        //Debug.Log("Cantidad de instancias del prefab " + prefabName + ": " + count);
    }
}
