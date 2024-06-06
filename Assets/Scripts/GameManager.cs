using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isNext { get; set; }
    public int MaxSeedNo { get; private set; } //落ちてくるものの種類

    [SerializeField] private seed[] seedPrefab;
    [SerializeField] private Transform seedPosition; //どこから落とすか
    [SerializeField]  GameObject GameOverUI; //終了時に出てくるUI
    [SerializeField] public TextMeshProUGUI ScoreNumber; //スコア表示テキスト
    [SerializeField] private TextMeshProUGUI NextText; //次に落ちてくるものを指すテキスト

    public int totalscore;
    public bool isOver{get; set;} //ゲームオーバーラインを超えたか
    private int insnum;
    private int nexti; //次に落ちてくるものの番号
    
    int[] score = {1,3,6,10,15,21,28,36,45,55,66}; //くっついた時に得られるスコア
    int[] ypos = {193,163,131,97,63,26,-14,-53,-94,-141,-187}; //落ちてくるもの一覧表示用

    // Start is called before the first frame update
    void Start()
    {   
        Instance = this;
        isNext = false;
        isOver = false;
        MaxSeedNo = seedPrefab.Length;
        totalscore = 0;
        SetScore(totalscore);//スコアを初期化
        nexti =  Random.Range(0,5);//最初に表示されるモノを決定
        CreateSeed();//モノを表示

    }

    // Update is called once per frame
    void Update()
    {
        if (isNext)//次のモノを表示
        {
            isNext = false;
            Invoke("CreateSeed", 1f);
            
        }
        if(Input.GetKeyDown (KeyCode.E))//Eを押したら終了
        {
            isOver=true;
            GameOverUI.SetActive(true);
        }
    }
    public void LoadCurrentScene()//通常シーン
    {   
        isOver=false;
        GameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }
    public void EndGame()//ゲームプレイ終了
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    private void CreateSeed()//次のモノを決める
    {
        seed seedIns = Instantiate(seedPrefab[nexti], seedPosition);
        seedIns.seedNo = nexti;// 今の番号
        seedIns.gameObject.SetActive(true);
        nexti = Random.Range(0,5);//小さいものから順に５つを候補とし、次のものをランダムに決定
        SetNext();
    }
    public void MergeNext(Vector3 target,int seedNo)//同じ大きさがくっついた時にやること
    {
        if (seedNo < MaxSeedNo - 1)//最大でなければ、一つ大きい番号になる
        {
            insnum=seedNo+1;
        }
        else//最大のもの同士がくっつくと最小になる
        {
            insnum=0;
        }
        seed seedIns = Instantiate(seedPrefab[insnum], target, Quaternion.identity, seedPosition);
        totalscore += score[seedNo];//今の番号でスコア計算
        seedIns.seedNo = insnum;//次の番号に更新
        seedIns.isDrop = true;
        seedIns.GetComponent<Rigidbody2D>().simulated = true;
        seedIns.gameObject.SetActive(true);
        
        SetScore(totalscore);//スコア表示
    }
    private void SetScore(int score)//スコア表示
    {
        ScoreNumber.text = score.ToString();
    }
    private void SetNext()//次のものの場所
    {

        Vector2 pos = NextText.GetComponent<RectTransform>().anchoredPosition;
       
        pos.y = ypos[nexti];
        NextText.GetComponent<RectTransform>().anchoredPosition = pos;
       
    }
    /* public void GameOver()
    {
        
        if(num==10)
        {
            GameOverUI.SetActive(true);
           
        }
        IsActive = false;
        /*
        // ハイスコアのセーブ
        if(highScore > PlayerPrefs.GetInt(highScoreKey, 0))
        {
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }*/
}
