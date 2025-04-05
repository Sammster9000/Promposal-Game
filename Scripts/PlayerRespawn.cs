using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    [SerializeField] private Transform currentCheckpoint;
    public Sprite checkPoint;
    public SpriteRenderer spriteRenderer;

    private Health playerHealth;

    private void Awake() {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn() {
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.tag == "Checkpoint") {
            currentCheckpoint = collision.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            spriteRenderer.sprite = checkPoint;
        }
    }

    private void Update() {
        if(playerHealth.dead) {
            Invoke("Respawn", 2);
        }
    }
}
