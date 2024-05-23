using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToGenerate;
    public Vector3 spawnPosition;
    public float spawnInterval = 10f; 
    private float timer; 


    void Start()
    {
        timer = 0f;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Generar();
        }
    }

    public void Generar() 
    {
        int rand1;
        int rand2;
        do
        {
            rand1 = Random.Range(20, 180);
            rand2 = Random.Range(-50, 110);

        } while ((rand1 > 40 && rand1 < 95) && (rand2 > 29 && rand2 < 70));

        spawnPosition = new Vector3(rand1, 0, rand2);
        // Generar el objeto en la posición especificada
        Instantiate(objectToGenerate, spawnPosition, Quaternion.identity);

        // Reiniciar el temporizador
        timer = 0f;
    }

}
