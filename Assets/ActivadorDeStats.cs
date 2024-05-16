using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDeStats : MonoBehaviour
{
    public LayerMask characterLayer; // Capa de los personajes
    public string[] characterTags; // Tags de los personajes
    public string statsTag = "Estadisticas"; // Tag del objeto de estadísticas

    private GameObject currentStatsObject; // Referencia al objeto de estadísticas actualmente activo

    void Update()
    {
        // Dispara un rayo desde la posición del mouse en la pantalla
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Comprueba si el rayo impacta con un objeto en la capa de los personajes
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, characterLayer))
        {
            // Comprueba si el objeto impactado tiene una de las etiquetas de personajes válidas
            if (ArrayContainsString(characterTags, hit.collider.gameObject.tag))
            {
                // Activa el objeto de estadísticas del personaje
                ActivateStatsObject(hit.collider.gameObject);
            }
            else
            {
                // Desactiva el objeto de estadísticas si no está sobre un personaje válido
                DeactivateStatsObject();
            }
        }
        else
        {
            // Desactiva el objeto de estadísticas si no hay ningún objeto impactado
            DeactivateStatsObject();
        }
    }

    // Activa el objeto de estadísticas del personaje
    void ActivateStatsObject(GameObject character)
    {
        // Desactiva el objeto de estadísticas actualmente activo si hay uno
        DeactivateStatsObject();

        // Busca el objeto de estadísticas del personaje
        currentStatsObject = character.transform.Find(statsTag).gameObject;

        // Activa el objeto de estadísticas del personaje
        currentStatsObject.SetActive(true);
    }

    // Desactiva el objeto de estadísticas
    void DeactivateStatsObject()
    {
        // Desactiva el objeto de estadísticas actualmente activo si hay uno
        if (currentStatsObject != null)
        {
            currentStatsObject.SetActive(false);
            currentStatsObject = null;
        }
    }

    // Comprueba si un array de strings contiene un string específico
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
