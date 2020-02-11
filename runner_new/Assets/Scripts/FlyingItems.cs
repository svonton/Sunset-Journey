using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingItems : MonoBehaviour
{
    public GameObject ParabolaRoot;
    public Item_spawner_parabola ISp;
    Vector3 playerpos;
    private string name1;
    private void OnTriggerEnter(Collider other)
    {
        ISp = FindObjectOfType<Item_spawner_parabola>();
        ParabolaRoot = GameObject.Find("ParabolaRoot");
        name1 = other.gameObject.name;
        if ((name1 != "trailer2_one_figure")&&(name1 != "arcanoid") && (name1 != "server_stand") && (name1 != "casete_prephab")) {
            if (ISp.check == 0)
            {
                playerpos = new Vector3(ISp.playerTR.position.x, 0.876f, -2f);
                ParabolaRoot.transform.GetChild(0).position = ISp.SC.spawnpoint_right.transform.position;
                ParabolaRoot.transform.GetChild(2).position = playerpos;
                GetComponent<ParabolaController>().FollowParabola();
            }
            else if (ISp.check == 1)
            {
                playerpos = new Vector3(ISp.playerTR.position.x, 0.876f, -2f);
                ParabolaRoot.transform.GetChild(0).position = ISp.SC.spawnpoint_center.transform.position;
                ParabolaRoot.transform.GetChild(2).position = playerpos;
                GetComponent<ParabolaController>().FollowParabola();
            }
            else if (ISp.check == 2)
            {
                playerpos = new Vector3(ISp.playerTR.position.x, 0.876f, -2f);
                ParabolaRoot.transform.GetChild(0).position = ISp.SC.spawnpoint_left.transform.position;
                ParabolaRoot.transform.GetChild(2).position = playerpos;
                GetComponent<ParabolaController>().FollowParabola();
            }
        }
    }
}
