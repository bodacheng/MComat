using DG.Tweening;
using UnityEngine;

namespace FightScene
{
    public partial class FightTeam_RotationMode : FightTeam
    {
        public override void Refresh()
        {
            base.Refresh();
            rotationModeHitCombo.color = teamConfig.myTeam == RealTimeGameProcessManager.playerTeam ? Color.yellow : Color.blue;
            rotationModeHitCombo.gameObject.SetActive(true);
            if (rotationModeHitCombo.gameObject.transform.parent != _targetCanvas)
            {
                rotationModeHitCombo.gameObject.transform.SetParent(_targetCanvas.transform);
            }
            rotationModeHitCombo.transform.localScale = Vector3.one;
            rotationModeHitCombo.fontSize = 30;
        }
        
        void RefreshComboHitRotationMode(Data_Center _datacenter)
        {
            if (_datacenter.FightDataRef._ComboHitCount.HitCount.Value > 1)
            {
                rotationModeHitCombo.text = _datacenter.FightDataRef._ComboHitCount.HitCount.Value.ToString() + "Hits!";
                rotationModeHitCombo.transform.DOMove(CameraManager._camera.WorldToScreenPoint(_datacenter.transform.position + Vector3.up * 1f + Vector3.right * 3.2f), 0.2f);
            }
            else
            {
                switch (teamConfig.myTeam)
                {
                    case Team.player1:
                        rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(-200, Screen.height + 100), 0.2f);
                        break;
                    case Team.player2:
                        rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(Screen.width + 200, Screen.height + 100), 0.2f);
                        break;
                    default:
                        rotationModeHitCombo.rectTransform.DOAnchorPos(new Vector2(-100, -100), 0.2f);
                        break;
                }
            }
        }
    }
}