using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildAnchor : MonoBehaviour
{
    public Rect selectionArea = new Rect(1.0f, 1.0f, 1.0f, 1.0f);
    public bool built = false;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (!built) {
            //build when selected
        }        
    }
    
    void OnDrawGizmos() {
        // Green
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        DrawRect(selectionArea);
    }

    void DrawRect(Rect selectionArea) {
        Gizmos.DrawWireCube(new Vector3(selectionArea.center.x, selectionArea.center.y, 0.01f), new Vector3(selectionArea.size.x, selectionArea.size.y, 0.01f));
    }
}
