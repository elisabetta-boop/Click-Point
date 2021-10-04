using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Level_Manager manager = FindObjectOfType<Level_Manager>();
        if (manager.loaded)
        {
            manager.Load();
        }
        else{
            manager.Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
