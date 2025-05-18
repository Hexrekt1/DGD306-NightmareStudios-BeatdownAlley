using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{

    public GameObject Fist;
    public float minFistTime, maxFistTime;

    private MusicController music;
    // Start is called before the first frame update
    void Awake()
    {
        Invoke("ThrowFist", Random.Range(minFistTime, maxFistTime));
        music = FindObjectOfType<MusicController>();
        music.PlaySong(music.bossSong);
    }
    void ThrowFist()
    {
        if(!isDead)
        {
            anim.SetTrigger("Fist");
            GameObject tempFist = Instantiate(Fist,transform.position,transform.rotation);
            if(facingRight)
            {
                tempFist.GetComponent<BossFist>().direction = 1;
            }
            else
            {
                tempFist.GetComponent<BossFist>().direction = -1;
            }
            Invoke("ThrowFist", Random.Range(minFistTime, maxFistTime));
        }
    }
    void BossDefeated()
    {
        music.PlaySong(music.levelClearSong);
        Invoke("LoadScene", 8f);
    }
    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
