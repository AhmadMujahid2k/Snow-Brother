using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement: MonoBehaviour
{
    [SerializeField] Collider2D enemyCollider;
    [SerializeField] float strikingDistance;
    [SerializeField] Transform player;
    [SerializeField] Transform selfEnemy;
    [SerializeField] Animator anime;
    [SerializeField] float enemySpeedFactor, jumpFactor;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] GameObject child;
    private bool check,touching;
    private int snowballcounter;
    
    void Start()
    {
        StartCoroutine(EnemySpawner());
        snowballcounter = 5;
        check = false;
        touching = false;
    }
    void Update()
    {
        if(snowballcounter == 0)
        {
            Destroy(this.gameObject,0.3f);
        }
        if(touching)
        {
        if((player.position-selfEnemy.position).magnitude > strikingDistance)
        {
            // do nothing
            Debug.Log("chilling");
            Debug.Log("check" + check);
            if(check == false && selfEnemy.position.x > selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x )
            { 
                float horizontalaxis = -1;
                Debug.Log( "LEFT MOvement" + horizontalaxis);
                anime.SetFloat("walking",horizontalaxis);
                Vector3 newPosition = selfEnemy.position; 
                newPosition.x = newPosition.x - (enemySpeedFactor); 
                selfEnemy.position = newPosition; 
                transform.localRotation= Quaternion.Euler(0,0,0);
            }
            if(check == true && selfEnemy.position.x < selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpDownPoint.position.x )
            {
                float horizontalaxis = 1;
                Debug.Log( "Right MOvement" + horizontalaxis);
                anime.SetFloat("walking",horizontalaxis);
                Vector3 newPosition = selfEnemy.position; 
                newPosition.x = newPosition.x + (enemySpeedFactor); 
                selfEnemy.position = newPosition; 
                transform.localRotation= Quaternion.Euler(0,180,0);
            }
            if(selfEnemy.position.x < selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x )
            {
                check = true;
            }
            if(selfEnemy.position.x > selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpDownPoint.position.x )
            {
                check = false;
            }
        }
        else
        {
            if(null != player.GetComponent<MyPlatform>().standingOnPlatform && null != selfEnemy.GetComponent<MyPlatform>().standingOnPlatform)
            {
                if(player.GetComponent<MyPlatform>().standingOnPlatform.gameObject.name == selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.gameObject.name)
                {
                        if(player.position.x < selfEnemy.position.x)
                        {
                            // move left
                             Vector3 newPosition = selfEnemy.position; 
                            newPosition.x = newPosition.x - (enemySpeedFactor); 
                            selfEnemy.position = newPosition; 
                            Debug.Log("need to move left");
                            transform.localRotation= Quaternion.Euler(0,0,0);
                        }
                        else
                        {
                            // move right
                            Vector3 newPosition = selfEnemy.position; 
                            newPosition.x = newPosition.x + (enemySpeedFactor); 
                            selfEnemy.position = newPosition; 
                            Debug.Log("need to move right");
                            transform.localRotation= Quaternion.Euler(0,180,0);
                        }
                    }
                    else if(player.GetComponent<MyPlatform>().standingOnPlatform.position.y < selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.position.y)
                    {
                        // jump down
                        Debug.Log("need to jump down");
                        enemyCollider.enabled = false;
                        StartCoroutine(waiter());
                        Debug.Log("need to jump down");
                    }
                    else
                    {
                        Debug.Log("standing on: " + selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.gameObject.name);

                        if(null!=selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>() &&
                        selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x < 
                        selfEnemy.position.x)
                        {
                            // jump up
                            Debug.Log("need to jump up, after moving left");
                            if(Mathf.Abs(rigid.velocity.y) == 0f  )
                              { 
                                    rigid.AddForce(Vector2.up * jumpFactor,ForceMode2D.Impulse);
                                }
                        }
                        else if(null!=selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>() &&
                        selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.GetComponent<MyJumpingPoints>().jumpUpPoint.position.x > 
                        selfEnemy.position.x)
                        {
                            // jump up
                            Debug.Log("need to jump up, after moving right");
                            if(Mathf.Abs(rigid.velocity.y) == 0f  )
                              { 
                                    rigid.AddForce(Vector2.up * jumpFactor,ForceMode2D.Impulse);
                                }
                        }
                    }
                }
                else
                {
                    if(player.GetComponent<MyPlatform>().standingOnPlatform.position.y == selfEnemy.GetComponent<MyPlatform>().standingOnPlatform.position.y)
                    {
                        if(player.position.x < selfEnemy.position.x)
                        {
                            // move left to fall down
                            Vector3 newPosition = selfEnemy.position; 
                            newPosition.x = newPosition.x - (enemySpeedFactor); 
                            selfEnemy.position = newPosition;
                            Debug.Log("need to move left, to fall down");
                            transform.localRotation= Quaternion.Euler(0,0,0);
                        }
                        else
                        {
                            // move right to fall down
                            Vector3 newPosition = selfEnemy.position; 
                            newPosition.x = newPosition.x + (enemySpeedFactor); 
                            selfEnemy.position = newPosition;
                            Debug.Log("need to move right, to fall down");
                            transform.localRotation= Quaternion.Euler(0,180,0);
                        }
                    }
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        touching=true;
        Debug.Log("NO JUMPPPPP");
        if(other.gameObject.tag == "platform")
        {
           anime.SetBool("Nojump",true);
           anime.SetBool("jump",false);
        }
        if(other.gameObject.CompareTag("snowball"))
        {
            snowballcounter--;
            anime.SetInteger("health",snowballcounter);
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        touching =false;
        Debug.Log("JUMPPPPP");
        if(other.gameObject.tag == "platform")
        {
            anime.SetBool("Nojump",false);
            anime.SetBool("jump",true);
        }
    }
    IEnumerator EnemySpawner()
    {
        enemyCollider.enabled = false;
        yield return new WaitForSeconds(1.2f);
        enemyCollider.enabled = true;
    }
    IEnumerator waiter()
    {
        enemyCollider.enabled = false;
        yield return new WaitForSeconds(0.4f);
        enemyCollider.enabled = true;
    }
    
}
