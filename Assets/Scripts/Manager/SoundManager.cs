using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public bool muted;

    Toggle toggle;

    private void Awake()
    {
        if (toggle == null)
        {
            toggle = FindObjectOfType<Toggle>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void ToggleMusic(bool newValue)
    {
        muted = newValue;

        if (muted)
        {
            toggle.isOn = true;
            AudioListener.volume = 0;
        }
        else
        {
            toggle.isOn = false;
            AudioListener.volume = 1;
        }
    }

}
