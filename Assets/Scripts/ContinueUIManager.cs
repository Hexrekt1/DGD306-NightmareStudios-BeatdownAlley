using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class ContinueUIManager : MonoBehaviour
{
    public GameObject continuePanel;
    public TMP_Text countdownText;
    public Image insertCoinImage;

    private Coroutine countdownCoroutine;
    private int countdownTime = 10;

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
            countdownText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        countdownText.text = "0";

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
