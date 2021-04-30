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

    void Dealing()
    {
        float offsetX = 1.0f;
        float offsetZ = 1.0f;
        int number = 1;
        for (int i =0; i<3; i++)
        {
            for( int j= 0; j<3; j++)
            {
                number = Random.Range(0,6);
                //Vector3 position = new Vector3(j*offsetX,0.1f,i*offsetZ);
                Instantiate (PieceBase[number], new Vector3((j*offsetX)+1.5f,0.1f,i*offsetZ),Quaternion.identity);

                if(number>3)
                {
                    break;
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
    void Update()
    {

        score += Time.deltaTime;
        seconds = (int)score-3;
        if(seconds>0)
        {
            scoreText.text = seconds.ToString()+"km";
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
