using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    List<Bullet> weapon_1_BulletPool=new List<Bullet>()
        , weapon_3_BulletPool = new List<Bullet>()
        , weapon_4_BulletPool = new List<Bullet>();

    [SerializeField]
    int InitialBulletCount = 10;

    [SerializeField]
    Transform bulletHolder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateBulletPool(int weaponType, Bullet bullet)
    {
        GameObject newBullet = null;
        if(weaponType == 0)
        {
            for(int i = 0; i < InitialBulletCount; i++)
            {
                newBullet = Instantiate(bullet.gameObject);
                newBullet.SetActive(false);
                newBullet.transform.SetParent(bulletHolder);
                weapon_1_BulletPool.Add(newBullet.GetComponent<Bullet>());
            }
        }
        if (weaponType == 2)
        {
            for (int i = 0; i < InitialBulletCount; i++)
            {
                newBullet = Instantiate(bullet.gameObject);
                newBullet.SetActive(false);
                newBullet.transform.SetParent(bulletHolder);
                weapon_3_BulletPool.Add(newBullet.GetComponent<Bullet>());
            }
        }
        if (weaponType == 3)
        {
            for (int i = 0; i < InitialBulletCount; i++)
            {
                newBullet = Instantiate(bullet.gameObject);
                newBullet.SetActive(false);
                newBullet.transform.SetParent(bulletHolder);
                weapon_4_BulletPool.Add(newBullet.GetComponent<Bullet>());
            }
        }
    }
    public void AddBulletToPool(int weaponType,Bullet bullet)
    {
        if (weaponType==0)
        {
            weapon_1_BulletPool.Add(bullet);
        }
        if (weaponType == 2)
        {
            weapon_3_BulletPool.Add(bullet);
        }
        if (weaponType == 3)
        {
            weapon_4_BulletPool.Add(bullet);
        }
        bullet.transform.SetParent(bulletHolder);
    }
    public Bullet GetBullet(int weaponType)
    {
        if (weaponType==0)
        {
            for(int i = 0; i < weapon_1_BulletPool.Count; i++)
            {
                if (!weapon_1_BulletPool[i].gameObject.activeInHierarchy)
                {
                    return weapon_1_BulletPool[i];
                }
            }
        }
        if (weaponType == 2)
        {
            for (int i = 0; i < weapon_3_BulletPool.Count; i++)
            {
                if (!weapon_3_BulletPool[i].gameObject.activeInHierarchy)
                {
                    return weapon_3_BulletPool[i];
                }
            }
        }
        if (weaponType == 3)
        {
            for (int i = 0; i < weapon_4_BulletPool.Count; i++)
            {
                if (!weapon_4_BulletPool[i].gameObject.activeInHierarchy)
                {
                    return weapon_4_BulletPool[i];
                }
            }
        }
        return null;
    }
   

}
