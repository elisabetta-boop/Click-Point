using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Changer : MonoBehaviour
{
    public void Change(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void ChangeWithLoad(string name) 
    {
        FindObjectOfType<Level_Manager>().loaded = true;
        Change(name);
    }
}
