using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movSpeed;      //Player's movement speed
    public AudioSource ambience, steps;
    Rigidbody rbPlayer;
    // Start is called before the first frame update
    void Start()
    {
        ambience.volume = 0.2f;
        ambience.Play();
        rbPlayer = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Camera.main.transform.forward*Input.GetAxis("Vertical");
        movement += Camera.main.transform.right*Input.GetAxis("Horizontal");
        movement.y = 0;
        transform.Translate(movement*Time.deltaTime*movSpeed, Space.Self);
        if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)&&!steps.isPlaying) steps.Play();
        
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            if (hit.collider.tag == "Escaleras") rbPlayer.AddForce(Vector3.up*10);
        }
    }

}
