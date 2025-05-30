using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ContinueUIManager continueUIManager;

    public static GameManager Instance;

    public int sharedLives = 3;

    public List<Player> players = new List<Player>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🔹 Add this to register new players when they spawn
    public void RegisterPlayer(Player player)
    {
        if (!players.Contains(player))
        {
            players.Add(player);
        }
    }

    // 🔹 And this for cleanup if a player dies/disconnects
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

        // Check if shared lives is 0 and all players are dead
        if (sharedLives <= 0 && AllPlayersDead())
        {
            if (continueUIManager != null)
                continueUIManager.ShowContinuePanel();
        }
    }

    private bool AllPlayersDead()
    {
        foreach (var player in players)
        {
            if (!player.isDead)
                return false;
        }
        return true;
    }

    public void EndGame()
    {
        Debug.Log("Game Over. You can load a scene or show game over screen here.");
        // Example: SceneManager.LoadScene("MainMenu");
    }

}
