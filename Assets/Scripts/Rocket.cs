using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private float fuel = 100f;

    private int now_y;
    private int score;
    private int highScore;

    private Transform roketTransform;
    
    private readonly float SPEED = 5f;
    private readonly float FUELPERSHOOT = 10f;

    [SerializeField] private TextMeshProUGUI currentScoreTxt;
    [SerializeField] private TextMeshProUGUI HighScoreTxt;
    [SerializeField] private Image fillImage;



    void Awake()
    {
        // TODO : Rigidbody2D 컴포넌트를 가져옴(캐싱) 
        _rb2d = GetComponent<Rigidbody2D>();
        roketTransform = GetComponent<Transform>();
    }
    private void Start()
    {
        score = (int)roketTransform.position.y;
        if (PlayerPrefs.HasKey("PlayerScore"))
        {
            highScore = LoadData();
        }
    }
    private void Update()
    {
        Calculation_y();
        HighScoreTxt.text = $"{highScore} M";
        fillImage.fillAmount = fuel/100;
        if (fillImage.fillAmount<1)
        {
            Debug.Log(fillImage.fillAmount);
            fuel +=0.1f;
        }
    }

    public void Shoot()
    {
        // TODO : fuel이 넉넉하면 윗 방향으로 SPEED만큼의 힘으로 점프, 모자라면 무시
        if(fuel >=10f)
        {
            fuel -= FUELPERSHOOT;
            _rb2d.AddForce(Vector2.up * SPEED,ForceMode2D.Impulse);

        }
    }
    private void Calculation_y()
    {
        
        now_y=(int)roketTransform.position.y;
        if (score<now_y)
        {
            score = now_y;
            currentScoreTxt.text = $"{score} M";
        }
        if (score>highScore)
        {
            highScore=score;
            HighScoreTxt.text = $"{highScore} M";
        }

    }
    public void resetRoket()
    {
        SaveData(highScore);
        SceneManager.LoadScene("RocketLauncher");
    }
    public void SaveData(int score)
    {
        PlayerPrefs.SetInt("PlayerScore", score);
    }

    public int LoadData()
    {
        return PlayerPrefs.GetInt("PlayerScore");
    }
}
public class RocketDashboard : Rocket
{

}
public class RocketEnergySystem : Rocket
{

}
