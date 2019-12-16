using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDirButton : MonoBehaviour
{
    [SerializeField] private Color onSelectColor = Color.yellow;
    private Image buttonImage;
    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnSelect()
    {
        buttonImage.color = onSelectColor;
    }

    public void OnExit()
    {
        buttonImage.color = Color.white;
    }
}
