using dataAccess;
using Api.Dto.Model;
using UnityEngine;
using UnityEngine.UI;

namespace mainMenu
{
    public class skillStoneDetail : MonoBehaviour
    {
        [Space(2)]
        [Header("技能信息")]
        public Text keyname;
        public Text Showname;
        public Text type;
        
        [Space(7)]
        [Header("MonsterBox")]
        public MonsterBox _MonsterBox;
        
        [Space(7)]
        [Header("UI elements 使用中角色头像的位置")]
        public RectTransform usingCharacterIconPlace;
        private charIcon stoneusingcharIcon;
        
        [Space(7)]
        [Header("EXTypes")]
        public GameObject Ex1Icon,Ex2Icon,Ex3Icon;
        
        public void showSkillStoneExType(int eX)
        {
            switch (eX)
            {
                case 0:
                    Ex1Icon.SetActive(false);
                    Ex2Icon.SetActive(false);
                    Ex3Icon.SetActive(false);
                break;
                case 1:
                    Ex1Icon.SetActive(true);
                    Ex2Icon.SetActive(false);
                    Ex3Icon.SetActive(false);
                break;
                case 2:
                    Ex1Icon.SetActive(true);
                    Ex2Icon.SetActive(true);
                    Ex3Icon.SetActive(false);
                break;
                case 3:
                    Ex1Icon.SetActive(true);
                    Ex2Icon.SetActive(true);
                    Ex3Icon.SetActive(true);
                break;
                case -1:
                    Ex1Icon.SetActive(false);
                    Ex2Icon.SetActive(false);
                    Ex3Icon.SetActive(false);
                    break;
            }
        }
        
        public void Switchusingmonstericon(string stonemonsterOfPlayerId)
        {
            SkillStoneOfPlayerInfoModel SkillStoneOfPlayerInfoModel = MySkillStonesReader.Instance.getSkillStoneOfPlayerInfoModelByMyStoneId(stonemonsterOfPlayerId);
            if (SkillStoneOfPlayerInfoModel != null)
            {
                charIcon charIcon = MonsterBox.GetCharIcon(SkillStoneOfPlayerInfoModel.inUsingMonsterOfPlayerId);
                if (charIcon != null)
                {
                    if (stoneusingcharIcon)
                        stoneusingcharIcon.gameObject.transform.SetParent(_MonsterBox.MonsterBoxContainer);
                    charIcon.gameObject.SetActive(true);
                    charIcon.gameObject.transform.SetParent(usingCharacterIconPlace);
                    charIcon.transform.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                    charIcon.transform.localScale = Vector3.one;
                    stoneusingcharIcon = charIcon;
                }
                else
                {
                    if (stoneusingcharIcon)
                        stoneusingcharIcon.gameObject.transform.SetParent(_MonsterBox.MonsterBoxContainer);
                }
            }
            else
            {
                if (stoneusingcharIcon)
                    stoneusingcharIcon.gameObject.transform.SetParent(_MonsterBox.MonsterBoxContainer);
            }
        }
    }
}