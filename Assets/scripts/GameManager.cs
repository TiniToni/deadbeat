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

    private GameObject currentEntity; // Track active entity

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
    

    void Start()
    {
        StartCoroutine(SpawnEntityLoop());
    }

    private IEnumerator SpawnEntityLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (currentEntity == null) // Ensure only one entity exists
            {
                //TriggerEntitySpawn();
            }
        }
    }

    private void TriggerEntitySpawn()
    {
        if (entityPrefab == null || player == null)
        {
            Debug.LogWarning("Entity or player reference is missing.");
            return;
        }

        Vector3 spawnPosition = GetValidSpawnPosition();

        if (spawnPosition != Vector3.zero)
        {
            currentEntity = Instantiate(entityPrefab, spawnPosition, Quaternion.identity);

            // Assign the "Entity" layer for easier detection
            currentEntity.layer = LayerMask.NameToLayer("Entity");
        }
    }

    private Vector3 GetValidSpawnPosition()
    {
        Vector3 facingDirection = player.right.normalized; // Ensure entity spawns in front of player
        Vector3 potentialPosition = player.position + facingDirection * Random.Range(minSpawnDist, maxSpawnDist);

        if (!Physics2D.OverlapCircle(potentialPosition, 1f, obstacleLayer))
        {
            return potentialPosition;
        }

        return Vector3.zero; // No valid position found
    }

    public void StartDangerSequence(GameObject entity)
    {
        if (currentEntity == entity) // Ensure it's the active entity
        {
            heartbeat.triggerHeightened();
            musicManager.PlayDangerMusic();
            StartCoroutine(HandleEntityDespawn(entity));
        }
    }

    private IEnumerator HandleEntityDespawn(GameObject entity)
    {
        yield return new WaitForSeconds(dangerCueDuration); // Danger cue duration before rhythm sequence
        Destroy(entity);
        currentEntity = null;

        // Reset heartbeat and start rhythm game
        heartbeat.resetNormal();
        musicManager.PlayRhythmMusic();
    }

}
