using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Bullet prefab")]
    [SerializeField]
    private GameObject bulletPrefab;

    [Space]

    [Header("Weapon settings")]
    [SerializeField]
    private float bulletSpeed;
    [Range(0,500)]
    [SerializeField]
    private int damage;
    [Range(1,100)]    
    [SerializeField]
    private int magazine;

    [Space]

    [Header("Weapon canon")]
    [SerializeField]
    private Transform initialweaponCanon;
    
    private Rigidbody bulletClone;
    private Rigidbody[] bullets;
    private int actualBullet = 0;

    [Space]

    [Header("VFX")]
    [SerializeField]
    private ParticleSystem muzzle;
    [SerializeField]
    private ParticleSystem muzzleBeam;

    [Space]

    [Header("Audio")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fireSound;

    private void Awake()
    {
        muzzle.gameObject.SetActive(false);
        muzzleBeam.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreatePool();     
    }

    private void CreatePool()
    {
        //Set up the space in the array
        bullets = new Rigidbody[magazine];
        actualBullet = magazine - 1;

        //Create the bullets pool
        for (int i = 0; i < bullets.Length; i++)
        {
            bulletClone = Instantiate(bulletPrefab.GetComponent<Rigidbody>(), Vector3.zero, Quaternion.identity);
            bullets[i] = bulletClone;
            bullets[i].gameObject.SetActive(false);
        }
    }

    public void ShootWeapon()
    {
        //Set damage to bullet
        bullets[actualBullet].gameObject.GetComponent<Bullet>().SetDamage(damage);


        if (actualBullet > 0) // if the magazine have bullets
        {
            //Activate the bullet
            if (!bullets[actualBullet].gameObject.activeInHierarchy)
            {
                bullets[actualBullet].gameObject.SetActive(true);
            }

            //Slow down the bullet and reposition  of the bullet         
            bullets[actualBullet].velocity = Vector3.zero;
            bullets[actualBullet].gameObject.transform.position = initialweaponCanon.position;

            //Shoot the proyectile
            bullets[actualBullet].AddForce(initialweaponCanon.TransformDirection(Vector3.forward) * bulletSpeed, ForceMode.Impulse);

            //Update the magazine
            actualBullet -= 1;
        }
        else
        {
            Reload(); // Reload the weapon
        }

        //Play sound
        audioSource.PlayOneShot(fireSound);

        //VFX
        if(!muzzle.gameObject.activeInHierarchy)
        {
            muzzle.gameObject.SetActive(true);
            muzzle.Play();
        }

        if (!muzzleBeam.gameObject.activeInHierarchy)
        {
            muzzleBeam.gameObject.SetActive(true);
            muzzleBeam.Play();
        }      
    }

    private void Reload()
    {
        actualBullet = magazine - 1; 
    }
}
