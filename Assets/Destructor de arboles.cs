using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructordearboles : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Comida"))
        {
            Destroy(other.gameObject);
        }
    }
}
