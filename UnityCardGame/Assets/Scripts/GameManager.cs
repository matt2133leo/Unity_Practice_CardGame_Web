using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region KID
    [Header("卡片陣列")]
    public GameObject[] cards;
    [Header("發牌按鈕")]
    public Button btnGetCard;
    
    private int player, pc;     // 玩家、電腦卡片編號

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 玩家取得卡片
    /// </summary>
    public void PlayerGetCard()
    {
        btnGetCard.interactable = false;
        player = GetCard(new Vector3(0, -3, 0));

        Invoke("PcGetCard", 1.5f);
        Invoke("GameWinner", 2.5f);
    }

    /// <summary>
    /// 電腦取得卡片
    /// </summary>
    private void PcGetCard()
    {
        pc = GetCard(new Vector3(0, 3, 0));
    }

    /// <summary>
    /// 取得卡片
    /// </summary>
    /// <param name="pos">卡片座標</param>
    /// <returns>取得的卡片編號</returns>
    private int GetCard(Vector3 pos)
    {
        aud.PlayOneShot(soundGetCard);

        int r = Random.Range(0, cards.Length);

        Instantiate(cards[r], pos, Quaternion.Euler(0, 180, 0));

        return r + 1;
    }
    #endregion

    #region 練習區域
    [Header("音效區域")]
    public AudioClip soundGetCard;  // 發牌
    public AudioClip soundWin;      // 獲勝
    public AudioClip soundLose;     // 失敗
    public AudioClip soundTie;      // 平手

    private AudioSource aud;        // 音效來源：喇叭

    [Header("結束畫面Canvas")]
    public GameObject gameoverscene; //結束畫面

    [Header("比賽結果字樣")]
    public Text Finalresault;        //勝利結果

    /// <summary>
    /// 勝負顯示：使用玩家與電腦取得卡片判斷獲勝、平手或失敗
    /// 玩家卡片編號：player
    /// 電腦卡片編號：pc
    /// 顯示結算畫面
    /// </summary>
    private void GameWinner()
    {
        #region 玩家與電腦判定區
        if(player > pc)                     //玩家卡面大於電腦
        {
            aud.PlayOneShot(soundWin);              //播放勝利音效
            gameoverscene.SetActive(true);          //打開結束畫面
            Finalresault.text = "你贏了！！！";     //結果調整成你贏了!
        }
        else if (player < pc)               //玩家卡面小於電腦
        {
            aud.PlayOneShot(soundLose);             //播放失敗音效
            gameoverscene.SetActive(true);          //打開結束畫面
            Finalresault.text = "弱～";             //結果調整成弱～
        }
        else if (player == pc)             //玩家卡面等於電腦
        {
            aud.PlayOneShot(soundTie);              //播放平手音效
            gameoverscene.SetActive(true);          //打開結束畫面
            Finalresault.text = "沒輸沒贏。";       //結果調整成沒輸沒贏
        }
        #endregion
    }

    /// <summary>
    /// 重啟遊戲方法
    /// </summary>
    public void ReGame()              
    {
        SceneManager.LoadScene(0);  //讀取遊戲場景
    }
    #endregion
}
