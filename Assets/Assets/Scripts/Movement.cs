using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform sizeUpSpawner;
    [SerializeField] private GameObject sizeUpText;
    [SerializeField] private GameObject rotatingBlades;
    
    private Touch touch;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveForward();
        TouchController();
    }
    public void Move()
    {
        //Using Rigidbody
        rb.velocity = Vector3.forward * -GameManager.Instance.speedForward;
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void TouchController()
    {
        if (GameManager.Instance.controllerActive)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Moved)
                {
                    rb.velocity += Vector3.left * (touch.deltaPosition.x * GameManager.Instance.speedTouch * Time.deltaTime);
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10.5f,10.5f), (transform.position.y),
                        (transform.position.z));
                    GameManager.Instance.StartGame();
                }
            }
        }
    }
    
    public void ActivateBlades()
    {
        rotatingBlades.SetActive(true);
        Debug.Log("POWER UP!");
    }

    public void DeactivateBlades()
    {
        rotatingBlades.SetActive(false);
        Debug.Log("POWER UP EXPIRED!");
    }
    
    public void SizeUp()
    {
        transform.DOScale(transform.localScale + new Vector3(20,20,20),1f);
        Instantiate(sizeUpText, sizeUpSpawner.position, sizeUpSpawner.rotation, gameObject.transform);
    }
    
    public void ResetToDefaultSize()
    {
        transform.localScale = new Vector3(100, 100, 100);
    }
    
    public void MoveForward()
    {
        if (GameManager.Instance.isMoving)
        {
            Move();
        }
        else if (!GameManager.Instance.isMoving)
        {
            StopMoving();
        }
    }
}
