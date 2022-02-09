using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Save
{
    /* player's information  */
    public float playerPositionX, playerPositionY;
    public GameObject playerSave;
    public bool playerBowMode;
    public bool playerBowModeRelease;
    public bool playerSwordModeRelease;
    public float playerCurrentHP, playerMaxHP;
    public float playerCurValBuffer, playerValBuffer;
    public int playerArrowNum;
    public bool playerBagState;
    public bool playerArrowPanelState;

    /* the index of current scene */
    public int currentSceneIndex;

    /* the information about inventory */
    public Inventory inventory;
}
