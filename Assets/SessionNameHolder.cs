using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SessionNameHolder : MonoBehaviour
{
    public string sessionName;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickCallback);
    }

    private void OnClickCallback()
    {
        RoomConfig.roomNameSelected = sessionName;
        SceneManager.LoadScene(1);
    }
}
