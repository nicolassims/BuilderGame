using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : MonoBehaviour {
    public MasterScript master;
    public GameObject hilightprefab;//the prefab used to indicate what part of the object has been selected
    private static GameObject hilighter;//the actual instance of the prefab specified above
    private static Vector3 lastPos;//the position the highlighter last occupied.
    

    private bool lastDeleting = false;//whether the last highlighter was a deleting highlighter
    private static int numberCubes = 0;//the total number of cubes in this game
    private static float lastSize = 1.0f;

    private float personalSize = 1.0f;//how large this specific block is

    private void Start() {
        numberCubes++;
    }

    private void OnMouseOver() {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
            if (!hilighter) {
                AddHilighter(hit);
            } else if (hit.normal + hit.transform.position != lastPos || master.deleting != lastDeleting || lastSize != master.size) {
                Destroy(hilighter);
            }
        }
    }

    private void OnMouseDown() {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
            if (!master.deleting) {
                DuplicateFace(hit);
            } else if (numberCubes > 1) {
                numberCubes--;
                Destroy(hit.transform.gameObject);
            }            
        } 
    }

    private Vector3 calculatePos(RaycastHit hit) {
        return master.deleting 
            ? transform.position
            : transform.position + hit.normal * ((master.size + hit.transform.gameObject.GetComponent<Duplicator>().personalSize) * 0.5f);
    } 

    private void AddHilighter(RaycastHit hit) {
        lastDeleting = master.deleting;
        lastPos = hit.normal + hit.transform.position;
        lastSize = master.size;
        hilighter = Instantiate(hilightprefab, calculatePos(hit), transform.rotation, transform.parent);
        hilighter.transform.localScale = (master.deleting ? hit.transform.localScale : Vector3.one * master.size);
    }

    private void DuplicateFace(RaycastHit hit) {
        GameObject dupe = Instantiate(gameObject, calculatePos(hit), transform.rotation, transform.parent);
        dupe.transform.localScale = Vector3.one * master.size;
        dupe.GetComponent<Duplicator>().personalSize = master.size;
    }
}