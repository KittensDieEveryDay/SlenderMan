using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter_v2 : MonoBehaviour
{
    //variables
    Transform player;
    public float speed;
    public static float minDistance;
    public bool isFollowing;

    //timing
    float jumpTimer;
    public float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        jumpTimer = 5;
        isFollowing = false;
        currentTime = 0;

        //start position
        transform.position = Vector3.zero;

        //speed&distances
        speed = 1f;
        minDistance = 3;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        GettingCloser();
    }

    public void Follow()
    {
            transform.position += transform.forward * speed * Time.deltaTime;
            isFollowing = true;
    }

    public void GettingCloser()
    {
        PlayerEngine_v2 playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerEngine_v2>();
        if (playerScript.notesAmount >= 4 && playerScript.isCollected)
        {
            minDistance -= 0.25f;
            speed += 0.05f;
            jumpTimer -= 0.3f;
        }
    }

    public void RandomJumps()
    {
        PlayerEngine_v2 playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerEngine_v2>();
        currentTime += Time.deltaTime;
        if (playerScript.notesAmount < 4 && currentTime >= jumpTimer)
        {
            isFollowing = false;
            transform.position = new Vector3(Random.Range(9, 84), 0, Random.Range(13, 85));
            currentTime = 0;
        }

    }

    /*IEnumerator Wait(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
    }*/

    /*public void Visability()
    {
        if(Vector3.Distance(transform.position, player.position) <= minDistance)
        {
            rend.enabled = true;
            col.enabled = true;
        }

        else
        {
            rend.enabled = false;
            col.enabled = false;
        }
    }*/
}

/*______________BACKUP BOX_______________
 
 
 //moving
    public bool hasMoved;
    private IEnumerator coroutine;
    public int notesAmount;
    // Start is called before the first frame update
    void Start()
    {
        hasMoved = false;
        transform.position = Vector3.zero;
        notesAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SlenderEngine();
    }

    public void SlenderEngine()
    {
        if(notesAmount >= 3)
        {
            coroutine = Wait(Random.Range(5, 180));
            transform.position = new Vector3(Random.Range(9, 84), 0, Random.Range(13, 85));
            //new WaitForSeconds(Random.Range(5, 180));
            StartCoroutine(coroutine);
        }
        StopCoroutine("Wait");
        GetComponent<Rigidbody>().velocity = Vector3.zero;



        //hasMoved = true;
        //coroutine = Wait(5);
        //StartCoroutine(coroutine);
        //new WaitForSeconds(5);
        //GetComponent<Rigidbody>().velocity = Vector3.zero;

        /*if (hasMoved)
        {
            coroutine = Wait(5);
            StartCoroutine(coroutine);
            new WaitForSeconds(5);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }*//*
        //hasMoved = false;
    }



    IEnumerator Wait(float waitingTime)
{
    yield return new WaitForSeconds(waitingTime);
    //yield return null;
}*//**/
