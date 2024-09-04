using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmptyTileEditorScript : MonoBehaviour {

    [SerializeField]
    GameObject canvas;
    [SerializeField]
    Text healthText;

    [HideInInspector]
    public int posX;
    [HideInInspector]
    public int posY;
    [HideInInspector]
    public int health = 0;

    private Collider2D col;
    private int initialChildCount;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
    }
    // Use this for initialization
    void Start()
    {
        initialChildCount = transform.childCount;
    }
    private void Update()
    {
        if (transform.childCount > initialChildCount)
        {
            col.enabled = false;
            canvas.SetActive(false);
        }
        else
        {
            col.enabled = true;
        }

    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag != "StartTile")
        {
            if (collision.tag == "Player" && collision.tag != "StartTile")
            {

                health++;
                if (health == 1)
                {
                    bool verif = true;
                    foreach(TileData tile in LevelEditor.instance.tileDatas)
                    {
                        if(tile.gridPositionX==posX && tile.gridPositionY==posY){
                            verif = false;
                        }
                    }
                    if (verif == true)
                    {
                        LevelEditor.instance.AddTileData(posX, posY, health, TileType.NormalTile);
                    }
                }
                else if (health > 1)
                {
                    LevelEditor.instance.EditTileDataHP(posX, posY, health);
                }
                if (healthText != null)
                {
                    healthText.text = health.ToString();
                }
            }
            else
            {

            }
        }
	}
}
