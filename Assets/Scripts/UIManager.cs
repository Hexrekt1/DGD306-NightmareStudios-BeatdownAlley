using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthUI;
    public Image playerImage;
    public Text playerName;
    public Text livesText;

    public GameObject enemyUI;
    public Slider enemySlider;
    public Text enemyName;
    public Image enemyImage;

    public float enemyUITime = 4f;

    private float enemyTimer;
    private Player player;



 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyTimer += Time.deltaTime;
        if (enemyTimer >= enemyUITime)
        {
            enemyUI.SetActive(false);
            enemyTimer = 0;
        }
     }
    public void UpdateHealth(int amount)
    {
        healthUI.maxValue = player.maxHealth; 
        healthUI.value = amount;
    }
    public void UpdateEnemyUI(int maxHealth, int currentHealth,string name,Sprite image)
    {
        enemySlider.maxValue = maxHealth;
        enemySlider.value = currentHealth;
        enemyName.text = name;
        enemyImage.sprite = image;
        enemyTimer = 0;

        enemyUI.SetActive(true);
    }
    public void UpdateLives()
{
    if (GameManager.Instance != null)
    {
        livesText.text = "x " + GameManager.Instance.sharedLives.ToString();
    }
}


    public void Initialize(Player p)
    {
        player = p;

        healthUI.maxValue = player.maxHealth;
        healthUI.value = player.maxHealth;
        playerName.text = player.playerName;
        playerImage.sprite = player.playerImage;
        UpdateLives();  
    }


}
