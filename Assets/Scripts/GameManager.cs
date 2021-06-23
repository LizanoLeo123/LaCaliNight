using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public GameObject loseBanner;
    public GameObject winBanner;
    public AudioSource cameraAudioSource;

    public Volume volume;
    private DepthOfField DOF;

    private Spawner spawner;

    [HideInInspector]
    public int defeatedEnemies;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet<DepthOfField>(out DOF);
        defeatedEnemies = 0;
        //Spawnear el primer enemigo
        spawner = GameObject.Find("Enemies").GetComponent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DefeatEnemy()
    {
        defeatedEnemies++;

        if (defeatedEnemies == 8)
        {
            FinishGame();
        }
    }

    public void LoseGame()
    {
        spawner.finished = true;
        loseBanner.SetActive(true);
        cameraAudioSource.Stop();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyController controller = enemy.GetComponent<EnemyController>();
            controller.ForceIdle();
        }
    }

    public void FinishGame()
    {
        spawner.finished = true;
        cameraAudioSource.Stop();
        winBanner.SetActive(true);
        DOF.active = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("DevLeo");
    }
}
