using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimals : MonoBehaviour
{
    public GameObject objectToGenerate;
    public Vector3 spawnPosition;
    public float spawnInterval = 1f;
    public int Cantidad;
    public int suma=0;
    private float timer; // Temporizador para rastrear el tiempo entre generaciones


    void Start()
    {
        timer = 0f;
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && suma<Cantidad)
        {
            suma++;
            Generar();
        }
    }

    public void Generar() 
    {

        spawnPosition = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        Instantiate(objectToGenerate, spawnPosition, Quaternion.identity);

        timer = 0f;
    }

}
