using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.controllerActive = false;
            StartCoroutine(SpeedBurst());
            
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator SpeedBurst()
    {
        GameManager.Instance.speedForward += 50f;
        GameManager.Instance.SpeedBurst(true);
        yield return new WaitForSeconds(0.04f);
        GameManager.Instance.SpeedBurst(false);
        GameManager.Instance.speedForward -= 50f;
        yield return new WaitForSeconds(0.04f);
        GameManager.Instance.isMoving = false;
        GameManager.Instance.PowerUp(false);
        GameManager.Instance.checkpointReached++;
    }
}
