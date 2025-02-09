using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public BeatManager heartbeat;
    public MusicManager musicManager;

    public GameObject entityPrefab;
    public Transform player;
    public float spawnInterval = 10f;
    public float minSpawnDist = 5f; 
    public float maxSpawnDist = 10f; 
    public LayerMask obstacleLayer;
    public float dangerCueDuration = 2f;

    private bool entityActive = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEntity());
    }

    private IEnumerator spawnEntity()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (!entityActive)
            {
                TriggerRandomEntity();
            }
        }
    }

    private void TriggerRandomEntity()
{
    if (entityPrefab == null || player == null)
    {
        Debug.LogWarning("Entity or player reference is missing.");
        return;
    }

    // Check if another entity exists within a certain radius
    if (Physics2D.OverlapCircle(player.position, maxSpawnDist, LayerMask.GetMask("Entity")))
    {
        return; // Prevents overwhelming spawns
    }

    Vector3 spawnPosition = GetValidSpawnPosition();

    if (spawnPosition != Vector3.zero)
    {
        GameObject spawnedEntity = Instantiate(entityPrefab, spawnPosition, Quaternion.identity);
        entityActive = true;

        // Assign the "Entity" layer to control spawning logic
        spawnedEntity.layer = LayerMask.NameToLayer("Entity");

        StartCoroutine(HandleEntityLife(spawnedEntity));
    }
}

public void StartDangerSequence()
{
    heartbeat.triggerHeightened();
    musicManager.PlayDangerMusic();
}
private Vector3 GetValidSpawnPosition()
{
    Vector3 facingDirection = player.right.normalized; // Adjust if needed for your character's facing direction
    Vector3 potentialPosition = player.position + facingDirection * Random.Range(minSpawnDist, maxSpawnDist);

    if (!Physics2D.OverlapCircle(potentialPosition, 1f, obstacleLayer))
    {
        return potentialPosition;
    }

    return Vector3.zero; // No valid position found
}


    private bool IsWithinCameraView(Vector3 position)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(position);
        return viewportPoint.x > 0.2f && viewportPoint.x < 0.8f && viewportPoint.y > 0.2f && viewportPoint.y < 0.8f;
    }

    private IEnumerator HandleEntityLife(GameObject entity)
    {
        yield return new WaitForSeconds(dangerCueDuration);  // Danger cue duration before rhythm sequence starts
        Destroy(entity);
        entityActive = false;

        // Reset heartbeat and stop danger music
        heartbeat.resetNormal();
        musicManager.PlayRhythmMusic();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
