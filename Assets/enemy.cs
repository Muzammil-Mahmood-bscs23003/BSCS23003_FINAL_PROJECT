using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject playerHelicopter;
    public float speed = 35f;
    public float rotationSpeed = 35f;
    public AudioClip collisionSound;
    private AudioSource audioSource;
    private Vector3 originalSpawnPoint;

    void Start()
    {
        originalSpawnPoint = transform.position;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (playerHelicopter == null)
        {
            playerHelicopter = GameObject.Find("Helicopter");
            if (playerHelicopter == null) return;
        }

        Vector3 direction = (playerHelicopter.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Helicopter"))
        {
            transform.position = originalSpawnPoint;
            Debug.Log("Respawned");
            transform.rotation = Quaternion.identity;

            if (collisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }
        }
    }
}
