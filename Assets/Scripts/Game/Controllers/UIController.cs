using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject gasPanel;
    public GameObject coinPanel;

    public GameObject fade;
    public GameObject pauseWindow;
    public GameObject gameOverWindow;

    public GameObject globalVolume;

    public PlayerController playerController;

    private int gasCapacity = 100;
    private int gasValue = 100;
    private int gasProgressBar = 100;
    private int coinValue = 0;

    private int nextUpdate = 1;

    private RectTransform rectTransform;

    void Start()
    {
        GameObject gasProgress = gasPanel.transform.GetChild(0).gameObject;
        rectTransform = gasProgress.GetComponent<RectTransform>();

        IncrementCoinValue(PlayerPrefs.GetInt("coinValue", 0));

        string graphicsOptions = PlayerPrefs.GetString("graphicsOptions", "HIGH");

        globalVolume.gameObject.active = (graphicsOptions == "HIGH");
    }

    private void SetGasValue(int progress) {
        if (progress < 0)
        {
            progress = 0;
        }
        else if (progress > 100)
        {
            progress = 100;
        }
        gasProgressBar = progress;
    }

    public void IncrementCoinValue(int count)
    {
        GameObject coinsText = coinPanel.transform.GetChild(0).gameObject;

        TextMeshProUGUI textMesh = coinsText.GetComponent<TextMeshProUGUI>();

        coinValue += count;

        PlayerPrefs.SetInt("coinValue", coinValue);

        textMesh.text = "" + coinValue;
    }

    public void IncrementGasValue(int count)
    {
        gasValue += count;

        if(gasValue < 0)
        {
            _pauseGame();
            _showWindow(gameOverWindow);
            return;
        }

        SetGasValue((int) (((float)gasValue / (float)gasCapacity) * 100));
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            IncrementGasValue(-1);
        }

        rectTransform.sizeDelta = new Vector2(15 + (3.35f * (float)gasProgressBar), rectTransform.sizeDelta.y);
    }

    public void onPressPause() {
        _pauseGame();
        _showWindow(pauseWindow);
    }

    public void onPressResume() {
        _resumeGame();
        _hideWindow(pauseWindow);
    }

    public void onPressReborn() {
        playerController.Respawn();
        _resumeGame();
        _hideWindow(gameOverWindow);
    }

    public void onPressNewGame()
    {
        _resumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onPressGoMainMenu()
    {
        _resumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void onDie() {
        _pauseGame();
        _showWindow(gameOverWindow);
    }

    private void _showWindow(GameObject window)
    {
        fade.gameObject.active = true;
        window.gameObject.active = true;
    }

    private void _hideWindow(GameObject window)
    {
        fade.gameObject.active = false;
        window.gameObject.active = false;
    }

    private void _pauseGame() {
        Time.timeScale = 0;
    }

    private void _resumeGame() {
        Time.timeScale = 1;
    }
}
