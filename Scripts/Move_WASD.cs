using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_WASD : MonoBehaviour
{

    public float speed = 10.5f;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    //shots
    public static GameObject shotPrefab;
    public static GameObject newShot;
    public GameObject shotP;
    public GameObject newShotP;

    public float shotSpeed;
    private float lastFire;
    public float fireDelay;


    void Start()
    {
        newShot = newShotP;
        shotPrefab = shotP;
        rb.velocity = new Vector2(0, 0);
        Debug.Log("Im so done WHY");
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        ProcessShots();
    }

    //movement
    void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    //shooting
    void ProcessShots()
    {
        float shootX = Input.GetAxisRaw("HorizontalShoot");
        float shootY = Input.GetAxisRaw("VerticalShoot");

        if((shootX != 0 || shootY != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootX, shootY);
            lastFire = Time.time;
        }
    }

    void Shoot(float x, float y)
    {
        GameObject shot = Instantiate(shotPrefab, transform.position, transform.rotation) as GameObject;
        shot.AddComponent<Rigidbody2D>().gravityScale = 0;
        shot.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * shotSpeed : Mathf.Ceil(x) * shotSpeed,
            (y < 0) ? Mathf.Floor(y) * shotSpeed : Mathf.Ceil(y) * shotSpeed, 0);
    }

    public static void SetShot()
    {
        shotPrefab = newShot;
    }


    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Room")
        {
            Room curr = other.gameObject.GetComponent<Room>();
            RoomController.instance.OnPlayerEnterRoom(curr);
        }

        Debug.Log("WTF" + other.tag + other);
    }*/
}