using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource somDano,somScore,somHit,
        somSparkle,somResult,somBreak,somBolaMagic,somGrass,
        somAqua, somGlass, somMagicAtive;

    public static AudioManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }
   
    //FUNÇÕES SÃO CHAMADAS PELOS SCRIPTS QUE POSSUEM OS AUDIOCLIPS
    //============================================================

    //GLASS  SOM
    public void GlassBall(AudioClip clipAudio)
    {
        if(somGlass == null)
        {
            somGlass = gameObject.AddComponent<AudioSource>();
            somGlass.clip = clipAudio;
            somGlass.Play();
        }
        if (somGlass!= null)
        {
             somGlass.clip = clipAudio;
             somGlass.Play();
        }       
    }
    //BOLA MAGIC SOM
    public void BolaMagic(AudioClip clipAudio)
    {
        if (somBolaMagic == null)
        {
            somBolaMagic = gameObject.AddComponent<AudioSource>();
            somBolaMagic.clip = clipAudio;
            somBolaMagic.Play();
        }
        if (somBolaMagic != null)
        {
            somBolaMagic.clip = clipAudio;
            somBolaMagic.Play();
        }
    }
    
    //SHATTER SOM
    public void Shatter(AudioClip clipAudio)
    {
        if (somBreak == null)
        {
            somBreak = gameObject.AddComponent<AudioSource>();
            somBreak.clip = clipAudio;
            somBreak.Play();
        }
        if (somBreak != null)
        {
            somBreak.clip = clipAudio;
            somBreak.Play();
        }
    }
    //DANO SOM
    public void Dano(AudioClip clipAudio)
    {
        if (somDano == null)
        {
            somDano = gameObject.AddComponent<AudioSource>();
            somDano.clip = clipAudio;
            somDano.Play();
        }
        if (somDano != null)
        {
            somDano.clip = clipAudio;
            somDano.Play();
        }
    }
    //SCORE SOM
    public void Score(AudioClip clipAudio)
    {
        if (somScore == null)
        {
            somScore = gameObject.AddComponent<AudioSource>();
            somScore.clip = clipAudio;
            somScore.Play();
        }
        if (somScore != null)
        {
            somScore.clip = clipAudio;
            somScore.Play();
        }
    }
    //SPARKLE SOM
    public void Sparkle(AudioClip clipAudio)
    {
        if (somSparkle == null)
        {
            somSparkle = gameObject.AddComponent<AudioSource>();
            somSparkle.clip = clipAudio;
            somSparkle.Play();
        }
        if (somSparkle != null)
        {
            somSparkle.clip = clipAudio;
            somSparkle.Play();
        }
    }
    //RESULT SOM
    public void Result(AudioClip clipAudio)
    {
        if (somResult == null)
        {
            somResult = gameObject.AddComponent<AudioSource>();
            somResult.clip = clipAudio;
            somResult.Play();
        }
        if (somResult != null)
        {
            somResult.clip = clipAudio;
            somResult.Play();
        }
    }
    //MAGIC ACTIVATION SOM
    public void MagicAtive(AudioClip clipAudio)
    {
        if (somMagicAtive == null)
        {
            somMagicAtive = gameObject.AddComponent<AudioSource>();
            somMagicAtive.clip = clipAudio;
            somMagicAtive.Play();
        }
        if (somMagicAtive != null)
        {
            somMagicAtive.clip = clipAudio;
            somMagicAtive.Play();
        }
    }




}
