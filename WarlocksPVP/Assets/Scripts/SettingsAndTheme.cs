using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsAndTheme : MonoBehaviour
{
    [SerializeField] private AudioSource _themeSource;
    [SerializeField] private Button _toMenuButton;
    [SerializeField] private Button _toggleMusicButton;

    private static SettingsAndTheme _instance;
    public static SettingsAndTheme Instance { get { return _instance; } }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _toMenuButton.onClick.AddListener(() =>
        {
            SceneTransitions.LoadSceneLocally(SceneTransitions.Scene.MenuScene);
            //disconnect from lobby/game
        });
        _toggleMusicButton.onClick.AddListener(() =>
        {
            if (_themeSource.isPlaying)
                _themeSource.Stop();
            else
                _themeSource.Play();
        });
        _themeSource.Play();
    }
}
