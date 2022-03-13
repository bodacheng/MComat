using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;

namespace FightScene
{
    public partial class TeamUIManager : MonoBehaviour
    {
        Text rotationModeHitCombo;
        
        void RotateClear()
        {
            UnitIconDic.Clear();
            rotationModeHitCombo.text = "";
        }
        
        void IniComboHit(ReactiveProperty<Data_Center> RMode_Unit)
        {
            RMode_Unit.Subscribe(x =>
            {
                if (rotationModeHitCombo != null)
                {
                    Destroy(rotationModeHitCombo.gameObject);
                }
                
                if (x != null)
                {
                    rotationModeHitCombo = Instantiate(HitCombo);
                    rotationModeHitCombo.name = teamConfig.myTeam + "HitCombo";
                    
                    rotationModeHitCombo.color = teamConfig.myTeam == RTFightManager.playerTeam ? Color.yellow : Color.blue;
                    rotationModeHitCombo.gameObject.SetActive(true);
                    if (rotationModeHitCombo.gameObject.transform.parent != _targetCanvasT)
                    {
                        rotationModeHitCombo.gameObject.transform.SetParent(_targetCanvasT.transform);
                    }
                    rotationModeHitCombo.transform.localScale = Vector3.one;
                    rotationModeHitCombo.fontSize = 30;
                    
                    x.FightDataRef._comboHitCount.HitCount.Subscribe(h =>
                    {
                        if (h > 1)
                        {
                            rotationModeHitCombo.text = h + "Hits!";
                            rotationModeHitCombo.transform.DOMove(CameraManager._camera.WorldToScreenPoint(x.transform.position + Vector3.up * 1f + Vector3.right * 3.2f), 1);
                        }
                        else
                        {
                            rotationModeHitCombo.text = null;
                            switch (teamConfig.myTeam)
                            {
                                case Team.player1:
                                    rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(-200, Screen.height + 100), 1);
                                    break;
                                case Team.player2:
                                    rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(Screen.width + 200, Screen.height + 100), 1);
                                    break;
                                default:
                                    rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(-100, -100), 1);
                                    break;
                            }
                        }
                    }).AddTo(rotationModeHitCombo.gameObject);
                }
            }).AddTo(this.gameObject);
        }
    }
}