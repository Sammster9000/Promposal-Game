using UnityEngine;
using System;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float healingFactor;
    [SerializeField] private AudioClip collect;

    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    private bool movingUp;
    private float topEdge;
    private float bottomEdge;
    private float midPoint;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player") {
        collision.GetComponent<Health>().Heal(healingFactor);
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
