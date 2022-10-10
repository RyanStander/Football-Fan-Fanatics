using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Units;

public class UnitSelectionComponent : MonoBehaviour
{
    bool isSelecting = false;
    Vector3 mousePosition1;

    public GameObject selectionCirclePrefab;

    private IList<SelectableUnitComponent> selectedObjects = new List<SelectableUnitComponent>();

    void Update()
    {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObjects.Count > 0)
                return;

            isSelecting = true;
            mousePosition1 = Input.mousePosition;
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedObjects.Count > 0)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hit, 100))
                {
                    bool isTargettingNode = hit.transform.gameObject.GetComponentInParent<NodeController>() != null;

                    foreach (var selectableObject in selectedObjects)
                    {
                        if (selectableObject == null)
                        {
                            continue;
                        }
                        
                        if (isTargettingNode)
                        {
                            selectableObject.GetComponentInChildren<TargetController>().SetNodeTarget(hit.transform.gameObject);
                        }
                        else
                        {
                            selectableObject.GetComponentInChildren<TargetController>().ResetTarget();
                            
                            Vector3 destination = hit.point + UnityEngine.Random.insideUnitSphere*(0.1f*(selectedObjects.Count/3f)); 
                            destination.y = 0;
                            selectableObject.GetComponent<NavMeshAgent>().destination = destination;
                        }
                    }

                    CleanUpSelection();
                }
            }

            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    selectedObjects.Add(selectableObject);
                }
            }

            isSelecting = false;
        }

        // Highlight all objects within the selection box
        if (isSelecting)
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    if (selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle = Instantiate(selectionCirclePrefab);
                        selectableObject.selectionCircle.transform.SetParent(selectableObject.transform, false);
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3(90, 0, 0);
                    }
                }
                else
                {
                    if (selectableObject.selectionCircle != null)
                    {
                        Destroy(selectableObject.selectionCircle.gameObject);
                        selectableObject.selectionCircle = null;
                    }
                }
            }
        }
    }

    public bool IsWithinSelectionBounds( GameObject gameObject )
    {
        if( !isSelecting )
            return false;

        var camera = Camera.main;
        var viewportBounds = Toolbox.GetViewportBounds( camera, mousePosition1, Input.mousePosition );
        return viewportBounds.Contains( camera.WorldToViewportPoint( gameObject.transform.position ) );
    }

    private void CleanUpSelection()
    {
        foreach (var selectableObject in selectedObjects)
        {
            if (selectableObject.selectionCircle != null)
            {
                Destroy(selectableObject.selectionCircle.gameObject);
                selectableObject.selectionCircle = null;
            }
        }
        selectedObjects.Clear();
    }

    void OnGUI()
    {
        if( isSelecting )
        {
            // Create a rect from both mouse positions
            var rect = Toolbox.GetScreenRect( mousePosition1, Input.mousePosition );
            Toolbox.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
            Toolbox.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
        }
    }
}