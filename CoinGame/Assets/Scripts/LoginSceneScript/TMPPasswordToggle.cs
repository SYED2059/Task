using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMPPasswordToggle : MonoBehaviour
{
    [Header("TMP_InputField")]
    [SerializeField] private TMP_InputField passwordField;

    [Header("Button")]
    [SerializeField] private Button toggleButton;

    [Header("Sprite")]
    [SerializeField] private Sprite eyeOpen;

    [SerializeField] private Sprite eyeClose;

    [Header("Image")]
    [SerializeField] private Image toggleImage;

    [Header("AudioManager")]
    public AudioManager AudioManager;

    bool isPasswordHidden = true;

    void Awake()
    {
        toggleImage = toggleButton.gameObject.GetComponent<Image>();
    }

    void OnEnable()
    {
        toggleButton.onClick.AddListener(TogglePasswordVisibility);
    }

    void Start()
    {
        SetPasswordMode(true);
    }

    void TogglePasswordVisibility()
    {
        AudioManager.PlayButtonClickSound();
        isPasswordHidden = !isPasswordHidden;
        SetPasswordMode(isPasswordHidden);
    }

    void SetPasswordMode(bool hide)
    {
        passwordField.contentType = hide ? TMP_InputField.ContentType.Password : TMP_InputField.ContentType.Standard;
        passwordField.ForceLabelUpdate();
        toggleImage.sprite = hide ? eyeClose : eyeOpen;
    }

    void OnDisable()
    {
        toggleButton.onClick.RemoveListener(TogglePasswordVisibility);
    }
}
