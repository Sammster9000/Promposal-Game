using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.UI;

public class key : MonoBehaviour
{
    [SerializeField] private AudioClip collect;
    [SerializeField] public GameObject door;
    [SerializeField] public GameObject doorOpen;
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    [SerializeField] public GameObject NPC;

    public NPC npc;

    private bool movingUp;
    private float topEdge;
    private float bottomEdge;
    private float midPoint;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player") {
        door.SetActive(false);
        doorOpen.SetActive(true);
        NPC.transform.position = new Vector3(NPC.transform.position.x - 4, NPC.transform.position.y, NPC.transform.position.z);
        npc.lastDialogueSet = true;
        gameObject.SetActive(false);
        SoundManager.instance.PlaySound(collect, 2.5f);
        }
    }

    private void Awake() {
        topEdge = transform.position.y + moveDistance;
        bottomEdge = transform.position.y - moveDistance;
        float pathLength = Math.Abs(topEdge - bottomEdge);
        midPoint = bottomEdge + pathLength;
    }

    private void Update() {
        if(movingUp){
            if(transform.position.y < topEdge) {
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
            } else {
                movingUp = false;
            }
            if(transform.position.y > midPoint) speed -= (float)0.05;
            else speed += (float)0.05;
        } else {
            if(transform.position.y > bottomEdge) {
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
            } else {
                movingUp = true;
            }
            if(transform.position.y < midPoint) speed -= (float)0.05;
            else speed += (float)0.05;
        }
    }
}

