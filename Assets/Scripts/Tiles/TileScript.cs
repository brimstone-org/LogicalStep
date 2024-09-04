using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileScript : MonoBehaviour {
    
    [SerializeField]
    private int health = 1;
    [SerializeField]
    private Text healthText;
    Animator animator;
    AudioSource tileSound;
    AudioClip tileClip;
    bool soundPlayed=false;

    private Text text;

    private void Start()
	{
        healthText.text = health.ToString();
        animator = GetComponent<Animator>();
        
        tileSound = GetComponent<AudioSource>();
        tileClip = tileSound.clip;
	}

	private void Update()
	{
        if(health==0)
        {
            if(soundPlayed == false) 
            {
                SoundManager.Instance.playTileSound();
                soundPlayed = true;
            }
            //tileSound.PlayOneShot(tileClip);
            animator.Play("Destroy");
            Destroy(gameObject, 0.5f);
            
        }
	}

	private void DecreaseHealth(){
        health--;
        healthText.text = health.ToString();

    }

    public int GetHealth() { return health; }
    public void SetHealth(int health){
        this.health = health;
        healthText.text = health.ToString();
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if(collision.tag=="Player" && health !=0){
            GameManager.totalTilesHealth--;
           // Debug.Log(GameManager.totalTilesHealth);
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && health!=0)
        {
            DecreaseHealth();
        }
	}
}
