using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    DialogueManager dm;

    [SerializeField] Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {   
        dm = FindObjectOfType<DialogueManager>();
        dm.StartDialogue(dialogue);
    }
}
