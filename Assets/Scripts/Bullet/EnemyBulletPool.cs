using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPool : MonoBehaviour
{
    public static EnemyBulletPool instance;
    [SerializeField]
    List<EnemyBullet> enemyBullet1Pool = new List<EnemyBullet>();
    [SerializeField]
    List<EnemyBullet> enemyBullet2Pool = new List<EnemyBullet>();

    [SerializeField]
    EnemyBullet enemyBullet_1_pref, enemyBullet_2_pref;

    [SerializeField]
    private bool playSound_1;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (playSound_1)
        {
            SoundManager.instance.Enemy_Weapon_1_Shoot();
        }
        else
        {
            SoundManager.instance.Enemy_Weapon_2_Shoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public EnemyBullet GetBullet(EnemyBullet enemyBullet)
    {
        int bulletType = 1;
        if (enemyBullet.gameObject.name.Contains("2"))
        {
            bulletType = 2;
        }
        EnemyBullet bulletObj = null;
        var bCreateNew = true;
        if (bulletType==1)
        {
            foreach (EnemyBullet bullet in enemyBullet1Pool)
            {
                if (!bullet.gameObject.activeInHierarchy)
                {
                    bulletObj=bullet;
                    bulletObj.gameObject.SetActive(true);
                    bCreateNew = false;
                }
            }
        }
        else
        {
            foreach (EnemyBullet bullet in enemyBullet2Pool)
            {
                if (!bullet.gameObject.activeInHierarchy)
                {
                    bulletObj = bullet;
                    bulletObj.gameObject.SetActive(true);
                    bCreateNew = false;
                }
            }
        }
        if (bCreateNew)
        {

            if (bulletType == 1)
            {
                bulletObj = Instantiate(enemyBullet_1_pref);
                enemyBullet1Pool.Add(bulletObj);    
            }
            else
            {
                bulletObj = Instantiate(enemyBullet);
                enemyBullet2Pool.Add(bulletObj);
            }
            bulletObj.transform.SetParent(transform);
        }
        return bulletObj;
    }
}
