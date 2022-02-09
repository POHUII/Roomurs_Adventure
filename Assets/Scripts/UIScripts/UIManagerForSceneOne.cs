using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerForSceneOne : MonoBehaviour
{
    public GameObject dialogPanel, dialogButton;
    public GameObject Player;
    private bool openDialog;
    private const float positionBlackSmith = -16.7f;
    private GameObject tipJump;
    private const float positionTipJumpX = -28.7f;
    private const float positionTipJumpY = -1.02702f;

    public GameObject tipMove;

    private void Start()
    {
        openDialog = true;

        tipJump = Resources.Load<GameObject>("Prefabs/JumpTip");
        Instantiate(tipJump, new Vector2(positionTipJumpX, positionTipJumpY), Quaternion.identity);

        Player = GameObject.Find("$Player");
    }

    private void Update()
    {
        DetectDialog();
        CloseTipMove();
    }

    private void DetectDialog()
    {
        if (Mathf.Abs(Player.transform.position.x - positionBlackSmith) < 2f)
        {
            StartDialog(true);
        }
        else
            StartDialog(false);
    }

    private void StartDialog(bool isDiaglog)
    {
        dialogButton.SetActive(isDiaglog);
        if (Input.GetKey(KeyCode.R) && openDialog)
        {
            dialogPanel.SetActive(true);
            openDialog = false;
        }
    }

    private void CloseTipMove()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Destroy(tipMove);
        }
    }
}
