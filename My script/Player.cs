using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Random Movement Area")]
    public float moveSpeed = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;
    private Vector3 targetPosition;

    [Header("Setting")]

    public bool StartGame = false;
    public GameObject Sword;
    public int MaxHitpoint = 10;
    public int Hitpoint = 10;
    public int SkillPoint = 0;
    public int Score = 0;
    public int LV = 1;
    public int CurrentLV = 0;
    public int NextLVUP = 20;
    public SpriteRenderer sprite;
    public SpriteRenderer ShieldSprite;
    public bool EnableShield = false;
    public bool IsShield = false;
    bool CDShield = false;
    public bool IsInvincible = false;
    
    public bool ActiveUltimate = false;

    Transform _transform;
    public Animator PlayerAnim;

    [Header("UI Setting")]
    public Slider HPBar;
    public Text HPText;
    public Slider SkillBar;
    public Text MPText;
    public Text ScoreText;
    public Text LevelText;
    public GameObject UpgradePanal;
    public GameObject Deadtext;
    public GameObject PlayerSprite;
    public GameObject SwordSprite;
    public GameObject StartText;
    public GameObject TitleText;

    private Vector3 GetRandomPosition()
    {
        // Generate a random position within the defined range
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Vector3 Rd = new Vector3(randomX, randomY, 0f);

        if (this.transform.position.x > Rd.x)
        {
            PlayerAnim.SetInteger("Side", 1);
        }
        if (this.transform.position.x < Rd.x)
        {
            PlayerAnim.SetInteger("Side", 0);
        }

        return Rd;
    }
    private IEnumerator InvincibleDelay()
    {        
        sprite.color = new Color(164, 164, 164);
        IsInvincible = true;
        yield return new WaitForSeconds(3f);
        IsInvincible = false;
        sprite.color = new Color(225, 225, 225);
    }

    private IEnumerator ShieldCooldown()
    {
        IsShield = false;
        CDShield = true;
        yield return new WaitForSeconds(10f);
        CDShield = false;
    }

    public void TakeDMG()
    {
        if(!IsInvincible)
        {
            if (IsShield)
            {                
                StartCoroutine(ShieldCooldown());
            }
            else
            {
                Hitpoint -= 1;
                StartCoroutine(InvincibleDelay());
            }          
        }        

      
    }

    private void Start()
    {
        // Set the initial random target position
        targetPosition = GetRandomPosition();
        _transform = this.transform;

    }

    private void Update()
    {
        //Set UI value
        HPBar.value = Hitpoint;
        HPBar.maxValue = MaxHitpoint;
        HPText.text = Hitpoint + " / " + MaxHitpoint;
        SkillBar.value = SkillPoint;
        MPText.text = SkillPoint + " / 10";
        LevelText.text = "Level: " + LV;
        ScoreText.text = "Score: " + Score;

        // Get the forward direction of the object
        Vector3 forwardDirection = _transform.forward;

        // Check if it's facing left or right
        /*if (forwardDirection.x < 0)//left
        {
            PlayerAnim.SetInteger("Side", 1);
        }
        else if (forwardDirection.x > 0)//right
        {           
            PlayerAnim.SetInteger("Side", 0);
        }*/
             

        if (StartGame == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var clones = GameObject.FindGameObjectsWithTag("Spawner");
                foreach (var clone in clones)
                {
                    clone.GetComponent<SpawnEnemy>().RESpawn = true;
                }
                StartGame = true;
                StartText.SetActive(false);
                TitleText.SetActive(false);
            }
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the target position is reached
        if(Hitpoint > 0)
        {
            if (transform.position == targetPosition)
            {
                // Set a new random target position
                targetPosition = GetRandomPosition();
            }
        }       
        
        if (SkillPoint == 10)
        {
            ActiveUltimate = true; //Set false SwordController
        }

        if(CurrentLV >= NextLVUP)
        {
            LV += 1;
            NextLVUP *= 2;
            CurrentLV = 0;
            UpgradePanal.GetComponent<LevelUPList>().ShowInterface();
        }

        if(EnableShield && !IsShield)
        {
           if(!CDShield) IsShield = true;
        }

        if (IsShield) ShieldSprite.enabled = true;
        else if(!IsShield) ShieldSprite.enabled = false;

        if(Hitpoint <= 0)
        {
            Hitpoint = 0;
            Deadtext.SetActive(true);
            PlayerSprite.SetActive(false);
            SwordSprite.SetActive(false);
            SwordSprite.GetComponent<SwordController>().EnableProjectile = false;
            EnableShield = false;
            ShieldSprite.enabled = false;

            if(Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }              
    }

    
}
