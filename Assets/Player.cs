using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Stacker _stacker;
    private Camera cam;
    private Vector3 point;
    private Color lineColor;
    private bool canPickup;
    [SerializeField] private Transform floatLayer;
    private Card card;
    private void Start()
    {
        cam = GetComponent<Camera>();
        _stacker = FindObjectOfType<Stacker>();
    }

    private void Update()
    {
        canPickup = false;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        LayerMask mask = LayerMask.GetMask("Default");
        
        if (Physics.Raycast(ray,out RaycastHit hitInfo, 10000f, mask))
        {
            point = hitInfo.point;
            switch (hitInfo.collider.tag)
            {
                case "Stacked":
                    lineColor = Color.cyan;
                    break;
                case "Top":
                    canPickup = true;
                    lineColor = Color.green;
                    break;
                case "Holding":
                    lineColor = Color.yellow;
                    break;
                default:
                    lineColor = Color.white;
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (card)
                card = null;

            else if (canPickup)
            {
                card = _stacker.Take();
                card.transform.SetParent(null);
                card.transform.position += Vector3.up * .075f;
                card.tag = "Holding";
            
                floatLayer.position = new Vector3(floatLayer.position.x, card.transform.position.y, floatLayer.position.z);
            }
        }

        if (card && Physics.Raycast(ray, out RaycastHit hitInfo2, 10000f, LayerMask.GetMask("Float")))
        {
            card.transform.position = hitInfo2.point;
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        Gizmos.DrawLine(transform.position, point);
    }
}
