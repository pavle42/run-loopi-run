using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MonitorClick : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private EventSystem eventSystem;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var pointer = new PointerEventData(eventSystem)
            {
                position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f)
            };

            var hits = new List<RaycastResult>();
            uiRaycaster.Raycast(pointer, hits);

            foreach (var hit in hits)
            {
                if (hit.gameObject.TryGetComponent(out Button btn))
                {
                    btn.onClick.Invoke();
                    return;
                }
            }
        }
    }
}
