using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerEngine_v2 : MonoBehaviour
{
    [Header("Game Manager")]
    Transform cam;

    [Header("Notes check")]
    public int notesAmount;
    public bool isCollected;
    public TextMeshProUGUI noteAmountTxt;

    [Header("Slender check")]
    public int lives = 3;
    private float maxDistance;
    float timer;
    public float currentTime;
    float timeReduce;
    public bool isSeen;
    Teleporter_v2 teleporterScript;
    GameObject slender;
    public static float minDistance;

    // Start is called before the first frame update
    void Start()
    {
        notesAmount = 0;
        teleporterScript = GameObject.Find("Slender").GetComponent<Teleporter_v2>();
        isCollected = false;
        currentTime = 0;
        timer = 1;
        timeReduce = 0.25f; 
        cam = GameObject.Find("Main Camera").transform;
        slender = GameObject.FindGameObjectWithTag("Slender");
        minDistance = 2;
        maxDistance = 4;
    }

    // Update is called once per frame
    void Update()
    {
        noteAmountTxt.text = notesAmount.ToString("0");
        
        NoteCollected();
        GoGetMe();
        WinOrDead();

        if (Vector3.Distance(transform.position, slender.transform.position) < maxDistance)
        {
            transform.LookAt(slender.transform);
        }


        /*// rons Slender//
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5f) && hit.collider.tag == "Slender")
        {
            Transform slenderon = GameObject.Find("SlendeRon").GetComponent<Transform>();
            slender.transform.position = Vector3.zero;
            DecLife();
        }
        //////////////*/
    }

    public void DecLife()
    {
        lives--;
    }

    public void NoteCollected()
    {
        RaycastHit hitN;
        if (
            Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hitN, 2f)
            && hitN.collider.tag == "Note"
            )
        {
            print("Note collected!");
            isCollected = true;
        }
        else isCollected = false;
    }

    

    public void GoGetMe()
    {
        RaycastHit hitS;

        if (notesAmount >= 4 && Vector3.Distance(transform.position, slender.transform.position) >= minDistance)
        {
            teleporterScript.Follow();
        }
        else if(notesAmount >= 4
            && Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hitS, maxDistance)
            && hitS.collider.tag == "Slender")
        {
            print("Can you see me?");
            currentTime += Time.deltaTime;
            if (currentTime >= timer)
            {
                print("Time's over!");
                currentTime = 0;
                lives -= 1;
                slender.transform.position = new Vector3(Random.Range(9, 84), 0, Random.Range(13, 85));
                timer -= timeReduce;
                //GameObject.Find("SkyCycle").GetComponent<ChangeTheSky>().GettingDarker();
            }
        }
        else teleporterScript.RandomJumps();
    }


    public void WinOrDead()
    {
        if (notesAmount >= 8)
        {
            SceneManager.LoadScene("YouWinScene");
            GameObject.Find("SkyCycle").GetComponent<ChangeTheSky>().SetDefault();
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
            GameObject.Find("SkyCycle").GetComponent<ChangeTheSky>().SetDefault();
        }
    }

    IEnumerator Wait(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
    }
}








    /*public void LookForSlender()
    {
        RaycastHit hitS;
        if (
            notesAmount >= 2
            && Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hitS, maxDistance)
            && hitS.collider.tag == "Slender"
            )
        {
            print("Can you see me?");
            currentTime += Time.deltaTime;
            if (currentTime >= timer)
            {
                print("Time's over!");
                currentTime = 0;
                lives -= 1;
                slender.transform.position = Vector3.zero;
            }
        }


        if (notesAmount >= 3 && isSeen)
        {
            timer -= timeReduce;
        }
    }*/

/*________BACKUP BOX__________________
 * 
 * 
 * 
 * 
 * public void NoteCollected() OLD ONE
    {
        RaycastHit hitN;
        //Teleporter_v2 tele = GameObject.FindWithTag("Slender").GetComponent<Teleporter_v2>();

        if (
            Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hitN, 2f)
            && hitN.collider.tag == "Note"
            )
        {
            isCollected = true;
            print("Note collected!");
        }
        Wait(1);
        notesAmount += 1;
        isCollected = false;
    }



 public class PlayerEngine_v2 : MonoBehaviour
{
    [Header("Game Manager")]
    GameManager gameManager;
    AudioSource ads;
    Transform cam;

    //[Header("Notes check")]
    //public LayerMask notes;

    [Header("Slender check")]
    public int lives = 3;
    Transform slender;
    private float maxDistance;
    float timer;
    public float currentTime;
    float index;
    public bool isSeen;

    // Start is called before the first frame update
    void Start()
    {

        isSeen = false;
        currentTime = 0;
        timer = 5;
        index = 0.25f;
        cam = GameObject.Find("Main Camera").transform;
        slender = GameObject.Find("Slender").transform;
        maxDistance = 3;
    }

    // Update is called once per frame
    void Update()
    {
        NoteCollected();
        HelloSlender();
        //TestFunc();
    }

    public void NoteCollected()
    {
        RaycastHit hitN;
        Teleporter tele = GameObject.FindWithTag("Slender").GetComponent<Teleporter>();

        if (
            Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hitN, 2f)
            && hitN.collider.tag == "Note"
            )
        {
            print("Note is detected!");
        }
        tele.noteCollected = false;
    }

    public void HelloSlender()
    {
        RaycastHit hitS;
        Teleporter_v2 tele = GameObject.FindWithTag("Slender").GetComponent<Teleporter_v2>();
        if (
            tele.notesAmount >= 3
            && Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hitS, maxDistance)
            && hitS.collider.tag == "Slender"
            )
        {
            currentTime += Time.time;  //somehow it works \o/
            tele.meetingHappened = true; 
            if (currentTime >= timer)
            {
                Wait(timer);
                lives -= 1;
                tele.transform.position = Vector3.zero;  //made it for tests
                currentTime = 0;
            }
            
            /*isSeen = true;
            tele.meetingHappened = true;
            print("I AM TOO CLOSE");
            if(isSeen)
             {
                currentTime += Time.time;
                if(currentTime >= timer)
                {
                    lives -= 1;
                    isSeen = false;
                }
            }*//*
            //waitforseconds

            *//*lives -= 1;
            isSeen = false;
            currentTime += Time.time;
            if(currentTime >= timer)
            {
                tele.meetingHappened = true;
            }*//*

            //lives -=1;
            //tele.meetingHappened = false;
            //currentTime = 0;
        }
        *//*lives -= 1;
        tele.meetingHappened = false;
        currentTime = 0;*//*

        if (tele.notesAmount >= 3 && tele.meetingHappened)
{
    timer -= index;
}
    }

    IEnumerator Wait(float waitingTime)
{
    yield return new WaitForSeconds(waitingTime);
}

public void TestFunc()
{

}

}*//**/
