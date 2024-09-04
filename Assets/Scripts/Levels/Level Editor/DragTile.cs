using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragTile : MonoBehaviour {
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    private GameObject player;



    

    private Vector3 screenPoint;
    private Vector3 offset;

    private static bool mouseUpCheck = false;
    PlayerController playerController;


	
	void OnMouseDown()
    {
        mouseUpCheck = false;


        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;

    }

	private void OnMouseUp()
	{
        mouseUpCheck = true;

	}
	

	private void OnTriggerStay2D(Collider2D collision)
	{
        if (mouseUpCheck == true)
        {
            if (collision.tag == "EditorTile")
            {
                
                Instantiate(tile, collision.transform);
                
                EmptyTileEditorScript editorTile = collision.GetComponent<EmptyTileEditorScript>();

                if (gameObject.tag == "StartTile")
                {
                    LevelEditor.instance.AddTileData(editorTile.posX, editorTile.posY, editorTile.health, TileType.StartTile);
                }
                else if (gameObject.tag == "EndTile")
                {
                    LevelEditor.instance.AddTileData(editorTile.posX, editorTile.posY, editorTile.health, TileType.EndTile);
                }
                if (player)
                {
                    player = Instantiate(player, collision.transform.position + player.transform.position, Quaternion.identity);
                    playerController = player.GetComponent<PlayerController>();
                    EmptyTileEditorScript emptyTileEditorScript = collision.GetComponent<EmptyTileEditorScript>();
                    playerController.PosX = emptyTileEditorScript.posX;
                    playerController.PosY = emptyTileEditorScript.posY;
                }
                Destroy(gameObject);
                
            }
        }
	}

}
