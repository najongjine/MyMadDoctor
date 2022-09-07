using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    List<EnemyBullet> enemyBullet1Pool = new List<EnemyBullet>();
    List<EnemyBullet> enemyBullet2Pool = new List<EnemyBullet>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetBullet(int bulletType=0)
    {
        if (bulletType==1)
        {
            foreach (EnemyBullet bullet in enemyBullet1Pool)
            {

            }
        }
        else
        {
            foreach (EnemyBullet bullet in enemyBullet2Pool)
            {

            }
        }
    }
}
