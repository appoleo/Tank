using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManage : MonoBehaviour
{

    public bool isdead;
    public int lifeValue = 3;
    public int score = 0;
    public GameObject born;
    public bool isDefeat;
    public Text textScore;
    public Text textLife;
    public GameObject gameover;

    private static PlayerManage instance;

    public static PlayerManage Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            gameover.SetActive(true);
            Invoke("ReturnToMainScene", 3);
            return;
        }
        if (isdead)
        {
            Reset();
        }
        textScore.text = score.ToString();
        textLife.text = lifeValue.ToString();
    }

    private void Reset()
    {
        if (lifeValue <= 0)
        {
            // 游戏结束
            isDefeat = true;
            Invoke("ReturnToMainScene", 3);
        }
        else
        {
            GameObject item = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            item.GetComponent<Born>().createPlayer = true;
            isdead = false;
        }
    }

    private void ReturnToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
}
