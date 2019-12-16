using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Brake : MonoBehaviour
{
    [SerializeField] private float brakeTime;
    private Material material;
    private Color defaultColor;
    public delegate void VoidDelegate();
    public static event VoidDelegate onBrake;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        defaultColor = material.color;
    }

    [SerializeField] private float brakeCooldown = 8;
    private bool canBrake = true;
    
    public void BrakeOn()
    {
        if (!canBrake)
        {
            return;
        }

        canBrake = false;
        transform.parent.DOLocalRotate(new Vector3(-30, 0, -17.3f), brakeTime);
        onBrake?.Invoke();

        DOVirtual.DelayedCall(brakeCooldown, ResetBrake);
    }

    public void ResetBrake()
    {
        transform.parent
            .DOLocalRotate(new Vector3(30, 0, -17.3f), brakeTime)
            .OnComplete(() =>
            {
                canBrake = true;
            });
    }

    public void PointerEnter()
    {
        if (canBrake)
        {
            SwitchColor(Color.red);
        }
    }

    public void PointerExit()
    {
        SwitchColor(defaultColor);
    }
    
    public void SwitchColor(Color color)
    {
        material.color = color;
    }
}
