using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int defeatedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        defeatedEnemies = 0;
        //Spawnear el primer enemigo

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DefeatEnemy()
    {
        defeatedEnemies++;

        if(defeatedEnemies == 8)
        {
            Debug.Log("A HUEVO!");
        }
    }
}
