using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            gameObject.layer = 9;

        if (other.gameObject.CompareTag("BallCounter"))
        {
            StartCoroutine(SelfDestruct());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            gameObject.layer = 12;
    }
}
