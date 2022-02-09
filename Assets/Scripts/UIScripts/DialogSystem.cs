using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImg;

    [Header("文本文件")]
    public TextAsset textFile;
    public static int textIndex;
    private float textPlaySpeed = 0.05f;

    [Header("头像")]
    public Sprite kingHeadPortrait, smithHeadPortrait;

    public GameObject prefabProp;
    private const float positionSwordX = 19f;
    private const float positionSwordY = -1.5f;

    public static bool textDone;
    public bool assign;

    bool textPlayeDone, cancelTyping;

    List<string> textList = new List<string>();

    private void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void Start()
    {
        textDone = false;
    }

    private void OnEnable()
    {
        textPlayeDone = true;
        StartCoroutine(SetTextUI());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && textIndex == textList.Count)
        {
            gameObject.SetActive(false);
            textDone = true;
            textIndex = 0;
            if (assign)
                prefabProp.SetActive(true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (textPlayeDone && !cancelTyping)
                StartCoroutine(SetTextUI());
            else if (!textPlayeDone)
            {
                cancelTyping = !cancelTyping;
            }
        }

    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        textIndex = 0;

        var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textPlayeDone = false;
        textLabel.text = "";

        switch (textList[textIndex])
        {
            case "S\r":
                faceImg.sprite = smithHeadPortrait;
                textIndex++;
                break;
            case "K\r":
                faceImg.sprite = kingHeadPortrait;
                textIndex++;
                break;
        }

        int letter = 0;
        while (!cancelTyping && letter < textList[textIndex].Length - 1)
        {
            textLabel.text += textList[textIndex][letter];
            letter++;
            yield return new WaitForSeconds(textPlaySpeed);
        }

        textLabel.text = textList[textIndex];
        cancelTyping = false;

        textPlayeDone = true;
        textIndex++;
    }
}
