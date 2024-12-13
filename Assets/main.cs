using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour
{
    Rigidbody rb;
    public GameObject heartPrefab;
    public Transform canvasTransform;
    private List<GameObject> hearts = new List<GameObject>();
    private int maxHearts = 7;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitializeHearts();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float verticalForce = 0;
        float forwardForce = 0;

        if (Input.GetKey(KeyCode.Space))
        {
            verticalForce = 200;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            verticalForce = -200;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            forwardForce = 70;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            forwardForce = -70;
        }

        rb.AddRelativeForce(0, verticalForce, forwardForce);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, -1f, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 1f, 0);
        }

        rb.velocity *= 0.98f;
    }

    void InitializeHearts()
    {
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = Instantiate(heartPrefab, canvasTransform);
            RectTransform rectTransform = heart.GetComponent<RectTransform>();

            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);

            rectTransform.anchoredPosition = new Vector2(-30 * i - 20, -20);

            hearts.Add(heart);
        }
    }

    public void OnEnemyHit()
    {
        if (hearts.Count > 0)
        {
            GameObject lastHeart = hearts[hearts.Count - 1];
            hearts.RemoveAt(hearts.Count - 1);
            Destroy(lastHeart);
        }

        if (hearts.Count == 0)
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene("gameover");
        }
    }

    public void RestoreHearts()
    {
        foreach (GameObject heart in hearts)
        {
            Destroy(heart);
        }
        hearts.Clear();

        InitializeHearts();
        Debug.Log("Hearts restored!");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyDragon"))
        {
            OnEnemyHit();
        }
    }
}
