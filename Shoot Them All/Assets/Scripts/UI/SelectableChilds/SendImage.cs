using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;
/// <summary>
/// Script para uso de selector de colores horizontal
/// </summary>
//[AddComponentMenu("UI/Lateral Bar")]
public class SendImage : Selectable,IMoveHandler
{
    [SerializeField]
    private int Value = 0;

    TMP_Text TextMeshPro;

    public UnityEvent<int> OnValueChanged;
    protected override void OnEnable()
    {
        base.OnEnable();
        TextMeshPro = gameObject.GetComponentInChildren<TMP_Text>();
    }
    public override void OnMove(AxisEventData axisEvent)
    {
        Value += (int)axisEvent.moveVector.x;
        TextMeshPro.text = Value.ToString();
        OnValueChanged?.Invoke(Value);
        if(axisEvent.moveVector.y != 0)
        {
            base.OnMove(axisEvent);
        }
    }

}
