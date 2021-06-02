using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketLaunchManager : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private float _currentSpeed;

    [SerializeField] private GameObject rocket2;
    [SerializeField] private GameObject rocket3;
    private GameObject rocket;

    private Rigidbody2D rb;

    private bool launch = false;

    private int i = 1;

    private Vector3 roc1pos;
    private Vector3 roc2pos;
    private Vector3 roc3pos;
    private void Start()
    {
        roc1pos = transform.position;
        roc2pos = rocket2.transform.position;
        roc3pos = rocket3.transform.position;

        _currentSpeed = speed;
    }

    private void NewStart()
    {
        Debug.Log("На исходную");

        transform.position = roc1pos;
        rocket2.transform.position = roc2pos;
        rocket3.transform.position = roc3pos;

        transform.transform.eulerAngles = new Vector3(0, 0, 0);
        rocket2.transform.transform.eulerAngles = new Vector3(0, 0, 0);
        rocket3.transform.transform.eulerAngles = new Vector3(0, 0, 0);

        launch = false;
        _currentSpeed = speed;
    }
    private void Update()
    {
        if (i == 4) {i = 1; Invoke("NewStart", 4.0f); }

        if (transform.position.y <= 8.29f && launch == false && i == 1) {
           rb = GetComponent<Rigidbody2D>();
           transform.Translate(new Vector3(0, 1f, 0) * _currentSpeed * Time.deltaTime);
           rb.gravityScale = 0.1f;
           }
        else {
           if (launch == false && i == 1 && transform.position.y >= 8.29f) {
                Debug.Log($"Отказ двигателя Ракеты {i}");
                rb.gravityScale = 1f;
                transform.Rotate(new Vector3(0, 0, Random.Range(-45.0f, 45.0f)));
                _currentSpeed = 0;
                i++;
                launch = true; //рандомній коммит  вфвфі
            }       
         }

        if (i == 2) { rocket = rocket2; launch = false; _currentSpeed = 5; }
        else if (i == 3) { rocket = rocket3; launch = false; _currentSpeed = 5; }
        else if (i == 1) { rocket = null; }

        if (rocket != null && rocket.transform.position.y <= 8.29f && launch == false)
        {
            rb = rocket.GetComponent<Rigidbody2D>();
            rocket.transform.Translate(new Vector3(0, 1f, 0) * _currentSpeed * Time.deltaTime);
            rb.gravityScale = 0.1f;
        }
        else
        {
            if (launch == false && rocket != null && i != 1)
            {
                Debug.Log($"Отказ двигателя Ракеты {i}");
                rb.gravityScale = 1f;
                rocket.transform.Rotate(new Vector3(0, 0, Random.Range(-45.0f, 45.0f)));
                _currentSpeed = 0;
                i++;
                launch = true;
            }
        }   
    }
}
