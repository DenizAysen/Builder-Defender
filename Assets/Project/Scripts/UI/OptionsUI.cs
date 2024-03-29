using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;
    #endregion

    #region Privates
    private TextMeshProUGUI _soundVolumeText;
    private TextMeshProUGUI _musicVolumeText;

    private Toggle _toggle;
    #endregion
    private void Awake()
    {
        _soundVolumeText = transform.Find("soundVolumeText").GetComponent<TextMeshProUGUI>();
        _musicVolumeText = transform.Find("musicVolumeText").GetComponent<TextMeshProUGUI>();

        _toggle = transform.Find("edgeScrollingToggle").GetComponent<Toggle>();

        transform.Find("soundIncreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            soundManager.IncreaseVolume();
            UpdateText();
        });
        transform.Find("soundDecreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            soundManager.DecreaseVolume();
            UpdateText();
        });
        transform.Find("musicIncreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.IncreaseVolume();
            UpdateText();
        });
        transform.Find("musicDecreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.DecreaseVolume();
            UpdateText();
        });
        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() => {
            Time.timeScale = 1f;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        _toggle.onValueChanged.AddListener((bool set) => {
            CameraHandler.Instance.SetEdgeScrolling(set);
        });
        
    }
    private void Start()
    {
        _toggle.SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrolling());

        UpdateText();
        gameObject.SetActive(false);
    }
    private void UpdateText()
    {
        _soundVolumeText.SetText(Mathf.RoundToInt(soundManager.GetVolume() * 10).ToString());
        _musicVolumeText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 10).ToString());
    }
    public void ToggleVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
