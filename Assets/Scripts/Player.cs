using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hits = 10;

    public GameObject damageVolume;
    public GameObject deathVolume;

    private bool inmune;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHit"))
        {
            if (!inmune)
            {
                hits--;
                Debug.Log("Remaining hits " + hits);
                inmune = true;
                StartCoroutine(QuitInmunity());
                damageVolume.SetActive(true);

                if(hits <= 0)
                {
                    Debug.Log("Me Mori");
                    deathVolume.SetActive(true);
                    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach(GameObject enemy in enemies)
                    {
                        EnemyController controller = enemy.GetComponent<EnemyController>();
                        controller.ForceIdle();
                    }
                }
            }
        }
    }

    IEnumerator QuitInmunity()
    {
        yield return new WaitForSeconds(1f);
        damageVolume.SetActive(false);

        yield return new WaitForSeconds(4f);
        inmune = false;
    }
}
