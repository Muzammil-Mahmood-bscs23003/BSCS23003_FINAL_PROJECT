using UnityEngine;
using UnityEngine.SceneManagement;


public class teleport : MonoBehaviour
{
    public Transform spawnPoint;
    public AudioClip spawnSound;
    private AudioSource audioSource;
    public enum PortalType { Type1, Type2 };
    public PortalType portalType;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Final!!!");
        if (portalType == PortalType.Type2 && GameController.isLast && Collectible.islastdiamond)
        {
            if (spawnSound != null)
            {
                audioSource.PlayOneShot(spawnSound);
            }
            SceneManager.LoadScene("win");
        }

        if (portalType == PortalType.Type1)
        {
            if (spawnPoint != null)
            {
                Vector3 newPosition = spawnPoint.position;
                newPosition.y -= 40;

                collision.transform.position = newPosition;

                if (spawnSound != null)
                {
                    audioSource.PlayOneShot(spawnSound);
                }

                Debug.Log($"{collision.gameObject.name} teleported to {spawnPoint.position}");
            }
            else
            {
                Debug.LogWarning("SpawnPoint is not assigned.");
            }
        }
    }
}
