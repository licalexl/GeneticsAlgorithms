using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorDeStats : MonoBehaviour
{
    public LayerMask characterLayer; 
    public string[] characterTags; 
    public string statsTag = "Estadisticas"; 

    private GameObject currentStatsObject; 

    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, characterLayer))
        {
           
            if (ArrayContainsString(characterTags, hit.collider.gameObject.tag))
            {
                
                ActivateStatsObject(hit.collider.gameObject);
            }
            else
            {
                
                DeactivateStatsObject();
            }
        }
        else
        {
            
            DeactivateStatsObject();
        }
    }

   
    void ActivateStatsObject(GameObject character)
    {
        
        DeactivateStatsObject();

      
        currentStatsObject = character.transform.Find(statsTag).gameObject;

       
        currentStatsObject.SetActive(true);
    }

   
    void DeactivateStatsObject()
    {
       
        if (currentStatsObject != null)
        {
            currentStatsObject.SetActive(false);
            currentStatsObject = null;
        }
    }

   
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
