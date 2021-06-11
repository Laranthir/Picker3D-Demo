using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private GameObject player;
    private Camera camera;

    public float duration = 5f;

    private void Start()
    {
        camera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            StartCoroutine(CinematicCamera());
        }
    }

    private IEnumerator CinematicCamera()
    {
        GameManager.Instance.speedForward += 10f;
        GameManager.Instance.controllerActive = false;
        player.transform.DOMoveX(0, 1f);
        camera.transform.DOLocalMoveZ(0.08f, duration);
        GameManager.Instance.ResetToDefaultSize();
        camera.transform.DOLocalRotate(new Vector3(-90, 180, 0),duration);
        yield return new WaitForSeconds(duration + 0.25f);
        camera.transform.DOLocalMoveZ(0.18f, duration);
        camera.transform.DOLocalRotate(new Vector3(-65, 180, 0),duration);
        yield return new WaitForSeconds(duration + 0.25f);
        GameManager.Instance.speedForward -= 10f;
        GameManager.Instance.controllerActive = true;
        GameManager.Instance.currentLevel++;
        GameManager.Instance.checkpointReached = 0;
        GameManager.Instance.UpdateLevelBar(true);
        GameManager.Instance.StopGame();
        GameManager.Instance.YouWon();
    }
}
