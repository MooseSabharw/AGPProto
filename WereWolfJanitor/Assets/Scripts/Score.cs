using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    [SerializeField] int multiply;
    [SerializeField] int divide;
    [SerializeField] int pointThreshold;
    [SerializeField] int bonusMultiplier;
    private int moneyAmount;
    private int moppedCount;
    private int trashCount;
    private int bonus = 0;
    public Text scoreText;
    public Text moneyText;
    public Text trashcountT;
    public Text moppedcountT;
    public Text bonusText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GameManager");
        score = gm.GetComponent<GameManager>().IncreaseScore(0);
        moppedCount = gm.GetComponent<GameManager>().GetMoppedCount();
        trashCount = gm.GetComponent<GameManager>().GetTrashCount();

        if (score<0)
        {
            moneyAmount = Mathf.Abs(score) * divide;
        }
        else
        {
            moneyAmount = score * multiply / divide;
        }
        
        if (pointThreshold < (moppedCount + trashCount))
        {
            int excess = moppedCount + trashCount - pointThreshold;
            bonus = excess * bonusMultiplier;
        }

        scoreText.text = string.Format("SCORE:          " + score);
        moneyText.text = string.Format("MONEY GAINED:   $" + moneyAmount);
        trashcountT.text = string.Format("TRASH COLLECTED:          " + trashCount);
        moppedcountT.text = string.Format("MOPPED TILES:          " + moppedCount);
        bonusText.text = string.Format("BONUS:          $" + bonus);
    }

}
