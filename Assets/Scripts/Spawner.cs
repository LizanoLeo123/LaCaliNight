using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemies;

    private int current;

    private bool next;

    [HideInInspector]
    public bool finished;

    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        current = 0;
        next = false;
        RandomizeEnemies();
        StartCoroutine(SpawnEnemy(5));
    }

    IEnumerator SpawnEnemy(int wait)
    {
        yield return new WaitForSeconds(3f);
        if (current < enemies.Length)
        {
            enemies[current].SetActive(true);
            current++;
        }
        else
        {
            Debug.Log("Ya cerro a golpes a todos, es usted un completo care...");
            finished = true;
        }
        yield return new WaitForSeconds(wait);
        next = true;
    }

    public void RandomizeEnemies()
    {
        for (int i = 0; i < enemies.Length - 1; i++)
        {
            int r = Random.Range(0, enemies.Length);
            GameObject temp = enemies[i];
            enemies[i] = enemies[r];
            enemies[r] = temp;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            if (next)
            {
                int wait = Random.Range(7, 16);
                StartCoroutine(SpawnEnemy(wait));
                next = false;
            }
        }
    }
}
