using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    //notes
    public GameObject[] notesStatic; //change name to Static
    public GameObject[] notesRandom;

    private GameObject[] createdNotesStatic; //change name to Static
    private GameObject[] createdNotesRandom;

    //locations of notes
    public Transform[] notesPositionStatic; //change name to Static
    public Transform[] notesPositionsRandom;

    // Start is called before the first frame update
    void Start()
    {
        CreateNotes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateNotes() //if place is taken, not to put it there
    {
        createdNotesStatic = new GameObject[notesStatic.Length];
        for (int i = 0; i < notesStatic.Length; i++)
        {
            createdNotesStatic[i] = Instantiate((notesStatic[i]), notesPositionStatic[i].position, transform.rotation);
        }

        createdNotesRandom = new GameObject[notesRandom.Length];

        for (int i = 0; i < 4; i++)
        {
            createdNotesRandom[i] = Instantiate(notesRandom[i], notesPositionsRandom[Random.Range(0, notesPositionsRandom.Length)].position, transform.rotation);
        }
    }
}

/*box for backup*/
