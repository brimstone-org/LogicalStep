using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeBG : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResizeSpriteToScreen();
    }

    void ResizeSpriteToScreen()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        double x = worldScreenWidth / width;
        double y = worldScreenHeight / height;
        transform.localScale = new Vector3((float)x,(float)y,1);
    }
}
