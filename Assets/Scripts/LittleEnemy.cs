using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleEnemy : MonoBehaviour
{
    [SerializeField] Rigidbody2D littleEnemyRgb;
    [SerializeField] float ShootingForce;
    [SerializeField] Vector2 littleEnemyDirection;
    // Start is called before the first frame update
    void Start()
    {
        littleEnemyRgb.AddForce((littleEnemyDirection.normalized)*ShootingForce,ForceMode2D.Impulse);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
   
}
