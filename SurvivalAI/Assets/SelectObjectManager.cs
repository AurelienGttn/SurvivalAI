using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectManager : MonoBehaviour {

    public GameObject selectedObject;
    public GameObject hitObject;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        /*RaycastHit[] hits = Physics.RaycastAll(ray, 100, 9);

        foreach (RaycastHit hitObj in hits)
        {
            if (hitObj.transform.root.gameObject.tag == "Building" || hitObj.transform.root.gameObject.tag == "Worker" || hitObj.transform.root.gameObject.tag == "Player")
            {
                hitObject = hitObj.transform.root.gameObject;
            }
        }*/

        if (Physics.Raycast(ray, out hitInfo, 100, 9) || Physics.Raycast(ray, out hitInfo, 100, 10))
        {
            hitObject = hitInfo.transform.root.gameObject;

            if (Input.GetMouseButtonDown(0))
            {
                SelectObject(hitObject);
            }
        }
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


