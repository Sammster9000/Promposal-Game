using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private AudioClip dmgSound;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<Health>().Hurt(damage);
            SoundManager.instance.PlaySound(dmgSound);
        }
    }
}
