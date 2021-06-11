using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;


public class BallCounter : MonoBehaviour
{
    [SerializeField] int objective;
    [SerializeField] Transform plane;
    [SerializeField] Transform leftBar;
    [SerializeField] Transform rightBar;
    [SerializeField] private Component textBox;
    [SerializeField] private GameObject goodJob;
    [SerializeField] private GameObject confetti;

    private TMP_Text m_TextComponent;
    
    private int sphereCounter = 0;
    private bool planeRising = true;

    private float timer = 0f;
    private float failTimer = 2500f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sphere"))
        {
            sphereCounter++;
            timer = 2f;
            failTimer = 5f;
            GameManager.Instance.score++;
            GameManager.Instance.UpdateScore(true);
        }
    }

    private void Awake()
    {
        m_TextComponent = textBox.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        m_TextComponent.text = sphereCounter + "/" + objective;
        timer -= Time.deltaTime;
        failTimer -= Time.deltaTime;
        
        if (sphereCounter >= objective && planeRising & timer < 0)
        {
            StartCoroutine(EventSequence());
            planeRising = false;
        }

        if (sphereCounter < objective && failTimer < 0)
        {
            GameManager.Instance.YouLost();
        }
        
        
    }
    
    IEnumerator EventSequence()
    {
        plane.DOMoveY(1, 0.75f);
        yield return new WaitForSeconds(0.95f);
        plane.DOMoveY(0, 0.375f);
        yield return new WaitForSeconds(0.5f);
        leftBar.DORotate(new Vector3(45, -90, -90), 1.5f);
        rightBar.DORotate(new Vector3(45, 90, 90), 1.5f);
        goodJob.SetActive(true);
        goodJob.transform.DOMoveY(17.5f, 1f);
        goodJob.transform.DOScale(new Vector3(2.5f * 1.5f, 1.75f * 1.5f, 2.25f), 1f);
        yield return new WaitForSeconds(1.25f);
        goodJob.transform.DOScale(new Vector3(0.25f, 0.25f, 2.25f),1f);
        yield return new WaitForSeconds(0.75f);
        goodJob.SetActive(false);
        confetti.SetActive(true);
        
        GameManager.Instance.UpdateCheckpointBar(true);
        GameManager.Instance.isMoving = true;
        GameManager.Instance.controllerActive = true;
        GameManager.Instance.SizeUp(true);
        yield return new WaitForSeconds(3.5f);
        confetti.SetActive(false);
    }
}
