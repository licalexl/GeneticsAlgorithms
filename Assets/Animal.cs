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
    [SerializeField] protected bool generandoHijo=false;

    [SerializeField] private float timer = 0f;
    [SerializeField] private bool startTimer = false;



    [SerializeField] private bool ignorando;


    [SerializeField] protected GameObject pointApareamiento; 

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
        if (startTimer==true)
        {
            timer += Time.deltaTime;

            if (timer >= 7f)
            {
                state = State.MovingRandomly;
                cambio = true;
                StartCoroutine(Pensar());
                startTimer = false;
                timer = 0;
            }
        }

        if (state == State.Procrear)
        {
            startTimer = true;
        }

        if (transform.position.y<-0.5)
        {
            Vector3 currentPosition = transform.position;

            currentPosition.y = 0f;

            transform.position = currentPosition;
        }
        if ((state == State.BuscarComida || state == State.Comer) && carnivoro ==false)
        {
            Physics.IgnoreLayerCollision(gameObject.layer, 6, false);
        
            ignorando = false;
        }
        else 
        {
          // Physics.IgnoreLayerCollision(gameObject.layer, 6, true);
            ignorando = true;

        }

        if (carnivoro == true)
        {
            Physics.IgnoreLayerCollision(gameObject.layer, 6, true);
        }
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) > 0.5f)
        {
            rb.MovePosition(transform.position + transform.forward * velocidad * Time.deltaTime);
        }
        else
        {         
               
             StartCoroutine(Pensar());
                       
        }


        estatsText.text = ("nombre: " + nombre + "\nVida: " + vida + "/" + vidaMax+ "\nHambre: " + hambre + "/"+hambreMax + "\nSed: " + sed + "/" + sedMax +
                           "\nFertilidad: " + fertilidad + "\nSexo: " + sexo + "\nResistencia: " + resistencia + "\nVelocidad: " + velocidad
                           + "\nEdad: " + edad + "\nFuerza: " + fuerza + "\nAlerta: " + alerta + "\nObjetivo: "+ state);

        if (cambio==true)
        {
            float rand1;
            float rand2;
            switch (state)
            {
                case State.BuscarComida:
                   
                    do
                    {
                       rand1 = Random.Range(25, 180);
                       rand2 = Random.Range(-50, 110);

                    } while((rand1 > 40 && rand1 < 95) && (rand2 > 29 && rand2 < 70));



                    targetPosition = new Vector3(rand1,0, rand2);
                    cambio = false;

                    break;

                case State.BuscarAgua:

                    targetPosition = new Vector3(Random.Range(40, 90), 0, Random.Range(30, 60));
                    cambio = false;                     
                    
                    break;

                case State.Escapar:

                    do
                    {
                        rand1 = Random.Range(25, 180);
                        rand2 = Random.Range(-50, 110);

                    } while ((rand1 > 40 && rand1 < 95) && (rand2 > 29 && rand2 < 70));


                    targetPosition = new Vector3(rand1, 0, rand2);
                    cambio = false; 

                    break;

                case State.Comer: cambio = false; break;

                case State.Beber: cambio = false; break;

                case State.Procrear: cambio = false; break;

                case State.BuscarPareja:

                    do
                    {
                        rand1 = Random.Range(pointApareamiento.gameObject.transform.position.x-5f, pointApareamiento.gameObject.transform.position.x + 30f);
                        rand2 = Random.Range(pointApareamiento.gameObject.transform.position.z -10f, pointApareamiento.gameObject.transform.position.z + 30f);

                    } while ((rand1 > 40 && rand1 < 95) && (rand2 > 29 && rand2 < 70));

                    targetPosition = new Vector3(rand1, 0, rand2);
                    cambio = false;

                    break;

                case State.MovingRandomly:

                  
                    do
                    {
                        rand1 = Random.Range(25, 180);
                        rand2 = Random.Range(-50, 110);

                    } while ((rand1 > 40 && rand1 < 95) && (rand2 > 29 && rand2 < 70));
                    targetPosition = new Vector3(rand1, 0, rand2);

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
                startTimer = true;
                timer = 0;

                StopCoroutine(Pensar());
            }

        }
        else if (carnivoro == false)
        {
            if (other.gameObject.CompareTag("Carnivoro") && alertado != true)
            {
                Animal objeto1 = other.gameObject.GetComponent<Animal>();
                if (objeto1.state == State.BuscarComida)
                {
                    alertado = true;
                    StartCoroutine(Pensar());
                }                             
            }
            if (other.gameObject.CompareTag("Comida") && state == State.BuscarComida)
            {
                targetPosition = new Vector3(other.transform.position.x, 0, other.transform.position.z);
                state = State.Comer;
                startTimer = true;
                timer = 0;
                StopCoroutine(Pensar());
              
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
                startTimer = true;
                timer = 0;
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
                    state = State.MovingRandomly;
                    StartCoroutine(Pensar());
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
                state = State.MovingRandomly;
                StartCoroutine(Pensar());
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

            if (nombre == objeto1.nombre && sexo != objeto1.sexo && objeto1.state == State.Procrear)
            {
                Animal objeto2 = gameObject.GetComponent<Animal>();
                if (generandoHijo == false)
                {
                    generandoHijo = true;           
                    objeto2.hambre = hambre - 50;
                    objeto1.hambre = hambre - 50;
                    objeto2.sed = sed - 50;
                    objeto1.sed = sed - 50;
                    GeneracionHijo(objeto2, objeto1);
                    objeto2.state = State.MovingRandomly;
                    objeto1.state = State.MovingRandomly;                    
                    objeto1.cambio = true;
                    objeto2.cambio = true;
                    StartCoroutine(creacion());
                    StartCoroutine(Pensar());

                }


            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (sed<sedMax && other.gameObject.CompareTag("Agua"))
        {

            if (state == State.BuscarAgua)
            {

                sed += 1f;
                if (sed > sedMax)
                {
                    sed = sedMax;
                   
                    StartCoroutine(Pensar());
                }

            }
        }
      
        

        if (carnivoro == true)
        {
            if (other.gameObject.CompareTag("Herviboro") && state == State.Comer)
            {
                targetPosition = new Vector3(other.transform.position.x, 0, other.transform.position.z);
                StopCoroutine(Pensar());
            }
            else if(state == State.Comer)
            {

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
   
    IEnumerator creacion() 
    {
        yield return new WaitForSeconds(10f);
        generandoHijo = false;
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
            }

        }
        else if (edad>=10 && hambre>80 && sed>80)
        {
            float rand2 = Random.Range(fertilidad/ 2, 11);
            if (rand2 > 5)
            {
                if (state != State.Procrear)
                {
                    state = State.BuscarPareja;
                    cambio = true;
                }
            }           
            
        }
        else if (hambre < 100 || sed < 100)
        {
            if (hambre < sed )
            {
                if (state != State.Comer)
                {
                    state = State.BuscarComida;
                    cambio = true;
                }
                

            }
            else
            {
                state = State.BuscarAgua;
                cambio = true;


            }
        }       
        else
        {
           
                state = State.MovingRandomly;
                cambio = true;
            
        }
        

        if (state == State.Escapar)
        {
            yield return new WaitForSeconds(10f);
            StartCoroutine(Pensar());
        }
        else if (state == State.Comer ||  state == State.Procrear)
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(Pensar());
        }
        else
        {
            yield return new WaitForSeconds(5f);
            StartCoroutine(Pensar());
        }
    }

    IEnumerator Tiempo()
    {
        if (edad>30)
        {
            Destroy(gameObject);
        }
        edad++;
        yield return new WaitForSeconds(20+edad);
        StartCoroutine(Tiempo());
    }

    IEnumerator Restador()
    {
        hambre = hambre - 7 + resistencia/2;
        sed = sed - 7 + resistencia/2;
        if (hambre <0 || sed <0)
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(10f);
        StartCoroutine(Restador());
    }


    public void GeneracionHijo(Animal animal1, Animal animal2) 
    {

        Vector3 spawnPositionn = new Vector3(animal1.transform.position.x, 0.5f, animal1.transform.position.z);
        // Generar el objeto en la posición especificada
        Animal newObj=Instantiate(animal1, spawnPositionn, Quaternion.identity);


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

