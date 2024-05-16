using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FuzzyLogic : MonoBehaviour
{
    public GameObject CanvasWin;
    public TextMeshProUGUI textMunicion;
    public Transform player; 
    public float rotationSpeed = 2.0f;

   /* public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private Vector3 dashDirection;*/


    public enum State
    {
        BuscarComida,
        BuscarAgua,
        BuscarPareja,
        MovingRandomly
    }

    public GameObject healthObject;
    public GameObject ammoObject;
    public GameObject playerObject;
    public float fleeDistance = 10f;
    public float health = 100f;
    public float ammo; 

    private NavMeshAgent agent;
    public State state;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = State.MovingRandomly;
        StartCoroutine(Pensar());
    }

    void Update()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
            CanvasWin.SetActive(true);
            Invoke("RecargarScene", 1.5f);

        }



       /* if (canDash==true)
        {
            Dash();
        }*/

        
        switch (state)
        {
            case State.BuscarComida:
                agent.SetDestination(healthObject.transform.position);
                break;
            case State.BuscarAgua:
                agent.SetDestination(ammoObject.transform.position);
                break;
            case State.BuscarPareja:
                Vector3 randomDirection = transform.position - playerObject.transform.position;                
                NavMeshHit navHit;

                if (NavMesh.SamplePosition(randomDirection, out navHit, fleeDistance, NavMesh.AllAreas))
                {
                    agent.SetDestination(navHit.position);
                }
                break;
            case State.MovingRandomly:
                if (!agent.hasPath)
                {
                    Vector3 randomPosition2 = Random.insideUnitSphere * 20f;
                    agent.SetDestination(randomPosition2);
                }
                break;
        }



        Vector3 direction2 = player.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction2, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);



       

    }

    public void MoveToHealth()
    {
        state = State.BuscarComida;
    }

    public void MoveToAmmo()
    {
        state = State.BuscarAgua;
    }

    public void FleePlayer()
    {
        state = State.BuscarPareja;
    }

    public void MoveRandomly()
    {
        state = State.MovingRandomly;
    }

    void OnTriggerEnter(Collider other)
    {
      
        if (other.CompareTag("Vida"))
        {
            health = health + 60;
            if (health >100 )
            {
                health = 100;
            }
        }

        if (other.CompareTag("Espada"))
        {
            health = health-10;
        }
    }
    IEnumerator Pensar() 
    {

        if (health <= 30)
        {
            state = State.BuscarComida;
        }
        else if (health < 50 && Vector3.Distance(healthObject.transform.position, playerObject.transform.position) > 7 && ammo >=10f)
        {
            state = State.BuscarComida;
        }
        else if (ammo <= 0)
        {
            state = State.BuscarAgua;
        }
        else if (ammo <= 25 && Vector3.Distance(ammoObject.transform.position, playerObject.transform.position) > 7)
        {
            state = State.BuscarAgua;
        }
        else if (Vector3.Distance(transform.position, playerObject.transform.position) < fleeDistance)
        {
            state = State.BuscarPareja;
        }
        else if (health >=80 && ammo >= 35)
        {
            float distanceToHealth = Vector3.Distance(playerObject.transform.position, healthObject.transform.position);
            float distanceToAmmo = Vector3.Distance(playerObject.transform.position, ammoObject.transform.position);
            state = distanceToHealth > distanceToAmmo ? State.BuscarComida : State.BuscarAgua;
        }
        else
        {
            if (Random.value < 0.1f)
            {
                state = State.MovingRandomly;
            }
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Pensar());
    }
      

    IEnumerator PerformDash(Vector3 moveDistance, float duration)
    {

        yield return new WaitForSeconds(5f);
    }
    public void RecargarScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
