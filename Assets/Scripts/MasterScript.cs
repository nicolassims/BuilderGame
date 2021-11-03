using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterScript : MonoBehaviour {
    public bool deleting = false;//whether the cursor is now in deleting mode
    public float size = 1.0f;

    // Update is called once per frame
    void Update() {
        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta > 0 && size < 8) {
            size *= 2;
        } else if (scrollDelta < 0 && size > 1/8) {
            size /= 2;
        }
        scrollDelta = 0;
        deleting = Input.GetMouseButton(1);
        
    }
}
