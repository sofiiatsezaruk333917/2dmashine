using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public GameObject lowGraphicsButton;
    public GameObject highGraphicsButton;

    public void Start()
    {
        _updateButton();
    }

    public void onPressPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void onPressOptions()
    {
        mainMenu.gameObject.active = false;
        optionsMenu.gameObject.active = true;
    }

    public void onPressOptionsBack()
    {
        mainMenu.gameObject.active = true;
        optionsMenu.gameObject.active = false;
    }

    public void onPressLowGraphics() {
        PlayerPrefs.SetString("graphicsOptions", "LOW");
        _updateButton();
    }

    public void onPressHighGraphics() {
        PlayerPrefs.SetString("graphicsOptions", "HIGH");
        _updateButton();
    }

    private void _updateButton() {
        string graphicsOptions = PlayerPrefs.GetString("graphicsOptions", "HIGH");

        if (graphicsOptions == "HIGH")
        {
            highGraphicsButton.GetComponent<Image>().color = new Color(1f, 110f / 255f, 110f / 255f);
            lowGraphicsButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        } else
        {
            lowGraphicsButton.GetComponent<Image>().color = new Color(1f, 110f / 255f, 110f / 255f);
            highGraphicsButton.GetComponent<Image>().color = new Color(1f, 1f, 1f);
        }

    }
}
