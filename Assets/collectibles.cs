using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public static int score = 0;
    public static bool islastdiamond = false;
    public AudioClip collectSound;
    public GameObject gemIconPrefab;
    public Transform canvasTransform;
    private AudioSource audioSource;

    public enum GemType { Type1, Type2, Type3, Type4 }
    public GemType gemType;

    public static int totalGemCount = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Helicopter"))
        {
            if (gemType == GemType.Type2)
                score += 2;
            else if (gemType == GemType.Type1)
                score++;

            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            if (gemType == GemType.Type4)
            {
                islastdiamond = true;
            }

            if (gemType == GemType.Type3)
            {
                main mainScript = other.GetComponent<main>();

                if (mainScript != null)
                {
                    mainScript.RestoreHearts();
                    Debug.Log("Lives Restored.");
                }

                Destroy(gameObject, collectSound.length);

                audioSource.PlayOneShot(collectSound);
                return;
            }

            else if (canvasTransform != null && gemIconPrefab != null && gemType != GemType.Type4)
            {
                int gemsToAdd = gemType == GemType.Type1 ? 1 : 2;

                for (int i = 0; i < gemsToAdd; i++)
                {
                    GameObject gemIcon = Instantiate(gemIconPrefab, canvasTransform);
                    RectTransform rectTransform = gemIcon.GetComponent<RectTransform>();

                    rectTransform.anchorMin = new Vector2(0, 1);
                    rectTransform.anchorMax = new Vector2(0, 1);
                    rectTransform.pivot = new Vector2(0, 1);

                    float xOffset = 30 * totalGemCount + 5;

                    rectTransform.anchoredPosition = new Vector2(xOffset, -20);

                    totalGemCount++;
                }
            }

            Debug.Log("Score: " + score);
            Destroy(gameObject, collectSound.length - 2);

            audioSource.PlayOneShot(collectSound);
        }
    }
}
