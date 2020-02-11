using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.y -= Time.deltaTime;
        if (offset.y<-10f) {
            offset.y = 0;
        }
        mat.mainTextureOffset = offset;
    }
}
