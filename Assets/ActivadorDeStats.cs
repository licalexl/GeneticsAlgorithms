using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDeStats : MonoBehaviour
{
    public LayerMask characterLayer; // Capa de los personajes
    public string[] characterTags; // Tags de los personajes
    public string statsTag = "Estadisticas"; // Tag del objeto de estad�sticas

    private GameObject currentStatsObject; // Referencia al objeto de estad�sticas actualmente activo

    void Update()
    {
        // Dispara un rayo desde la posici�n del mouse en la pantalla
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Comprueba si el rayo impacta con un objeto en la capa de los personajes
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, characterLayer))
        {
            // Comprueba si el objeto impactado tiene una de las etiquetas de personajes v�lidas
            if (ArrayContainsString(characterTags, hit.collider.gameObject.tag))
            {
                // Activa el objeto de estad�sticas del personaje
                ActivateStatsObject(hit.collider.gameObject);
            }
            else
            {
                // Desactiva el objeto de estad�sticas si no est� sobre un personaje v�lido
                DeactivateStatsObject();
            }
        }
        else
        {
            // Desactiva el objeto de estad�sticas si no hay ning�n objeto impactado
            DeactivateStatsObject();
        }
    }

    // Activa el objeto de estad�sticas del personaje
    void ActivateStatsObject(GameObject character)
    {
        // Desactiva el objeto de estad�sticas actualmente activo si hay uno
        DeactivateStatsObject();

        // Busca el objeto de estad�sticas del personaje
        currentStatsObject = character.transform.Find(statsTag).gameObject;

        // Activa el objeto de estad�sticas del personaje
        currentStatsObject.SetActive(true);
    }

    // Desactiva el objeto de estad�sticas
    void DeactivateStatsObject()
    {
        // Desactiva el objeto de estad�sticas actualmente activo si hay uno
        if (currentStatsObject != null)
        {
            currentStatsObject.SetActive(false);
            currentStatsObject = null;
        }
    }

    // Comprueba si un array de strings contiene un string espec�fico
    bool ArrayContainsString(string[] array, string target)
    {
        foreach (string str in array)
        {
            if (str == target)
                return true;
        }
        return false;
    }
}
