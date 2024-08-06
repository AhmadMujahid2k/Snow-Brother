using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallController : MonoBehaviour
{
    [SerializeField] Rigidbody2D SnowBallRgb;
    [SerializeField] float ShootingForce;
    [SerializeField] Vector2 SnowBallDirection;
    [SerializeField] Animator ballanime;
   // [SerializeField] GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        SnowBallDirection.x = transform.parent.GetComponent<Movement>().getFacingDirection().x;
        SnowBallRgb.AddForce((SnowBallDirection.normalized)*ShootingForce,ForceMode2D.Impulse);
        transform.parent=null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        ballanime.SetTrigger("Touch");
        if(other.gameObject.CompareTag("platform"))
        {
            Destroy(this.gameObject,0.3f);
        }
    }
}
