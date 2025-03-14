using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public BeatManager heartbeat;
    public MusicManager musicManager;
    // public RhythmGameManager rhythmGame; // Reference to the rhythm game


    public GameObject entityPrefab;
    private GameObject entityInstance;  // Keep a single instance
    public Transform player;
    public float minSpawnDist = 5f;
    public float maxSpawnDist = 10f;
    public float teleportInterval = 10f;  // Time between teleports
    public LayerMask obstacleLayer;

    private bool entityVisible = false;

    void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);  // Keep it across scenes
    }
    else
    {
        Destroy(gameObject);
    }
}


    void Start()
    {
        // Create a single entity but keep it inactive
        entityInstance = Instantiate(entityPrefab, Vector3.zero, Quaternion.identity);
        entityInstance.SetActive(false);  // Initially hidden

        StartCoroutine(TeleportEntity());
    }

    private IEnumerator TeleportEntity()
{
    while (true)
    {
        // Wait for a random time between 30 to 45 seconds (adjust as needed)
        float randomInterval = Random.Range(30f, 45f); // You can change 30f and 45f to any other range you prefer
        yield return new WaitForSeconds(randomInterval);

        if (!entityVisible)  // Only teleport if it's currently hidden
        {
            Vector3 newPosition = GetValidSpawnPosition();
            if (newPosition != Vector3.zero)
            {
                entityInstance.transform.position = newPosition;
                entityInstance.SetActive(true);  // Make it visible
                entityVisible = true;

                // Wait for 15 seconds before starting the danger sequence
                yield return new WaitForSeconds(15f);  // 15-second delay before triggering danger sequence
                StartDangerSequence();  // Trigger the danger sequence
            }
        }
    }
}



    private Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 10; i++) // Try 10 times to find a valid position
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 potentialPosition = player.position + (Vector3)(randomDirection * Random.Range(minSpawnDist, maxSpawnDist));

            if (IsWithinCameraView(potentialPosition) && !Physics2D.OverlapCircle(potentialPosition, 1f, obstacleLayer))
            {
                return potentialPosition;
            }
        }
        return Vector3.zero; // No valid position found
    }

    private bool IsWithinCameraView(Vector3 position)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(position);
        return viewportPoint.x > 0.2f && viewportPoint.x < 0.8f && viewportPoint.y > 0.2f && viewportPoint.y < 0.8f;
    }

    public void StartDangerSequence()
    {
        heartbeat.triggerHeightened();
        musicManager.PlayDangerMusic();

    //    if (rhythmGame != null)
    //     {
    //         rhythmGame.StartRhythmSequence(OnRhythmGameEnd);
    //     }
    //     else
    //     {
    //         Debug.LogWarning("RhythmGameManager is not assigned.");
    //         StartCoroutine(HandleEntityVisibility()); // Fallback if rhythmGame is missing
    //     }
    }

    private void OnRhythmGameEnd()
    {
        StartCoroutine(HandleEntityVisibility());
    }

    private IEnumerator HandleEntityVisibility()
    {
        yield return new WaitForSeconds(2f);  // Duration of the danger cue
        entityInstance.SetActive(false);  // Hide the entity
        entityVisible = false;

        // Reset heartbeat and music
        heartbeat.resetNormal();
        musicManager.StopDangerMusic();
        musicManager.PlayRhythmMusic();
    }

}
