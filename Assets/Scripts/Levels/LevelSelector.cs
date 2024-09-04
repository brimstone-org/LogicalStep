using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour 
{
    [SerializeField]
    GameObject level;
    [SerializeField]
    GameObject easyPack;
    [SerializeField]
    GameObject hardPack;
    [SerializeField]
    GameObject bonusPack;



    //procedurally generate level select buttons
    private void Awake()
	{
        for (int i = 0; i < 100; i++)
        {
            GameObject currentLevel = Instantiate(level, transform);

            int a=1;
            if (easyPack.activeSelf == true)
            {
                a = i + 1;
            }
            else if (hardPack.activeSelf == true)
            {
                a = i + 101;
            }
            else if (bonusPack.activeSelf == true){
                a = i + 201;
            }

            
             if (a>=1 && a<=100) 
             {
                currentLevel.GetComponent<PlayBttn>().packID=1;
            }
             else if (a>100 && a<=200) 
             {
                currentLevel.GetComponent<PlayBttn>().packID = 2;
            }
             else if (a>200 && a<=300) 
            {
                currentLevel.GetComponent<PlayBttn>().packID = 3;
            }
            currentLevel.name = (a).ToString();
            currentLevel.gameObject.transform.GetChild(0).GetComponent<Text>().text = (a).ToString();
            currentLevel.GetComponent<Button>().onClick.AddListener(delegate { currentLevel.GetComponent<PlayBttn>().LoadLevelOnButtonPressed(a); });
        }
	}
}
