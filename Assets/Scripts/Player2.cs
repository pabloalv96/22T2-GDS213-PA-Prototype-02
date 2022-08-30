using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Vector2 rawInput;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Shooter shooter;

    Vector2 minBounds2;
    Vector2 maxBounds2;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBounds();
    }

    private void Update()
    {
        Move();
        Fire();
    }


    void InitBounds()
    {
        Camera mainCamera = Camera.main;

        minBounds2 = mainCamera.ViewportToWorldPoint(new Vector2(0.55f, 0));
        maxBounds2 = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 gama = transform.position;

        Vector2 newPos2 = new Vector2();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            gama.y += moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            gama.y -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gama.x -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gama.x += moveSpeed * Time.deltaTime;
        }


        newPos2.x = Mathf.Clamp(gama.x, minBounds2.x + paddingLeft, maxBounds2.x - paddingRight);
        newPos2.y = Mathf.Clamp(gama.y, minBounds2.y + paddingBottom, maxBounds2.y - paddingTop);


        transform.position = newPos2;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void Fire()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            shooter.isFiring = true;
        } else
        {
            shooter.isFiring = false;
        }
    }

}
