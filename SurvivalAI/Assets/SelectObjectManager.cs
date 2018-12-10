using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectManager : MonoBehaviour {

    public GameObject selectedObject;
    public GameObject hitObject;

    public Camera cam;

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

        RaycastHit hitInfo;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray.origin, ray.direction * 100);

        if (Physics.Raycast(ray.origin,ray.direction * 100, out hitInfo))
        {
            foreach (RaycastHit hitObj in hits)
            {
                if (hitObj.collider.gameObject.GetComponent<CapsuleCollider>().tag == "ClicCollider")
                {
                    hitObject = hitObj.transform.root.gameObject;
                }
            }
        }
            

        /*if (Physics.Raycast(ray.origin, ray.direction *100, out hitInfo) && hitInfo.transform.gameObject.tag == "Worker")
        {
            hitObject = hitInfo.transform.root.gameObject;

            if (Input.GetMouseButtonDown(0))
            {
                SelectObject(hitObject);
            }
        }*/
    }

    void SelectObject(GameObject obj)
    {
        if(selectedObject != null)
        {
            if (obj == selectedObject)
            {
                return;
            }
            ClearSelection();
        }

        selectedObject = obj;
    }

    void ClearSelection()
    {
        selectedObject = null;
    }
}


