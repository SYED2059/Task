using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIManager : MonoBehaviour
{

    [SerializeField] private TMP_InputField phoneInput;

    [SerializeField] private TMP_InputField passwordInput;


    [SerializeField] private TextMeshProUGUI statusText;

    [SerializeField] private Button loginButton;

    public SceneLoader SceneLoader;

    void Awake()
    {
        if (PlayerPrefs.HasKey("IsLoggedIn") && PlayerPrefs.GetInt("IsLoggedIn") == 1)
        {
            statusText.text = "Already logged in.";
            LoadMainScene("MainMenuScene");
        }
    }

    void OnEnable()
    {
        loginButton.onClick.AddListener(OnLoginClicked);
    }


    void OnLoginClicked()
    {
        string phone = phoneInput.text;

        if (phone.Length != 10 || !IsAllDigits(phone))
        {
            statusText.text = "Please enter a valid 10-digit phone number.";
            return;

        }
        if (!string.IsNullOrEmpty(phoneInput.text) && !string.IsNullOrEmpty(passwordInput.text))
        {
            PlayerPrefs.SetInt("IsLoggedIn", 1);
            PlayerPrefs.SetString("Phone", phone);
            PlayerPrefs.Save();

            statusText.text = "Login successful!";
            LoadMainScene("MainMenuScene");
        }
        else
        {
            statusText.text = "Fill the information";
        }


    }

    void LoadMainScene(string SceneName)
    {
        //SceneManager.LoadScene("MainScene");
        Debug.Log("LoadScene");
        SceneLoader.LoadSceneAsync(SceneName);
    }

    void OnDisable()
    {

        loginButton.onClick.RemoveListener(OnLoginClicked);

    }

    bool IsAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }
}
