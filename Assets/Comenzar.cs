using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comenzar : MonoBehaviour
{
    public SpawnAnimals[] spawnAnimals;
    public GameObject[] desactivacionObject;
    public GameObject panel;
    void Start()
    {
        
    }


    public void comenzar() 
    {

        for (int i = 0; i < spawnAnimals.Length; i++)
        {
            spawnAnimals[i].comenzar = true;
            
        }
        for (int i = 0; i < desactivacionObject.Length; i++)
        {
            desactivacionObject[i].SetActive(false);
        }

        panel.SetActive(true); 

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
