using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerProxy : MonoBehaviour
{
    public void SetUsername(string username)
    {
        GameManager.instance.SetPlayerUsername(username);
    }
    public void Play()
    {
        GameManager.instance.Play();
    }
}