using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class AudioColorManager : MonoBehaviour
{
    private Color32 TunnelColor;

    public float bias;
    public float timeStep;
    public Material LineMat;

    private float m_previousAudioValue;
    private float m_audioValue;
    private float m_timer;
    // private bool m_isBeat;
    private int _Random;

    private void OnBeat()
    {
        m_timer = 0;
        // m_isBeat = true;
        System.Random random = new System.Random();
        int Random = random.Next(1, 11);
        while (_Random == Random || (_Random == 3 && Random == 8) || (_Random == 8 && Random == 3) || (Random == 1 && TunnelColor.r == 241) || (Random == 2 && TunnelColor.g == 255 && TunnelColor.r == 0) || (Random == 3 && TunnelColor.b == 214) || (Random == 3 && TunnelColor.b == 233) || (TunnelColor.r == 255 && TunnelColor.g == 255 && TunnelColor.b == 255 && (Random == 8 || Random == 9 || Random == 7 || Random == 4)) || ((Random == 5 || Random == 7) && TunnelColor.r == 255 && TunnelColor.g == 105))
        {
            Random = random.Next(1, 11);
        }
        _Random = Random;
        // Debug.Log(_Random);
        switch (Random)
        {
            case 1:
                {
                    LineMat.color = Color.red;
                    LineMat.SetColor("_EmissionColor", Color.red);
                    //_Bloom.color.Override(Color.red);
                    break;
                }
            case 2:
                {
                    LineMat.color = Color.green;
                    LineMat.SetColor("_EmissionColor", Color.green);
                    //_Bloom.color.Override(Color.green);
                    break;
                }
            case 3:
                {
                    LineMat.color = Color.blue;
                    LineMat.SetColor("_EmissionColor", Color.blue);
                    //_Bloom.color.Override(Color.blue);
                    break;
                }
            case 4:
                {
                    LineMat.color = new Color32(205, 112, 227, 255);
                    LineMat.SetColor("_EmissionColor", new Color32(205, 112, 227, 255));
                    //_Bloom.color.Override(new Color32(205, 112, 227, 255));
                    break;
                }
            case 5:
                {
                    LineMat.color = new Color32(255, 20, 147, 255);
                    LineMat.SetColor("_EmissionColor", new Color32(255, 20, 147, 255));
                    //_Bloom.color.Override(new Color32(255, 20, 147, 255));
                    break;
                }
            case 6:
                {
                    LineMat.color = new Color32(255, 165, 0, 255);
                    LineMat.SetColor("_EmissionColor", new Color32(255, 165, 0, 255));
                    // _Bloom.color.Override(new Color32(255, 165, 0, 255));

                    break;
                }
            case 7:
                {
                    LineMat.color = new Color32(251, 101, 127, 255);
                    LineMat.SetColor("_EmissionColor", new Color32(251, 101, 127, 255));
                    //_Bloom.color.Override(new Color32(251, 101, 127, 255));

                    break;
                }
            case 8:
                {
                    LineMat.color = new Color32(127, 202, 255, 255);
                    LineMat.SetColor("_EmissionColor", new Color32(127, 202, 255, 255));
                    //_Bloom.color.Override(new Color32(127, 202, 255, 255));

                    break;
                }
            case 9:
                {
                    LineMat.color = new Color32(143, 236, 247, 255);
                    LineMat.SetColor("_EmissionColor", new Color32(143, 236, 247, 255));
                    // _Bloom.color.Override(new Color32(143, 236, 247, 255));

                    break;
                }
            case 10:
                {
                    LineMat.color = new Color32(70, 117, 174, 255);
                    LineMat.SetColor("_EmissionColor", new Color32(70, 117, 174, 255));
                    // _Bloom.color.Override(new Color32(70, 117, 174, 255));

                    break;
                }
        }
    }

    private void Start()
    {
        ColorData _ColorData = SaveSystem.LoadColorofLand();
        TunnelColor = new Color32(_ColorData.Color[0], _ColorData.Color[1], _ColorData.Color[2], 255);
        OnBeat();
        if (SceneManager.GetActiveScene().name != "UnlimitedLevel")
        {
            switch (AudioManager.CurrentMusic)
            {
                case 0:
                    {
                        timeStep = 0.2f;
                        bias = 5;
                        break;
                    }
                case 1:
                    {
                        bias = 2f;
                        timeStep = 0.53f;
                        break;
                    }
                case 2:
                    {
                        goto case 0;
                    }
                case 3:
                    {
                        timeStep = 0.45f;
                        bias = 10.7f;
                        break;
                    }
                case 4:
                    {
                        timeStep = 0.3f;
                        bias = 10.7f;
                        break;
                    }
               /* case 5:
                    {

                        break;
                    }*/
            }
            for (int i = 2; ; ++i, LineMoving.BoostEachLevel += 0.01f)
            {
                if (SceneManager.GetActiveScene().buildIndex == i)
                {
                    LineMoving.Speed *= LineMoving.BoostEachLevel;
                    break;
                }
            }
        }
        else
        {
            switch (AudioManager.CurrentMusic)
            {
                case 0:
                    {
                        timeStep = 0.15f;
                        bias = 2;
                        break;
                    }
                case 1:
                    {
                        bias = 8f;
                        timeStep = 0.25f;
                        break;
                    }
                case 3:
                    {
                        timeStep = 0.4f;
                        bias = 10f;
                        break;
                    }
                case 4:
                    {
                        timeStep = 0.3f;
                        bias = 14f;
                        break;
                    }
            }
        }
    }

    private void Update()
    {
        if (!FinishGame.IsCompleted)
        {
            m_previousAudioValue = m_audioValue;
            m_audioValue = AudioManager.SpectrumValue;
            if (AudioManager.CurrentMusic == 2 && SceneManager.GetActiveScene().name == "UnlimitedLevel")
            {
                if ((((m_previousAudioValue > 10/* && m_audioValue <= bias*/) || (m_audioValue > 10 && m_previousAudioValue <= 10)) && m_timer > 0.4f) || (((m_previousAudioValue > 0.02 && m_audioValue <= 0.02) || (m_audioValue > 0.02 && m_previousAudioValue <= 0.02)) && m_timer > 0.3f))
                {
                    OnBeat();
                }
                if ((m_previousAudioValue > bias && m_audioValue <= bias) || (m_audioValue > bias && m_previousAudioValue <= bias))
                {
                    if (m_timer > timeStep)
                    {
                        OnBeat();
                    }
                }
            }
            else
            {
                if ((m_previousAudioValue > bias && m_audioValue <= bias) || (m_audioValue > bias && m_previousAudioValue <= bias))
                {
                    if (m_timer > timeStep)
                    {
                        OnBeat();
                    }
                }
            }
            m_timer += Time.deltaTime;
        }
    }

}
