using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip walk;
    [SerializeField] private float speed;
    [SerializeField] private float scale;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
    	body = GetComponent<Rigidbody2D>();
	    anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
	    float horizontalInput = Input.GetAxis("Horizontal");
	
	//Move L/R
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

	//Flip player on move left/right
	if(horizontalInput > 0.01f) 
		transform.localScale = new Vector3(scale, scale, 1);
	else if (horizontalInput < -0.01f)
		transform.localScale = new Vector3(-1*scale, scale, 1);

	//Jump
	if(Input.GetKey(KeyCode.UpArrow) && grounded)
		Jump();

	anim.SetBool("Running", horizontalInput != 0);
	anim.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
	body.velocity = new Vector2(body.velocity.x, speed + 1);
    anim.SetTrigger("Jump");
	grounded = false;
    //jump.Volume = 0.5f;
    SoundManager.instance.PlaySound(jump, 0.5f);
    }	

    private void OnCollisionEnter2D(Collision2D collision)
    {
	    if(collision.gameObject.tag == "Ground") {
	    	grounded = true;
        }     
    }

}
