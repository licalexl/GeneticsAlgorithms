using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SpawnAnimals : MonoBehaviour
{
    public GameObject objectToGenerate;
    public Vector3 spawnPosition;
    public float spawnInterval = 1f;
    public int Cantidad;
    public int suma=0;
    private float timer; 
    public bool comenzar=false;
    private string input;
    public TMP_InputField inputField;

    void Start()
    {
        
    }
    void Update()
    {
        if (comenzar ==false)
        {
            int.TryParse(inputField.text, out Cantidad);
            timer = 0f;
        }
        if (comenzar == true) 
        {
            timer += Time.deltaTime;

            if (timer >= spawnInterval && suma < Cantidad)
            {
                suma++;
                Generar();
            }

        }
       
    }

    public void Generar() 
    {
        spawnPosition = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
        Instantiate(objectToGenerate, spawnPosition, Quaternion.identity);

        timer = 0f;
    }


    public void AsignacionCantidad(string numeros)
    {
        //Cantidad = numeros;
    }


}
