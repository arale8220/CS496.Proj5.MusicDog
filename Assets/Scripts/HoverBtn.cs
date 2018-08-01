using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Text theText;
    private Color color;

    private void Start()
    {
        color = theText.color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = Color.black; //Or however you do your color
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = color; //Or however you do your color
    }
}