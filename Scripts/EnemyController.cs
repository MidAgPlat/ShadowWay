using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
    Attack
};


public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public EnemyState currentState = EnemyState.Wander;
    public float range;
    public float speed;
    public float attackRange;
    public float coolDown;
    public float HP;
    public GameObject DropItem;
    public GameObject BackItem;

    private bool chooseDir = false;
    private bool dead = false;
    private bool coolDownAttack = false;
    private Vector3 randDir;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Die):

                break;
        }
        if(IsPlayerInRange(range) && currentState!=EnemyState.Die)
        {
            currentState = EnemyState.Follow;
        }
        else if(!IsPlayerInRange(range) && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Wander;
        }

        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randDir = new Vector3(0, 0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f,2f));
        chooseDir = false;
    }

    void Wander()
    {
        if(!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += -transform.right * speed * Time.deltaTime;

        if(IsPlayerInRange(range))
        {
            currentState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Death()
    {
        if(this.tag == "BOSS")
        {
            Instantiate(DropItem, this.transform.position, Quaternion.identity);
            Instantiate(BackItem, RoomController.instance.currRoom.GetRoomCentre(), Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void Attack()
    {
        if(!coolDownAttack)
        {
            Game_Controller.DmgPlayer(1);
            StartCoroutine(CoolDown());
        }
        
    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "LeftWall" || collision.gameObject.name == "RightWall" || collision.gameObject.name == "UpWall" || collision.gameObject.name == "DownWall")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
        }
    }

    public void MinusHP()
    {
        this.HP -= 1;
    }

    public float GetHP()
    {
        return HP;
    }
}
