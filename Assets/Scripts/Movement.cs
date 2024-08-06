using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
  [SerializeField] float horizontalSpeed;
  [SerializeField] float jumpSpeed;
  [SerializeField] Rigidbody2D rgbPlayer;
  [SerializeField] Animator anime;
  [SerializeField] bool TouchingGround;
  [SerializeField] GameObject SnowBall;
  [SerializeField] GameObject playerGO;
  [SerializeField] Vector3 SnowballStartOffset; 
  [SerializeField] Vector2 facing;
  [SerializeField] Transform MyLook;
  [SerializeField] Transform player;
  [SerializeField] Text txtlives;
  private int lives;
  private bool newLife;
  // Start is called before the first frame update
    void Start()
    {
        newLife = false;
        lives = 3;
    }
    // Update is called once per frame
    void Update()
    {
      if(lives == 0)
      {
        SceneManager.LoadSceneAsync(3);
      }
      if (Input.GetButtonDown("Fire1"))
      {
        anime.SetTrigger("Firing");
        if(facing.x==1)
        {
          SnowballStartOffset.x=Mathf.Abs(SnowballStartOffset.x);
        }
        else if(facing.x==-1)
        {
          SnowballStartOffset.x =Mathf.Abs(SnowballStartOffset.x)*-1;
        }
        Instantiate(SnowBall,MyLook.position+SnowballStartOffset,Quaternion.identity,this.transform);
      }
     
      if(newLife == true)
     {
        Instantiate(playerGO,new Vector3(0,-3.77f,0),Quaternion.identity);
      }

    }
    private void FixedUpdate()
    {
      float horizontalaxis = Input.GetAxis("Horizontal")*horizontalSpeed;
      anime.SetFloat("Walking",horizontalaxis);
      rgbPlayer.AddForce(new Vector2(horizontalaxis,0),ForceMode2D.Impulse);

      Vector3 playerRot = MyLook.rotation.eulerAngles;
      if(Input.GetAxis("Horizontal")>0)
      {
        MyLook.rotation=Quaternion.Euler(0,0,0);
        player.localRotation= Quaternion.Euler(0,0,0);
      }
      else if(Input.GetAxis("Horizontal")<0)
      {
        MyLook.rotation=Quaternion.Euler(0,180,0);
        player.localRotation= Quaternion.Euler(0,180,0);
      }
      facing.x=(MyLook.rotation.y==0 ? 1:-1);

      if(Mathf.Abs(rgbPlayer.velocity.y)==0)
      {
        float verticalaxis = Input.GetAxis("Jump")*jumpSpeed;
        rgbPlayer.AddForce(new Vector2(0,verticalaxis),ForceMode2D.Impulse);
        anime.SetFloat("Jumping",verticalaxis);
      }

    }
    public Vector2 getFacingDirection()
    {
        return facing;
    }
      private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("enemy"))
        {
            Physics.IgnoreLayerCollision (9,8, true);
            lives--;
            txtlives.text = "Lives:  " + lives.ToString();
            anime.SetTrigger("die");
        }
        if(other.gameObject.CompareTag("littleEnemy"))
        {
            Physics.IgnoreLayerCollision (9,8, true);
            lives--;
            txtlives.text = "Lives:  " + lives.ToString();
            anime.SetTrigger("die");
        }
    }
}
