using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : MonoBehaviour {
    public GameObject hilightprefab;
    private static GameObject hilighter, lastObject;
    private static Vector3 lastFace;
    private static int numberCubes = 0;

    private void Start() {
        numberCubes++;
    }

    private void OnMouseOver() {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
            if (!hilighter) {
                AddHilighter(hit);
            } else if (hit.normal != lastFace || hit.transform.gameObject != lastObject) {
                Destroy(hilighter);
            } else if (Input.GetMouseButtonDown(1) && numberCubes > 1) {
                numberCubes--;
                Destroy(hit.transform.gameObject);
            }
        }
    }

    private void OnMouseDown() {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {



            Debug.Log("hit!");
            DuplicateFace(hit);
        } 
    }

    private void OnMouseUp() {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
            Debug.Log("hit!");
            DuplicateFace(hit);
        } else {
            Debug.Log("registered");
        }
    }

    private void AddHilighter(RaycastHit hit) {
        lastFace = hit.normal;
        lastObject = gameObject;
        hilighter = Instantiate(hilightprefab, transform.position + hit.normal, transform.rotation, transform.parent);
    }

    private void DuplicateFace(RaycastHit hit) {
        Instantiate(gameObject, transform.position + hit.normal, transform.rotation, transform.parent);
    }
}