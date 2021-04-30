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

    void CreatePieces()
    {
        float pieceX = 1.5;
        float pieceY = 0.1;
        float pieceZ = 0.5;
        Vector3 position = new Vector3(pieceX,pieceY,pieceZ);
        Instantiate(PieceBase(1),new Vector3(pieceX,pieceY,pieceZ));
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = 0.0f;
        CreatePieces();
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
