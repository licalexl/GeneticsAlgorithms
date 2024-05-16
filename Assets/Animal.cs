using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class Animal : MonoBehaviour
{
    [SerializeField] protected string nombre;//1
    [SerializeField] protected float vida;//2
    [SerializeField] protected float vidaMax;
    [SerializeField] protected float hambre;//3
    [SerializeField] protected float hambreMax;//3
    [SerializeField] protected float sed;//4
    [SerializeField] protected float sedMax;//4
    [SerializeField] protected string sexo;//5
    [SerializeField] protected float edad;//6
    [SerializeField] protected float velocidad;//7
    [SerializeField] protected float resistencia;//8
    [SerializeField] protected float fertilidad;//9
    [SerializeField] protected float alerta;//10
    [SerializeField] protected float fuerza;//11

    [SerializeField] protected bool carnivoro;
    [SerializeField] protected bool hijo=false;
    [SerializeField] protected bool alertado;
    [SerializeField] protected bool cambio=false;

    public float rotationSpeed = 5f;
    public Vector3 targetPosition; 


    private Rigidbody rb;



    public TextMeshProUGUI estatsText;
    public enum State
    {
        BuscarComida,
        BuscarAgua,
        BuscarPareja,
        Escapar,
        Comer,
        Beber,
        Procrear,
        MovingRandomly
    }
    public State state;


    protected virtual void Start( )
    {
        
    }
    public void GetFood() 
    { 
    
    }


    private void Update()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) > 0.5f)
        {
            rb.MovePosition(transform.position + transform.forward * velocidad * Time.deltaTime);
        }
        else
        {
            cambio = true;
        }


        estatsText.text = ("nombre: " + nombre + "\nVida: " + vida + "/" + vidaMax+ "\nHambre: " + hambre + "/"+hambreMax + "\nSed: " + sed + "/" + sedMax +
                           "\nFertilidad: " + fertilidad + "\nSexo: " + sexo + "\nResistencia: " + resistencia + "\nVelocidad: " + velocidad
                           + "\nEdad: " + edad + "\nFuerza: " + fuerza + "\nAlerta: " + alerta + "\nObjetivo: "+ state);

        if (cambio==true)
        {
            switch (state)
            {
                case State.BuscarComida:
                    int rand1 = Random.Range(25, 180);
                    int rand2 = Random.Range(-50, 110);

                    targetPosition = new Vector3(rand1,0, rand2);
                    cambio = false;

                    break;

                case State.BuscarAgua:

                    targetPosition = new Vector3(70, 0, 50);
                    cambio = false;                     
                    
                    break;

                case State.Escapar:
                    int rand3 = Random.Range(20, 180);
                    int rand4 = Random.Range(-50, 110);

                    targetPosition = new Vector3(rand3, 0, rand4);
                    cambio = false; 

                    break;

                case State.Comer: cambio = false; break;

                case State.Beber: cambio = false; break;

                case State.Procrear: cambio = false; break;

                case State.BuscarPareja:

                    int rand5 = Random.Range(25, 180);
                    int rand6 = Random.Range(-50, 110);

                    targetPosition = new Vector3(rand5, 0, rand6);
                    cambio = false;

                    break;

                case State.MovingRandomly:
                    int rand7 = Random.Range(25, 180);
                    int rand8 = Random.Range(-50, 110);

                    targetPosition = new Vector3(rand7, 0, rand8);
                    cambio = false;
                    break;

            }

        }

    }


    public abstract void GetWater();
    public abstract void GetPartner();
    public abstract void Run();
    public abstract void Eat();
    public abstract void DrinkWater();

    public void Generacion()
    {
        if (hijo==false)
        {
            rb = GetComponent<Rigidbody>();
            vidaMax = Random.Range(50, 100);
            vida = vidaMax;
            hambre = Random.Range(100, 150);
            hambreMax = hambre;
            int rand = Random.Range(0, 2);
            switch (rand)
            {
                case 0: sexo = "Macho"; break;
                case 1: sexo = "Hembra"; break;
                default:
                    sexo = "Hembra";
                    break;
            }
            edad = Random.Range(1, 21);
            velocidad = Random.Range(5, 11);
            resistencia = Random.Range(1, 11);
            fertilidad = Random.Range(1, 11);
            alerta = Random.Range(1, 11);
            sed = Random.Range(100, 150);
            sedMax = sed;
            fuerza = Random.Range(50, 100);

            int rand1 = Random.Range(25, 180);
            int rand2 = Random.Range(-50, 110);
            targetPosition = new Vector3(rand1, 0, rand2);

            StartCoroutine(Pensar());
            StartCoroutine(Tiempo());
            StartCoroutine(Restador());
        }     

    }
    private void OnTriggerEnter(Collider other)
    {

        if (carnivoro==true)
        {
            if (other.gameObject.CompareTag("Herviboro") && state == State.BuscarComida) 
            {
                targetPosition = new Vector3(other.transform.position.x, 0, other.transform.position.z);
                state = State.Comer;
                cambio = true;
            }          

        }
        else if (carnivoro == false)
        {
            if (other.gameObject.CompareTag("Carnivoro"))
            {
                alertado = true;
                StartCoroutine(Pensar());
               
            }
            if (other.gameObject.CompareTag("Comida") && state == State.BuscarComida)
            {
                targetPosition = new Vector3(other.transform.position.x, 0, other.transform.position.z);
                state = State.Comer;
                cambio = true;
            }
        }


        if (other.gameObject.CompareTag("Carnivoro") || other.gameObject.CompareTag("Herviboro") && state == State.BuscarPareja)
        {

            Animal objeto1 = other.gameObject.GetComponent<Animal>();
            Animal objeto2 = gameObject.GetComponent<Animal>();           

            if (nombre ==  objeto1.nombre && sexo != objeto1.sexo &&  objeto1.state == State.BuscarPareja)
            {
                objeto2.targetPosition = new Vector3(other.transform.position.x, 0, other.transform.position.z);
                objeto1.targetPosition = new Vector3(objeto2.transform.position.x, 0, objeto2.transform.position.z);
                objeto2.state = State.Procrear;
                objeto1.state = State.Procrear;
                cambio = true;
                objeto1.cambio = true;

            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (carnivoro == true)
        {
            if (collision.gameObject.CompareTag("Herviboro") && state == State.Comer)
            {
                
                Animal objeto = collision.gameObject.GetComponent<Animal>();
                objeto.vida = objeto.vida - fuerza + objeto.fuerza/2;
                if (objeto.vida <=0)
                {
                    Destroy(collision.gameObject);
                    hambre += 60;
                    if (hambre>hambreMax)
                    {
                        hambre = hambreMax;
                    }
                }
            }

        }
        else if (carnivoro == false)
        {
            if (collision.gameObject.CompareTag("Comida") && state == State.Comer)
            {
                Destroy(collision.gameObject);
                hambre += 40;
                if (hambre > hambreMax)
                {
                    hambre = hambreMax;
                }
            }
        }


        if (collision.gameObject.CompareTag("Carnivoro") || collision.gameObject.CompareTag("Herviboro") && state == State.Procrear)
        {

            Animal objeto1 = collision.gameObject.GetComponent<Animal>();
            Animal objeto2 = gameObject.GetComponent<Animal>();

            objeto2.state = State.Procrear;
            objeto1.state = State.Procrear;
            cambio = true;
            objeto1.cambio = true;

            if (nombre == objeto1.nombre && sexo != objeto1.sexo && objeto1.state == State.Procrear)
            {
                objeto2.state = State.MovingRandomly;
                objeto1.state = State.MovingRandomly;
                cambio = true;
                objeto1.cambio = true;
                objeto2.cambio = true;

                objeto2.hambre = hambre-50;
                objeto1.hambre = hambre - 50;

                objeto2.sed = sed - 70;
                objeto1.sed = sed - 70;

                GeneracionHijo(objeto2, objeto1);
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Agua") && state == State.BuscarAgua)
        {
            sed+=1f;
            if (sed>sedMax)
            {
                sed = sedMax;
                cambio = true;
                StartCoroutine(Pensar());
            }

        }

        if (carnivoro == true)
        {
            if (other.gameObject.CompareTag("Herviboro") && state == State.Comer)
            {
                targetPosition = new Vector3(other.transform.position.x, 0, other.transform.position.z);               
            }

        }
        else if (carnivoro == false)
        {           
            if (other.gameObject.CompareTag("Comida") && state == State.Comer)
            {
                targetPosition = new Vector3(other.transform.position.x, 0, other.transform.position.z);                
            }
        }
    }
   

    IEnumerator Pensar()
    {

        if (alertado == true)
        {
            float rand1 = Random.Range(alerta/2, 11);
            if (rand1 > 5)
            {
                state = State.Escapar;
                cambio = true;
            }
            else
            {
                alertado = false;
                StartCoroutine(Pensar());
            }

        }
        else if (edad>=8 && hambre>=80 && sed>=80)
        {
            float rand2 = Random.Range(fertilidad/ 2, 11);
            if (rand2 > 5)
            {
                state = State.BuscarPareja;
                cambio = true;
            }
            else
            {
                hambre=hambre - 20;
                sed=sed - 20;
            }
            
        }
        else if (hambre <= 100 || sed <= 100)
        {
            if (hambre < sed )
            {
                state = State.BuscarComida;
                cambio = true;

            }
            else
            {
                state = State.BuscarAgua;
                cambio = true;


            }
        }       
        else
        {
            if (Random.value < 0.1f)
            {
                state = State.MovingRandomly;
                cambio = true;
            }
        }


        
            yield return new WaitForSeconds(5f);

        if (state == State.Escapar)
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(Pensar());
        }
        else if (state == State.Comer || state == State.Procrear)
        {
            yield return new WaitForSeconds(3f);
            StartCoroutine(Pensar());
        }
        else
        {
            StartCoroutine(Pensar());
        }
    }

    IEnumerator Tiempo()
    {
        if (edad>20)
        {
            Destroy(gameObject);
        }
        edad++;
        yield return new WaitForSeconds(20+edad);
        StartCoroutine(Tiempo());
    }

    IEnumerator Restador()
    {
        hambre = hambre - 15 + resistencia;
        sed = sed - 15 + resistencia;
        yield return new WaitForSeconds(10f);
        StartCoroutine(Tiempo());
    }


    public void GeneracionHijo(Animal animal1, Animal animal2) 
    {

        Vector3 spawnPosition = new Vector3(animal1.transform.position.x, 0, animal1.transform.position.y);
        // Generar el objeto en la posición especificada
        Animal newObj=Instantiate(animal1, spawnPosition, Quaternion.identity);


        int random1 = Random.Range(0, 2);
        if (random1==0)
        {
            if (animal1.vidaMax < animal2.vidaMax)
            {
                newObj.vidaMax = Random.Range(animal1.vidaMax, animal2.vidaMax);
            }
            else
            {
                newObj.vidaMax = Random.Range(animal2.vidaMax, animal1.vidaMax);
            }
        }
        else
        {
            newObj.vidaMax = Random.Range(50, 100);
        }
        newObj.vida = newObj.vidaMax;
        //hambre
        random1 = Random.Range(0, 2);
        if (random1 == 0)
        {
            if (animal1.hambreMax < animal2.hambreMax)
            {
                newObj.hambreMax = Random.Range(animal1.hambreMax, animal2.hambreMax);
            }
            else
            {
                newObj.hambreMax = Random.Range(animal2.hambreMax, animal1.hambreMax);
            }
        }
        else
        {
            newObj.hambreMax = Random.Range(100, 150);
        }
        newObj.hambre = newObj.hambreMax;

        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0: newObj.sexo = "Macho"; break;
            case 1: newObj.sexo = "Hembra"; break;
            default:
                newObj.sexo = "Hembra";
                break;
        }
        edad = Random.Range(1, 5);

        random1 = Random.Range(0, 2);
        if (random1 == 0)
        {
            if (animal1.velocidad < animal2.velocidad)
            {
                newObj.velocidad = Random.Range(animal1.velocidad, animal2.velocidad);
            }
            else
            {
                newObj.velocidad = Random.Range(animal2.velocidad, animal1.velocidad);
            }
        }
        else
        {
            newObj.velocidad = Random.Range(5, 11);
        }

        random1 = Random.Range(0, 2);
        if (random1 == 0)
        {
            if (animal1.resistencia < animal2.resistencia)
            {
                newObj.resistencia = Random.Range(animal1.resistencia, animal2.resistencia);
            }
            else
            {
                newObj.resistencia = Random.Range(animal2.resistencia, animal1.resistencia);
            }
        }
        else
        {
            newObj.resistencia = Random.Range(1, 11);
        }

        random1 = Random.Range(0, 2);
        if (random1 == 0)
        {
            if (animal1.fertilidad < animal2.fertilidad)
            {
                newObj.fertilidad = Random.Range(animal1.fertilidad, animal2.fertilidad);
            }
            else
            {
                newObj.fertilidad = Random.Range(animal2.fertilidad, animal1.fertilidad);
            }
        }
        else
        {
            newObj.fertilidad = Random.Range(1, 11);
        }

        random1 = Random.Range(0, 2);
        if (random1 == 0)
        {
            if (animal1.alerta < animal2.alerta)
            {
                newObj.alerta = Random.Range(animal1.alerta, animal2.alerta);
            }
            else
            {
                newObj.alerta = Random.Range(animal2.alerta, animal1.alerta);
            }
        }
        else
        {
            newObj.alerta = Random.Range(1, 11);
        }

        random1 = Random.Range(0, 2);
        if (random1 == 0)
        {
            if (animal1.sedMax < animal2.sedMax)
            {
                newObj.sedMax = Random.Range(animal1.sedMax, animal2.sedMax);
            }
            else
            {
                newObj.sedMax = Random.Range(animal2.sedMax, animal1.sedMax);
            }
        }
        else
        {
            newObj.sedMax = Random.Range(100, 150);
        }


        newObj.sed = newObj.sedMax;

        random1 = Random.Range(0, 2);
        if (random1 == 0)
        {
            if (animal1.fuerza < animal2.fuerza)
            {
                newObj.fuerza = Random.Range(animal1.fuerza, animal2.fuerza);
            }
            else
            {
                newObj.fuerza = Random.Range(animal2.fuerza, animal1.fuerza);
            }
        }
        else
        {
            newObj.fuerza = Random.Range(50, 100);
        }

        int rand1 = Random.Range(25, 180);
        int rand2 = Random.Range(-50, 110);
        newObj.targetPosition = new Vector3(rand1, 0, rand2);

       
    }

}

