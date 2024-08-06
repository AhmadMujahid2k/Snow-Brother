using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LittleEnemyMovement: MonoBehaviour
{
    private int snowballcounter;
    [SerializeField] Animator anime;
    [SerializeField] Transform littleEnemy;
    [SerializeField] float enemySpeedFactor;
    [SerializeField] Rigidbody2D rgbPlayer;
    bool check;

    void Start()
    {
        check = false;
        snowballcounter = 5;
    }

    void Update()
    {
        //movement pending
        if(snowballcounter == 0)
        {
            Destroy(this.gameObject,0.3f);
        }
        if(Mathf.Abs(rgbPlayer.velocity.y)==0)
       {
         if(check == false && littleEnemy.position.x > littleEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x )
            { 
               
                Vector3 newPosition = littleEnemy.position; 
                newPosition.x = newPosition.x - (enemySpeedFactor); 
                littleEnemy.position = newPosition; 
                transform.localRotation= Quaternion.Euler(0,0,0);
            }
            if(check == true && littleEnemy.position.x < littleEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpDownPoint.position.x )
            {
  
                Vector3 newPosition = littleEnemy.position; 
                newPosition.x = newPosition.x + (enemySpeedFactor); 
                littleEnemy.position = newPosition; 
                transform.localRotation= Quaternion.Euler(0,180,0);
            }
            if(littleEnemy.position.x < littleEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x )
            {
                check = true;
            }
            if(littleEnemy.position.x > littleEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpDownPoint.position.x )
            {
                check = false;
            }
       }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
         if(other.gameObject.CompareTag("snowball"))
        {
            snowballcounter--;
            anime.SetInteger("littlehealth",snowballcounter);
        }
    }
}
