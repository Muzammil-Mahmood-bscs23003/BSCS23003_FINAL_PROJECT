using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static bool isLast = false;
    public GameObject enemy;
    public GameObject lastItem;
    public GameObject lastPortal;
    public Button levelButton;
    public AudioClip levelUpSound;
    private AudioSource audioSource;
    public int levTwoThresh = 5;
    public int levThreeThresh = 8;
    public int levFourThresh = 11;
    private bool istwo = true;
    private bool isthree = true;
    private bool isfour = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (levelButton != null)
        {
            levelButton.GetComponentInChildren<Text>().text = "Level 1";
        }
    }

    private void FixedUpdate()
    {
        if (Collectible.score >= levTwoThresh && istwo)
        {
            IncreaseEnemySpeed(55);
            UpdateButtonText("Level 2");
            istwo = false;
        }

        if (Collectible.score >= levThreeThresh && isthree && !istwo)
        {
            IncreaseEnemySpeed(75);
            UpdateButtonText("Level 3");
            isthree = false;
        }

        if (Collectible.score >= levFourThresh && isfour && !isthree && !istwo)
        {
            IncreaseEnemySpeed(90);
            UpdateButtonText("Final Level");
            isfour = false;


            Vector3 randomPosition = new Vector3(
                Random.Range(-95, 1503),
                585,
                Random.Range(-286, 1251)
            );

            Vector3 randomPosition2 = new Vector3(
                Random.Range(-95, 1503),
                585,
                Random.Range(-286, 1251)
            );

            Instantiate(lastItem, randomPosition, Quaternion.identity);
            Instantiate(lastPortal, randomPosition2, Quaternion.identity);
            isLast = true;
        }
    }

    public void UpdateButtonText(string level)
    {
        if (levelButton != null)
        {
            levelButton.GetComponentInChildren<Text>().text = level;
        }

        if (levelUpSound != null)
        {
            audioSource.PlayOneShot(levelUpSound);
        }
    }

   
    public void IncreaseEnemySpeed(int newSpeed)
    {
        enemy enemyMovement = enemy.GetComponent<enemy>();
        if (enemyMovement != null)
        {
            enemyMovement.speed = newSpeed;
            enemyMovement.rotationSpeed = newSpeed;
        }
    }
}
