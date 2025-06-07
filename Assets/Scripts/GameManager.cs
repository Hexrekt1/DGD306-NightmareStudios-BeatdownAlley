using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Tooltip("Total shared lives for all players")]
    public int sharedLives = 3;

    
    public ContinueUIManager continueUIManager;

    public List<Player> players = new List<Player>();

    private void Awake() 
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void RegisterPlayer(Player player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
        }
    }

    public void UnregisterPlayer(Player player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);
        }
    }

    
    public void NotifyAllUIManagersLivesChanged()
    {
        foreach (var player in players)
        {
            if (player != null && player.uiManager != null)
            {
                player.uiManager.UpdateLives(); 
            }
        }

        CheckGameOver();
    }

    private void CheckGameOver()
    {
        // Check if all players are dead (isDead == true)
        bool allDead = true;

        foreach (var player in players)
        {
            if (!player.isDead)
            {
                allDead = false;
                break;
            }
        }

        if (allDead)
        {
            // No lives left, show continue UI panel
            if (sharedLives <= 0)
            {
                if (continueUIManager != null)
                {
                    continueUIManager.ShowContinuePanel();
                }
            }
            else
            {
                // If still have lives, respawn players
                RespawnAllPlayers();
            }
        }
    }

    public void RespawnAllPlayers()
    {
        foreach (var player in players)
        {
            if (player.isDead)
            {
                player.PlayerRespawn();
            }
        }
    }
}
