using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Range(0,1)]
    [SerializeField]
    private float moveSpeed;
    [Range(0,100)]
    [SerializeField]
    private int life = 100;
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private bool usingKeyboard = false;
    
     
    [Space]

    [Header("Weapons & equipment")]
    [SerializeField]
    private Weapon weapon;

    [Space]
    [Header("UI")]
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private GameObject fireButtonOBJ;

    [Space]

    [Header("Sounds")]
    [SerializeField]
    private AudioSource audioS;
    [SerializeField]
    private AudioClip keySound;
    [Range(0, 1)]
    [SerializeField]
    private float keySoundVolume = 1;
    [SerializeField]
    private AudioClip stepSound;
    [Range(0, 1)]
    [SerializeField]
    private float stepSoundVolume = 1;
    [SerializeField]
    private AudioClip painSound;
    [Range(0, 1)]
    [SerializeField]
    private float painSoundVolume = 1;

    private bool shooting = false;
    private bool hasKey = false;

    public bool HasKey { get { return hasKey; } set { hasKey = value; } }
    public bool Shooting { get { return shooting; } set { shooting = value; } }

    // Start is called before the first frame update
    void Start()
    {
        InputValidation();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AnimationController();
       
        if(Input.GetKeyDown(KeyCode.Space))
        {
            shooting = true;          
        }

        if (Input.GetKey(KeyCode.Space))
        {
            shooting = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            shooting = false;
        }
    }

    private void Move()
    {
        if (!usingKeyboard)
        {
            Vector3 direction = new Vector3(transform.position.x + joystick.Horizontal, transform.position.y, transform.position.z + joystick.Vertical);
            transform.position = Vector3.MoveTowards(transform.position, direction, moveSpeed * Time.deltaTime);
            RotationController(direction);
        }
        else
        {
            Vector3 direction = new Vector3(transform.position.x + Input.GetAxis("Horizontal"), transform.position.y, transform.position.z + Input.GetAxis("Vertical"));
            transform.position = Vector3.MoveTowards(transform.position, direction, moveSpeed * Time.deltaTime);
            RotationController(direction);
        }
    }

    private void RotationController(Vector3 _direction)
    {
        transform.LookAt(_direction);
    }

    private void AnimationController()
    {
        if (!shooting) // Simple animations
        {
            if (joystick.Vertical != 0 || joystick.Horizontal != 0 || Input.GetAxis("Vertical") != 0|| Input.GetAxis("Horizontal") != 0)
            {
                //Run animation
                anim.SetInteger("SimpleState", 1);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(2, 0);
            }
            else
            {
                //Idle animation
                anim.SetInteger("SimpleState", 0);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(2, 0);
            }
        }
        else 
        {
            if (joystick.Vertical != 0 || joystick.Horizontal != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) // Layer animations
            {
                //Run animation ans shoot           
                anim.SetLayerWeight(1, 1);
                anim.SetLayerWeight(2, 1);
            } 
            else
            {
                //Stand and shoot
                anim.SetInteger("SimpleState", 2);
                anim.SetLayerWeight(1, 0);
                anim.SetLayerWeight(2, 0);
            }
        }
        
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

    public void Reset()
    {
        SceneManager.LoadScene("PTest");
    }

    private void InputValidation()
    {
        if(usingKeyboard)
        {
            joystick.gameObject.SetActive(false);
            fireButtonOBJ.SetActive(false);
        }
        else
        {
            joystick.gameObject.SetActive(true);
            fireButtonOBJ.SetActive(true);
        }
    }

    public void SwitchInputs()
    {
        usingKeyboard = !usingKeyboard;
        InputValidation();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            int dmg = collision.gameObject.GetComponent<Bullet>().GetDmage();
            ReciveDamage(dmg);

            audioS.volume = painSoundVolume;
            audioS.PlayOneShot(painSound);          
        }

        if (collision.gameObject.tag == "DoorKey")
        {
            HasKey = true;

            audioS.volume = keySoundVolume;
            audioS.PlayOneShot(keySound);

            collision.gameObject.SetActive(false);
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
