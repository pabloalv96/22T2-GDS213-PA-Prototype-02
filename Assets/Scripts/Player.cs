using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Vector2 rawInput;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Shooter shooter;

    Vector2 minBounds1;
    Vector2 maxBounds1;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBounds();
    }
    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;

        minBounds1 = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds1 = mainCamera.ViewportToWorldPoint(new Vector2(.45f, 1)); 
    }


    public void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;

        Vector2 newPos1 = new Vector2();
        newPos1.x = Mathf.Clamp(transform.position.x + delta.x, minBounds1.x + paddingLeft, maxBounds1.x - paddingRight);
        newPos1.y = Mathf.Clamp(transform.position.y + delta.y, minBounds1.y + paddingBottom, maxBounds1.y - paddingTop);

        transform.position = newPos1;
    }
    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

}
