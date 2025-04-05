using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] private AudioClip meow;
    public GameObject dialoguePanel;
    public GameObject secretPlatform;
    public GameObject key;
    public Text dialogueText;
    public Text z;
    public string[] lines;
    public float textSpeed;
    public bool playerIsClose;

    public bool lastDialogueSet;
    public string[] lines2;

    private int index;


    // Update is called once per frame
    //

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerIsClose = true;
            secretPlatform.SetActive(true);
            key.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerIsClose = false;
            flushText();
        }
    }

    void Update()
    {
        if(lastDialogueSet) lines = lines2;
        if(Input.GetKeyDown(KeyCode.Z) && playerIsClose) {
            SoundManager.instance.PlaySound(meow);
            if (dialoguePanel.activeInHierarchy) {
                    if(dialogueText.text ==  lines[index]) {
                    NextLine();
                } else {
                    StopAllCoroutines();
                    dialogueText.text = lines[index];
                }
            } else {
                dialoguePanel.SetActive(true);
                StartDialogue();
            }
        }
    }
    public void flushText() {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());

    }

    IEnumerator TypeLine() {
        foreach(char c in lines[index].ToCharArray()) {
            dialogueText.text+= c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if(index < lines.Length - 1) {
            index++;
            dialogueText.text = "";
            StartCoroutine(TypeLine());
            if(index == lines.Length - 1 && lastDialogueSet) {
                z.text = "[Z] Yes";
                z.transform.position = new Vector3(z.transform.position.x - 2, z.transform.position.y, z.transform.position.z);
                z.color = new Color(0, 1, 0);
            }
        } else {
            dialoguePanel.SetActive(false);
            flushText();
        }
    }

    //Thanks Bella! Now we can make it to prom!
    //Will you go with me? ILY!!
}
