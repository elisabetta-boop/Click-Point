using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; // pour sauvegarde
using System.IO; // pour sauvegarde
using System.Runtime.Serialization.Formatters.Binary; // pour sauvegarde

[Serializable]
public class GameData // pour sauvegarde (les trucs a memoriser)
{
    public int AshColor;
    public int[] CubesColor;
    public float[] ashPosition = new float[3]; // 3 positions xyz
}

public class Level_Manager : MonoBehaviour
{
    public static Level_Manager instance; //1 pour singleton level manager
    

    public void Save()
    {
        FileStream file = File.Create(Application.persistentDataPath + "/data.dat"); //// pour sauvegarde pour creer mon flux des donnés
        try // en cas d'error proprement
        {
            BinaryFormatter bf = new BinaryFormatter();
            GameData data = new GameData();
            Ash_Behaviour ash = FindObjectOfType<Ash_Behaviour>();
            data.AshColor = ash.colorIndex;
            data.ashPosition[0] = ash.transform.position.x;
            data.ashPosition[1]= ash.transform.position.y;
            data.ashPosition[2] = ash.transform.position.y;

            Cube_Behaviour[] cubes = FindObjectsOfType<Cube_Behaviour>();
            data.CubesColor = new int[cubes.Length]; // instantie tableau color du meme numero des cubes

            for (int i=0; i < cubes.Length; i++) // je parcour le tableau 
            {
                data.CubesColor[i] = cubes[i].colorIndex; // pour chaque color je recuper l'index
            }
            bf.Serialize(file, data);
        }
        finally
        {
            file.Close();
        }
    }
    public void Load()
    {
        GameData data; // == null une instance cree est tjr null

        if(File.Exists(Application.persistentDataPath + "/data.dat"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/data.dat", FileMode.Open);

            try{
                BinaryFormatter bf = new BinaryFormatter();
                data =  bf.Deserialize(file) as GameData;

            }
            finally{
                file.Close();
            }
        }
        Initialize(data);
            

        
    }
    void Start()
    {
        if(instance == null) //code pour gerer le level manager
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); // per non distruggerlo quando cambio scena
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Save();
            Application.Quit();
            
        }
    }
    public void Initialize(GameData data = null) // si on passe pas des arguments = null 
    {
        
        Ash_Behaviour ash =  FindObjectOfType<Ash_Behaviour>();

        Cube_Behaviour[] cubes = FindObjectsOfType<Cube_Behaviour>();

        if (data != null)
        {
            ash.colorIndex = data.AshColor;
            ash.loaded = true;
            Vector3 position = new Vector3(data.ashPosition[0],data.ashPosition[1],data.ashPosition[2]);
            ash.transform.position = position;

            for(int i=0; i < cubes.Length; i++)
            {
                cubes[i].colorIndex = data.CubesColor[i];
                cubes[i].loaded = true;
            }
        }
        
    }

}
