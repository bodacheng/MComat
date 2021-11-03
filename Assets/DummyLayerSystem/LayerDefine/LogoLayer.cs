using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LogoLayer : UILayer
{
    [SerializeField] private Image logo1;
    [SerializeField] private Image logo2;
    
    public bool finished = false;
    
    public async void Nagare()
    {
        finished = false;
        logo1.gameObject.SetActive(true);
        await Task.Delay(2000);
        logo1.gameObject.SetActive(false);
        logo2.gameObject.SetActive(true);
        finished = true;
    }
}
