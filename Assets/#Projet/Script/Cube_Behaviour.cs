using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Behaviour : MonoBehaviour
{
    public Ash_Behaviour ash;
    public int colorIndex;
    public bool loaded = false;


    public void Initialize() // ho cambiato da start a Initialize per decidere quando far partire vedi level manager
    {
        if(!loaded)
        {
            colorIndex = Random.Range(0,5);
        }
        
        if (ash == null)
        {
            ash = FindObjectOfType<Ash_Behaviour>(); //FindObjectsOfType too 
        }

        Material mat = ash.materials[colorIndex];
        GetComponent<Renderer>().material = mat;
    }



        void OnMouseDown()
        {
            ash.SetDestination(this);


        }
}


