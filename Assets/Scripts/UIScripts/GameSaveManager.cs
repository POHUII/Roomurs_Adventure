using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;   //turn imformations to a binary file

public class GameSaveManager : MonoBehaviour
{
    public Inventory myInventory;
    public GameObject player;
    public int sceneIndex;
    public static bool loadingPlayer;

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("$Player");
        }
    }

    public void SaveGame()
    {
        Debug.Log(Application.persistentDataPath);

        if (!Directory.Exists(Application.persistentDataPath + "/game_SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_SaveData");
        }

        BinaryFormatter saveFormatter = new BinaryFormatter();


        /*   use the FileStream to create a new file of game's information under directory. */
        FileStream file = File.Create(Application.persistentDataPath + "/game_SaveData/game_save.txt");

        Save save = new Save();
        CreateSaveGameObject(save);

        var json = JsonUtility.ToJson(save);

        saveFormatter.Serialize(file, json);

        file.Close();
    }

    public void LoadGame()
    {
        BinaryFormatter loadFormatter = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/game_SaveData/game_save.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_SaveData/game_save.txt", FileMode.Open);

            /*   Deserialize the json data in file and overwrite on the save. */
            Save save = new Save();
            JsonUtility.FromJsonOverwrite((string)loadFormatter.Deserialize(file), save);

            /* set the corresponding elements */
            LoadInfoToObject(save);

            file.Close();

        }

    }

    public void CreateSaveGameObject(Save saveObj)
    {
        saveObj.playerPositionX = player.transform.position.x;
        saveObj.playerPositionY = player.transform.position.y;
        saveObj.playerBowMode = PlayerController.bowMode;
        saveObj.playerBowModeRelease = PlayerController.bowModeRelease;
        saveObj.playerSwordModeRelease = PlayerController.swordModeRelease;
        saveObj.playerBagState = PlayerController._instance.isBagCanUse;
        saveObj.playerArrowPanelState = PlayerController._instance.panelArrowNumber.activeInHierarchy;

        saveObj.playerMaxHP = PlayerController.maxHP;
        saveObj.playerCurrentHP = PlayerController.currentHP;

        saveObj.playerArrowNum = PlayerAttack._instance.arrowNum;
        saveObj.playerValBuffer = PlayerAttack._instance.valBuffer;
        saveObj.playerCurValBuffer = PlayerAttack._instance.curValBuffer;

        saveObj.currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        saveObj.inventory = myInventory;
    }

    public void LoadInfoToObject(Save loadObj)
    {

        sceneIndex = loadObj.currentSceneIndex;
        SceneManager.LoadScene(sceneIndex);

        myInventory = loadObj.inventory;

        loadingPlayer = true;

        /* Load the Player's position */
        PlayerController._instance.transform.position = new Vector3(loadObj.playerPositionX, loadObj.playerPositionY, 0);

        /* Load the Player's Attack Modes */
        PlayerController.bowMode = loadObj.playerBowMode;
        PlayerController.bowModeRelease = loadObj.playerBowModeRelease;
        PlayerController.swordModeRelease = loadObj.playerSwordModeRelease;

        /* Load the Player's value which is related to UI */
        PlayerController.currentHP = loadObj.playerCurrentHP;
        PlayerController.maxHP = loadObj.playerMaxHP;
        PlayerController._instance.isBagCanUse = loadObj.playerBagState;
        PlayerController._instance.showArrowPanel = loadObj.playerArrowPanelState;

        PlayerAttack._instance.arrowNum = loadObj.playerArrowNum;
        PlayerAttack._instance.valBuffer = loadObj.playerValBuffer;
        PlayerAttack._instance.curValBuffer = loadObj.playerCurValBuffer;
    }
}
