using UnityEngine;
using UnityEngine.UI;

public class BCModeScript : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
            if (toggle == null)
            {
                Debug.LogError("toggle not found");
                return;
            }
        }

        bool savedState = PlayerPrefs.GetInt("BCMode", 0) == 1;

        toggle.isOn = savedState;

        toggle.onValueChanged.AddListener(OnToggleValueChanged);

        Debug.Log("Initial BC Mode: " + savedState);
    }

    public void OnToggleValueChanged(bool isOn)
    {
        PlayerPrefs.SetInt("BCMode", isOn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("BC Mode Changed To: " + isOn);
    }
}
