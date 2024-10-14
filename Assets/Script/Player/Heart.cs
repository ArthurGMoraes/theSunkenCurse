using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Sprite emptyHeart, halfheart, fullHeart;
    Image heart;

    private void Awake()
    {
        heart = GetComponent<Image>(); 
    }

    public void SetHeart(HeartStatus status) { 
        switch (status){
            case HeartStatus.Empty:
                heart.sprite = emptyHeart;
                break;
            case HeartStatus.Half:
                heart.sprite = halfheart;
                break;
            case HeartStatus.Full:
                heart.sprite = fullHeart;
                break;
        }
    }
}

public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}
