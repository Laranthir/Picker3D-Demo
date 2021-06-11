using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using DG.Tweening;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject sphere;
    [SerializeField] private float spawnFrequency = 0.15f;
    [SerializeField] private float movementDuration = 5f;
    [SerializeField] private float moveSpeedForward = 10f;
    [SerializeField] private float amplitude = 5f;
    [SerializeField] private float frequency = 5f;
    
    private float timer = 0f;
    private float x, y, z;
    private bool startMoving = false;
    private float movementTimer = 0f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (startMoving)
        {
            movementTimer += Time.deltaTime;

            SinWaveMovement();
            SpawnSpheres();
            if (movementTimer > movementDuration)
            {
                movementTimer = 0f;
                TakeOff();
                startMoving = false;
            }
        }
    }

    void SpawnSpheres()
    {
        timer += Time.deltaTime;

        if (timer > spawnFrequency)
        {
            GameObject.Instantiate(sphere, spawnPoint.position, spawnPoint.rotation);
            timer = 0f;
        }
    }

    void SinWaveMovement()
    {
        x = Mathf.Sin(Time.time * frequency) * amplitude;
        y = transform.position.y;
        z = transform.position.z;
            
            
        transform.position = new Vector3(x, y, z);
        transform.position += Vector3.back * (moveSpeedForward * Time.deltaTime);
    }

    void TakeOff()
    {
        transform.DOMove(new Vector3(x + -50, y + 25, z + -50), 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            startMoving = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
