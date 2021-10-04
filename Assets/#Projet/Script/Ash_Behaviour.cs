using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Ash_Behaviour : MonoBehaviour
{
    public Material[] materials;
    private NavMeshAgent agent;
    private Cube_Behaviour cubeDestination;
    public int colorIndex; // pour sauvegarde j'ai besoin de memorizer la couleur
    public bool loaded = false;

    public void Initialize() // ho cambiato da start a Initialize per decidere quando far partire vedi level manager
    {
        if(materials == null || materials.Length < 4)
        {
            Debug.LogError("This component need 4 materials.", gameObject); //je lui dit ou il pourrait il y avoir un problem
        }
        else
        {
            if (!loaded)
            {
                colorIndex = Random.Range(0,4);
            }
            
            GetComponent<Renderer>().material = materials[colorIndex];
        }
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Cube_Behaviour cube)
    {
        agent.SetDestination(cube.transform.position);
        cubeDestination = cube;
    }
    private void ChangeColor()
    {
        int exchange = colorIndex; //abc
        colorIndex = cubeDestination.colorIndex;
        cubeDestination.colorIndex = exchange;

        
        GetComponent<Renderer>().material = materials[colorIndex];
        cubeDestination.GetComponent<Renderer>().material = materials[cubeDestination.colorIndex];
        
        

        // //transposition algoritmique c=a a=b b=c
        // Material mat = GetComponent<Renderer>().material;
        // GetComponent<Renderer>().material = cubeDestination.GetComponent<Renderer>().material;
        // cubeDestination.GetComponent<Renderer>().material = mat;
    }
    void Update()
    {
        if(cubeDestination != null)
        {
            if(Vector3.Distance(cubeDestination.transform.position, transform.position) < 0.5f)
            //if (agent.remainingDistance<0.5f) //ne marche pas il y a autre type d'object
            {
                ChangeColor();
                cubeDestination = null;
            }
        }
    }
}
