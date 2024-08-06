using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossMovement : MonoBehaviour
{
  [SerializeField] GameObject littleEnemy;
  [SerializeField] Vector3 EnemyStartOffset; 
  [SerializeField] Vector2 facing;
  [SerializeField] Transform MyLook;
  [SerializeField] Animator anime;
  private int counter;
  [SerializeField] int BossHealth;
  // Start is called before the first frame update
    void Start()
    {
        counter = 0;
    }
    // Update is called once per frame
    void Update()
    {
        counter++;
        if(counter == 500)
        {
            Instantiate(littleEnemy,MyLook.position+EnemyStartOffset,Quaternion.identity,this.transform);
            counter = 0;
        }    
        if(BossHealth == 0)
        {
            Destroy(this.gameObject,1f);
            anime.SetTrigger("damage");
            SceneManager.LoadSceneAsync(4);
        }       
    }
    private void FixedUpdate()
    {
      
    }
    public Vector2 getFacingDirection()
    {
        return facing;
    }
      private void OnCollisionEnter2D(Collision2D other)
    {
         if(other.gameObject.CompareTag("snowball"))
        {
            BossHealth--;

        }
    }
}
