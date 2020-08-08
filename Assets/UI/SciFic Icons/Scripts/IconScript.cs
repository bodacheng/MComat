
using UnityEngine;
using UnityEngine.EventSystems;

public class IconScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerClickHandler
{
    ImageFadeIn imageFade;

    void Start()
    {
        imageFade = GetComponent<ImageFadeIn>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

        StartCoroutine(imageFade.FadeInIcon());
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        StartCoroutine(imageFade.FadeOutIcon());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(imageFade.FadeOutIcon());
        StartCoroutine(imageFade.FadeInIcon());
    }
}
