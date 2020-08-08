using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageFadeIn : MonoBehaviour
{
    public bool isIcon;
    public float iconFadeSpeed;

    public float iconOpacity;
    float defaultImageAlpha;
    Image _image;


    private void Start()
    {
       
            defaultImageAlpha = _image.color.a;
        if (isIcon)
            print(defaultImageAlpha);
       
    }
    private void OnEnable()
    {
        _image = GetComponent<Image>();
        defaultImageAlpha = _image.color.a;

        if (!isIcon)
        StartCoroutine(FadeIn());

    }
    
  
    IEnumerator FadeIn()
    {
        float a = 0;
      
        while (a < defaultImageAlpha)
        {
            a += Time.deltaTime/2 ;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, a);
            yield return null;
        }
        yield return 0;
    }

   public IEnumerator FadeInIcon()
    {
        float a = defaultImageAlpha;

        while (a < 1)
        {
            a += Time.deltaTime* iconFadeSpeed;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, a);
            yield return null;
        }
        iconOpacity = a;
        yield return 0;
    }
    public IEnumerator FadeOutIcon()
    {

        float a = 1;
        print(defaultImageAlpha + "--");
        while (a > defaultImageAlpha)
        {
            a -= Time.deltaTime* iconFadeSpeed;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, a);
            yield return null;
        }
        iconOpacity = a;
        yield return 0;
    }

   
}
