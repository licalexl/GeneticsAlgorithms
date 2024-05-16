using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToGenerate;
    public Vector3 spawnPosition;
    public float spawnInterval = 10f; 
    private float timer; // Temporizador para rastrear el tiempo entre generaciones


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

        spawnPosition = new Vector3(Random.Range(20, 180), 0, Random.Range(-50, 110));
        // Generar el objeto en la posición especificada
        Instantiate(objectToGenerate, spawnPosition, Quaternion.identity);

        // Reiniciar el temporizador
        timer = 0f;
    }

}
