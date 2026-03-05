using UnityEngine;

public class VillageCapture : MonoBehaviour
{
    public float captureTime = 10f;
    private float timer = 0f;
    private bool playerInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            timer = 0f;
        }
    }

    void Update()
    {
        if (playerInside)
        {
            timer += Time.deltaTime;

            if (timer >= captureTime)
            {
                Debug.Log("Village Captured!");
                timer = 0f;
            }
        }
    }
}