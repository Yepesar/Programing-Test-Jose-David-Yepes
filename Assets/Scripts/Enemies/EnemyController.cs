using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 1)]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private int life = 100;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject player;
    [Range(0,100)]
    [SerializeField]
    private float staticShootingRange = 1;
    [Range(0, 100)]
    [SerializeField]
    private float staticMovementRange = 5;

    [Space]
    [Header("UI")]
    [SerializeField]
    private Image healthBar;

    [Space]

    [Header("Weapons & equipment")]
    [SerializeField]
    private Weapon weapon;

    [Space]

    [Header("Sounds")]
    [SerializeField]
    private AudioSource audioS;
    [SerializeField]
    private AudioClip stepSound;
    [Range(0, 1)]
    [SerializeField]
    private float stepSoundVolume = 1;

    private bool shooting = false;
    private bool chasing = false;
    private float distance = 0;
    
    // Update is called once per frame
    void Update()
    {
        AnimationController();
        RotationFix();
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (player.activeInHierarchy)
        {
            //Chase player
            if (distance > staticMovementRange && distance > staticShootingRange)
            {
                chasing = true;
                shooting = false;
            }

            else if (distance < staticShootingRange) // still and shoot
            {
                chasing = false;
                shooting = true;
            }       
        }
        else
        {
            chasing = false;
            shooting = false;
        }

        if (chasing)
        {
            Movement();
        }
        else
        {
            agent.speed = 0;
            agent.destination = transform.position;
        }
    }

    private void Movement()
    {
        agent.speed = moveSpeed;
        agent.destination = player.transform.position;
    }

    private void RotationFix()
    {
        transform.LookAt(player.transform.position);
    }

    public void Shoot()
    {
        weapon.ShootWeapon();
    }

    public void Stept()
    {
        audioS.volume = stepSoundVolume;
        audioS.PlayOneShot(stepSound);
    }

    private void AnimationController()
    {
        if (!shooting && chasing) // Simple animations
        {
            //Run animation
            anim.SetInteger("SimpleState", 1);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
        }
        else if (!chasing && shooting)
        {
            //Stand and shoot
            anim.SetInteger("SimpleState", 2);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
        }
        else if(!shooting && !chasing)
        {
            //Idle
            anim.SetInteger("SimpleState", 0);
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 0);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            int dmg = collision.gameObject.GetComponent<Bullet>().GetDmage();
            ReciveDamage(dmg);
            Debug.Log("Auch");
        }
    }

    private void ReciveDamage(int _damage)
    {
        life -= _damage;
        UpdateLifeUI(life);

        if (life <= 0)
        {
            //Die
            gameObject.SetActive(false);
        }
    }

    private void UpdateLifeUI(float actualLife)
    {
        actualLife /= 100;
        healthBar.transform.localScale = new Vector3(actualLife, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }
}
