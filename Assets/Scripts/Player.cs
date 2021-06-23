using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using EZCameraShake;

public class Player : MonoBehaviour
{
    public int hits = 10;

    public AudioClip[] soundFX;

    public GameObject damageVolume;
    public GameObject deathVolume;

    public Volume vol;
    private float min = -0.6f;
    private float max = 0.6f;
    private LensDistortion ld;
    private bool growing;

    public bool drunk;
    private bool inmune;
    private bool death = false;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        vol.profile.TryGet<LensDistortion>(out ld);
        ld.intensity.value = 0;
        growing = true;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!death)
        {
            if (drunk) //Drunk effect condition
            {
                if (ld.intensity.value > max)
                    growing = false;
                if (ld.intensity.value < min)
                    growing = true;

                if (growing)
                {
                    //ld.intensity.value = Mathf.Lerp(ld.intensity.value, 1, 0.3f * Time.deltaTime);
                    ld.intensity.value = Mathf.Lerp(ld.intensity.value, 0.7f, Time.deltaTime / 2);
                }
                else
                {
                    //ld.intensity.value = Mathf.Lerp(ld.intensity.value, -1, 0.3f * Time.deltaTime);
                    ld.intensity.value = Mathf.Lerp(ld.intensity.value, -0.7f, Time.deltaTime / 2);
                }
            }
            else
            {
                ld.intensity.value = 0;
            }
        }
        else
        {
            ld.intensity.value = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHit"))
        {
            if (!inmune && !death)
            {
                int index = Random.Range(0, soundFX.Length);
                AudioSource.PlayClipAtPoint(soundFX[index], transform.position, 0.2f);
                CameraShaker.Instance.ShakeOnce(10f, 10f, .1f, 1f);
                hits--;
                Debug.Log("Remaining hits " + hits);
                inmune = true;
                StartCoroutine(QuitInmunity());
                damageVolume.SetActive(true);

                if(hits <= 0)
                {
                    gameManager.LoseGame();
                    death = true;
                    Debug.Log("Me Mori");
                    deathVolume.SetActive(true);
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
