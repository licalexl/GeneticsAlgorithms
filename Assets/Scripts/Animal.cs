using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal
{
    private Genes _genes;
    private float _currentHealth;

    public abstract void GetFood();
    public abstract void GetWater();
    public abstract void GetPartner();
    public abstract void Run();
    public abstract void Eat();
    public abstract void DrinkWater();

    public Genes Reproduce(Animal a1, Animal a2)
    {
        return a1.GetGenes().Reproduce(a2.GetGenes());
    }

    public void SetGenes(Genes genes)
    {
        _genes = genes;
    }

    public Genes GetGenes()
    {
        return _genes;
    }

    public void SetCurrentHealth(float currentHealth)
    {
        _currentHealth = currentHealth;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
    
}

public class Genes
{
    public Vector3 _color;
    public float _velocidad;
    public float _fuerza;
    public float _maxVida;
    public float _miedo;
    public Prioridad _prioridad;

    public Genes()
    {
        _color = new Vector3(Random.Range(0,255),Random.Range(0,255),Random.Range(0,255));
        _velocidad = Random.Range(1,50);
        _fuerza = Random.Range(1,50);
        _maxVida = Random.Range(1,50);
        _miedo = Random.Range(1,50);
        _prioridad = (Prioridad)Random.Range(0,3);
    }

    public Genes(Vector3 color, float velocidad, float fuerza, float maxVida, float miedo, Prioridad prioridad)
    {
        _color = color;
        _velocidad = velocidad;
        _fuerza = fuerza;
        _maxVida = maxVida;
        _miedo = miedo;
        _prioridad = prioridad;
    }

    public void Mutate(float probability)
    {
        if (Random.Range(0f, 1f) > probability)
        {
            _color.x += Random.Range(0.1f, 100f);
            _color.y += Random.Range(0.1f, 100f);
            _color.z += Random.Range(0.1f, 100f);
        }

        if (Random.Range(0f, 1f) > probability)
        {
            _velocidad += Random.Range(-40f, 40f);
        }
        
        if (Random.Range(0f, 1f) > probability)
        {
            _fuerza += Random.Range(-40f, 40f);
        }
        
        if (Random.Range(0f, 1f) > probability)
        {
           _maxVida += Random.Range(-40f, 40f);
        }
        
        if (Random.Range(0f, 1f) > probability)
        {
            _miedo += Random.Range(-40f, 40f);
        }
        
        if (Random.Range(0f, 1f) > probability)
        {
            _prioridad = (Prioridad)Random.Range(0, 3);
        }
    }

    public Genes Reproduce(Genes g2)
    {
        Genes gHijo = new Genes(_color, _velocidad, _fuerza, g2._maxVida, g2._miedo, g2._prioridad);
        gHijo.Mutate(Random.Range(0f,1f));
        return gHijo;
    }
    
}

public enum Prioridad
{
    AGUA = 0, COMIDA = 1, HUIR = 2, REPRODUCIRSE = 3
}