using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _sliderMusic;

    [SerializeField] private Slider _sliderEffects;

    [SerializeField] private Text _sliderMusicText;

    [SerializeField] private Text _sliderEffectsText;

    void Start()
    {
            _sliderMusic.onValueChanged.AddListener((v) =>
            {
                _sliderMusicText.text = v.ToString("0");
            });

            _sliderEffects.onValueChanged.AddListener((v) =>
            {
                _sliderEffectsText.text = v.ToString("0");
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
