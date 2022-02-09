using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemes : MonoBehaviour
{
    public GameObject swrod;
    public GameObject title;
    public GameObject cave;
    public GameObject player;
    public GameObject tree_1, tree_2;

    public void DestroyItem()
    {
        Destroy(swrod);
        Destroy(title);
    }

    public void DestroyItemes()
    {
        Destroy(cave);
        Destroy(player);
        Destroy(tree_1);
        Destroy(tree_2);
    }
}
