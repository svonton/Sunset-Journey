using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star_scroller : MonoBehaviour
{
    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.x +=0.5f* Time.deltaTime;
        if (offset.x > 10f)
        {
            offset.x = 0;
        }
        mat.mainTextureOffset = offset;
    }
}
