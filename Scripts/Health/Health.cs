using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration; 
    [SerializeField] private int numFlashes;
    private SpriteRenderer spriteRend;

    private void Awake() 
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        dead = false;
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void Hurt(float _damage) 
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if(currentHealth > 0) {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
        } else {
            if(!dead) {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                dead = true;
            }
        }
    }

    public void Heal(float _heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + _heal, 0, startingHealth);
    }

    public void Respawn() {
        dead = false;
        Heal(startingHealth);  
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());
        GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator Invulnerability() {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        //inv duration
        //
        for(int i = 0; i < numFlashes; i++) {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration);
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);

    }
    
}
