using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : MonoBehaviour {
    public GameObject hilightprefab;
    private static GameObject hilighter;
    private static Vector3 lastPos;
    private bool lastDeleting = false;
    private bool deleting = false;
    private static int numberCubes = 0;

    private void Start() {
        numberCubes++;
    }

    private void Update() {
        deleting = Input.GetMouseButton(1);
    }

    private void OnMouseOver() {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
            if (!hilighter) {
                AddHilighter(hit);
            } else if (hit.normal + hit.transform.position != lastPos || deleting != lastDeleting) {
                Destroy(hilighter);
            }
        }
    }

    private void OnMouseDown() {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
            if (!deleting) {
                DuplicateFace(hit);
            } else if (numberCubes > 1) {
                numberCubes--;
                Destroy(hit.transform.gameObject);
            }            
        } 
    }

    private void AddHilighter(RaycastHit hit) {
        lastDeleting = deleting;
        lastPos = hit.normal + hit.transform.position;
        hilighter = Instantiate(hilightprefab, transform.position + (deleting ? Vector3.zero : hit.normal), transform.rotation, transform.parent);
    }

    private void DuplicateFace(RaycastHit hit) {
        Instantiate(gameObject, transform.position + hit.normal, transform.rotation, transform.parent);
    }
}