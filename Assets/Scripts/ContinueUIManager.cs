using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ContinueUIManager : MonoBehaviour
{
    public GameObject continuePanel;
    public Image insertCoinImage;

    private Coroutine countdownCoroutine;
    private int countdownTime = 10;

    private void Start()
    {
        continuePanel.SetActive(false);
        if (insertCoinImage != null)
            insertCoinImage.enabled = false;
    }

    public void ShowContinuePanel()
    {
        continuePanel.SetActive(true);

        if (insertCoinImage != null)
            insertCoinImage.enabled = true;

        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);

        countdownCoroutine = StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        int timeLeft = countdownTime;
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(0.5f);
            timeLeft--;
        }

        if (insertCoinImage != null)
            insertCoinImage.enabled = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void HideContinuePanel()
    {
        continuePanel.SetActive(false);
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);
    }
}
