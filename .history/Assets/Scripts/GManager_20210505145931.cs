using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    private AudioSource audioSource = null;
    public Text scoreText; // スコアText
    private float score; // スコア
    int seconds;

    public GameObject[] PieceBase;
    public Sprite[] PieceFaces;
    private List<GameObject> PieceList = new List<GameObject>();//使うか未定


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Dealing()//タイル初期配置
    {
        Debug.Log("Dealing");
        float offsetX = 1.0f;
        float offsetZ = 1.0f;
        int number = 1;
        for (int i =0; i<6; i++)
        {
            for( int j= 0; j<6; j++)
            {
                number = Random.Range(0,7);
                //Vector3 position = new Vector3(j*offsetX,0.1f,i*offsetZ);
                if(i == 0 && j == 0)
                {
                    Instantiate (PieceBase[2], new Vector3((j*offsetX)-2.5f,0.1f,(i*offsetZ)-2.5f),Quaternion.identity);
                }

                else if(i == 0 && j == 2)
                {
                    Instantiate (PieceBase[1], new Vector3((j*offsetX)-2.5f,0.1f,(i*offsetZ)-2.5f),Quaternion.identity);
                }
                elseif(i == 0 && j == 5)
                {
                    Instantiate (PieceBase[3], new Vector3((j*offsetX)-2.5f,0.1f,(i*offsetZ)-2.5f),Quaternion.identity);
                }
                else if(i == 3 && j == 2)
                {/*empty*/}
                else if(i==5 && j==5)
                {
                    Debug.Log("i5j5");

                    Instantiate (PieceBase[5], new Vector3((j*offsetX)-2.5f,0.1f,(i*offsetZ)-2.5f),Quaternion.identity);
                }
                else
                {
                    Instantiate (PieceBase[number], new Vector3((j*offsetX)-2.5f,0.1f,(i*offsetZ)-2.5f),Quaternion.identity);
                }

            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = 0.0f;
        Dealing();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        score += Time.deltaTime;
        seconds = (int)score-3;
        int score2 =900;
        string scoreText2 = "Score:"+score2.ToString("D4") + "0";
        if(seconds>0)
        {
            scoreText.text = scoreText2;
        }

    }

    /// <summary>
    /// SEを鳴らす
    /// </summary>
    public void PlaySE(AudioClip clip)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.Log("オーディオソースが設定されていません");
        }
    }
}
