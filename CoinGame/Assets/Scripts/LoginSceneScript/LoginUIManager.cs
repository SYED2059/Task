using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIManager : MonoBehaviour
{
    [Header("TMP_InputField")]
    [SerializeField] private TMP_InputField phoneInput;

    [SerializeField] private TMP_InputField passwordInput;

    [Header("TextMeshProUGUI")]
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Button")]
    [SerializeField] private Button loginButton;

    public SceneLoader SceneLoader;

    public AudioManager AudioManager;

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
        AudioManager.PlayButtonClickSound();
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
        Debug.Log("LoadScene");
        SceneLoader.LoadSceneAsync(SceneName);
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

    void OnDisable()
    {
        loginButton.onClick.RemoveListener(OnLoginClicked);
    }
}
