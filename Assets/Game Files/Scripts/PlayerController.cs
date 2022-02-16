using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI countText;
    public GameObject winTextObj;
    public Transform pickups;
    public Action Won;
    public bool won = false;

    private int count;
    private int pickupCount;
    private float movementX;
    private float movementY;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
        count = 0;

        SetCountText();
        winTextObj.SetActive(false);

        pickupCount = pickups.transform.childCount;
        Time.timeScale = 1;
        won = false;
        countText.text = "Count : " + count.ToString() + " / " + pickupCount;

        Debug.Log(pickupCount);
        Debug.Log("TimeScale: " + Time.timeScale);
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }


    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    public void SetCountText()
    {
        countText.text = "Count : " + count.ToString() + " / " + pickupCount;
        if(count >= pickupCount)
        {
            winTextObj.SetActive(true);
            Time.timeScale = 0;
            won = true;
            Won?.Invoke();
        }
    }

}
