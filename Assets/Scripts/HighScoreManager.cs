using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//ハイスコア表示関係
public class HighScoreManager : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI  _highscoreText; //ハイスコア表示用テキスト
    private int score; //現在のスコア
    private int highScore; //ハイスコア
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HIGHSCORE",highScore);//ハイスコア定義
        _highscoreText.text = highScore.ToString();
        //全部のキーと値を削除
        //PlayerPrefs.DeleteAll();
    }

    public void SetHighScore(int value)
    {
        PlayerPrefs.SetInt("HIGHSCORE", value);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isOver)
        {   
            score = GameManager.Instance.totalscore;//このタイミングでスコア取得
            
            if (highScore < score)  //ハイスコアを超えた場合に更新
            {
                highScore= score;

                //"HIGHSCORE"をキーとして、ハイスコアを保存
                PlayerPrefs.SetInt("HIGHSCORE", highScore);
                PlayerPrefs.Save();//ディスクへの書き込み
                _highscoreText.text=highScore.ToString();
                
            } 
        }
    }
}
