using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
using TMPro;

public enum HPBarDisplayMode : int
{
	onlyNearEnemies = 1,
	allEnemies = 2,
    hide = 3
}

//角色列表的职责现在不光是负责两侧菜单中角色的icon，也负责被控制角色又上角血条和ex条
public class jueSeLiebiao : MonoBehaviour
{
    private bool team1sideicons_on = true;
    private bool team2sideicons_on = false;
    private bool team1comboshow_on = true;
    private bool team2comboshow_on = true;
    private bool team1hpbar_on = true;
    private bool team2hpbar_on = true;

    [Header("Basic Element")]
    public CharsManager _CharSetManager;
	public CameraManager _CameraManager;
	public mobileInputsManager _mobileInputsManager;

    [Header("TargetCavanas")]
    [Space(6)]
    public Canvas _targetCanvas;

    [Header("两侧角色菜单相关参数")]
    [Space(6)]
    public SideCharIcon button_prefab;//这个是为了确定按钮的样式，可能图片也包括在这个pretab里
	public RectTransform Team1Container,Team2Container; // 这个就是为了利用layout这个组件.而关键麻烦的是那两排icon的隐藏。应该靠的是这个transform的位移

	[Header("左上角控制中角色信息")]
	[Space(6)]
	public charIcon controlleringChar;
	public Slider controllerHPbar;
	public Slider controllerEXbar;
    public Slider controllerResistbar;
    public TextMeshProUGUI HitCombo;

    [Header("Auto BUtton")]
    [Space(6)]
    public Button autoBUtton;
    public Image _C_button;
    public Image _AI_button;

    [Header("浮动血条pretab")]
	[Space(6)]
	public Slider hpBarPrefab;
    [Header("浮动抵抗pretab")]
    [Space(6)]
    public Slider resistBarPrefab;

    private HPBarDisplayMode hPBarDisplayMode = HPBarDisplayMode.allEnemies;
    private IDictionary<Data_Center, SideCharIcon> datacenterCharIconDic = new Dictionary<Data_Center, SideCharIcon>();
    private IDictionary<Data_Center, Slider> datacenterHPBarDic = new Dictionary<Data_Center,Slider>();
    private IDictionary<Data_Center, Slider> datacenterResistBarDic = new Dictionary<Data_Center, Slider>();
    private IDictionary<Data_Center, TextMeshProUGUI> datacenterHitComboDic = new Dictionary<Data_Center, TextMeshProUGUI>();
    private Data_Center focusingChar;

    public void fightGUIProcess()
    {
        if (focusingChar != null)     
            CurrentControllerHPEXShow(focusingChar);

        if (team2hpbar_on)
        {
            switch (hPBarDisplayMode)
            {
                case HPBarDisplayMode.allEnemies:
                    showEnemyHPBar(Team.player2);
                    break;
                case HPBarDisplayMode.onlyNearEnemies:
                    showEnemyHPBar(Team.player2);
                    break;
                case HPBarDisplayMode.hide:
                    break;
                default:
                    break;
            }
        }

        if (team1comboshow_on)// 固定为图标附近
        {
            if (_CharSetManager.TeamMembers.ContainsKey(Team.player1))
            {
                foreach (Data_Center _datacenter in _CharSetManager.TeamMembers[Team.player1])
                {
                    if (_datacenter != null)
                    {
                        if (datacenterHitComboDic.ContainsKey(_datacenter))
                        {
                            datacenterHitComboDic[_datacenter].color = Color.yellow;
                            refreshComboHit(_datacenter, datacenterHitComboDic[_datacenter], false);
                        }
                    }
                }
            }
        }

        if (team2comboshow_on)// 固定为图标附近
        {
            if (_CharSetManager.TeamMembers.ContainsKey(Team.player2))
            {
                foreach (Data_Center _datacenter in _CharSetManager.TeamMembers[Team.player2])
                {
                    if (_datacenter != null)
                    {
                        if (datacenterHitComboDic.ContainsKey(_datacenter))
                        {
                            datacenterHitComboDic[_datacenter].color = Color.yellow;
                            refreshComboHit(_datacenter, datacenterHitComboDic[_datacenter], true);
                        }
                    }
                }
            }
        }
    }

    void refreshComboHit(Data_Center Data_Center, TextMeshProUGUI _hitcomboText,bool onHead)//onHead = true :显示在头顶，false ：显示在头像旁
    {
        if (Data_Center != null)
        {
            if (Data_Center.getBOHealth() != null)
            {
                if (Data_Center.getBOHealth().getHitCount() > 1)
                {
                    _hitcomboText.gameObject.SetActive(true);
                    _hitcomboText.text = Data_Center.getBOHealth().getHitCount().ToString() + "Hits!";

                    if (Data_Center == this.focusingChar)
                    {
                        if (_hitcomboText.gameObject.transform.parent != controlleringChar.gameObject.transform)
                            _hitcomboText.gameObject.transform.SetParent(controlleringChar.gameObject.transform);
                        _hitcomboText.transform.localScale = Vector3.one;
                        _hitcomboText.transform.position = controlleringChar.transform.position - Vector3.up * 70f;
                        _hitcomboText.fontSizeMax = 100f;
                    }
                    else{
                        if (onHead)
                        {
                            if (_hitcomboText.gameObject.transform.parent != _targetCanvas)
                                _hitcomboText.gameObject.transform.SetParent(_targetCanvas.transform);
                            _hitcomboText.transform.localScale = Vector3.one;
                            _hitcomboText.fontSizeMax = 30f;
                            _hitcomboText.transform.position =
                                Vector3.Lerp(_hitcomboText.transform.position,
                                             CameraManager._camera.WorldToScreenPoint(Data_Center.gameObject.transform.position + Vector3.up * 2.5f + Vector3.right * 2.5f),
                                Time.deltaTime * 20f);
                        }else{
                            SideCharIcon button;
                            datacenterCharIconDic.TryGetValue(Data_Center, out button);
                            if (button != null)
                            {
                                if (_hitcomboText.gameObject.transform.parent != button.gameObject.transform)
                                    _hitcomboText.gameObject.transform.SetParent(button.gameObject.transform);
                                _hitcomboText.transform.localScale = Vector3.one;
                                _hitcomboText.transform.position = button.transform.position + Vector3.right * 1f;
                                _hitcomboText.fontSizeMax = 30f;
                            }
                        }
                    }
                }
                else
                {
                    _hitcomboText.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            _hitcomboText.gameObject.SetActive(false);
        }
    }

    void showEnemyHPBar(Team enemyTeam)
	{
        if (this.hPBarDisplayMode == HPBarDisplayMode.hide
            || (this.hPBarDisplayMode == HPBarDisplayMode.onlyNearEnemies && this.focusingChar == null))
        {
            return;
        }
        if (_CharSetManager.TeamMembers == null)
			return;
            
        if (!_CharSetManager.TeamMembers.ContainsKey(enemyTeam))
			return;
        Slider oneHpBar = null;
        Slider resistBar = null;
		foreach(Data_Center _one in _CharSetManager.TeamMembers[enemyTeam])
		{
			datacenterHPBarDic.TryGetValue(_one,out oneHpBar);
            if (oneHpBar != null)
                oneHpBar.gameObject.SetActive(true);
            switch (this.hPBarDisplayMode)
			{
				case HPBarDisplayMode.allEnemies:
					break;
				case HPBarDisplayMode.onlyNearEnemies:
					if (Vector3.Distance(_one.transform.position, focusingChar.gameObject.transform.position) > 11f)
                    {
                        if (oneHpBar != null)
						    oneHpBar.gameObject.SetActive(false);
						continue;
                    }
					break;                    
			}

            if (oneHpBar != null)
			{
                if (oneHpBar.transform.parent != _targetCanvas.transform)
                {
                    oneHpBar.transform.SetParent(_targetCanvas.transform);
                }
                oneHpBar.value = Mathf.Lerp(oneHpBar.value, (float)_one.getBOHealth()._health / (float)_one._playerBattleInfo.MaxHP,Time.deltaTime);
                oneHpBar.transform.localScale = Vector3.one;
                oneHpBar.gameObject.transform.position =
                    Vector3.Lerp(oneHpBar.gameObject.transform.position,
                                 CameraManager._camera.WorldToScreenPoint(_one.gameObject.transform.position + Vector3.up * 3.5f),
                                 Time.deltaTime * 20f);

				if (oneHpBar.value > 0)
				{
					oneHpBar.gameObject.SetActive(true);
				}else{
					oneHpBar.gameObject.SetActive(false);
				}
			}

            datacenterResistBarDic.TryGetValue(_one,out resistBar);
            if (resistBar != null)
            {
                resistBar.gameObject.SetActive(false);
                resistBar.value = Mathf.Lerp(resistBar.value, (float)_one.getBOHealth().Resistance / 10f, Time.deltaTime);//抵抗槽最大10格
                if (resistBar.value > 0)
                    resistBar.gameObject.SetActive(true);
                else
                    resistBar.gameObject.SetActive(false);
            }

            if (oneHpBar != null)
            {
                if (resistBar.transform.parent != oneHpBar.transform)
                    resistBar.transform.SetParent(oneHpBar.transform);
                resistBar.transform.position = Vector3.zero;
            }
        }
	}

	public void refresh()//这个刷新是倾向于画面制御
	{
        if (this.focusingChar.getRunner().playerMode)
        {
            _C_button.gameObject.SetActive(true);
            _AI_button.gameObject.SetActive(false);
        }
        else
        {
            _C_button.gameObject.SetActive(false);
            _AI_button.gameObject.SetActive(true);
        }
        UnityEngine.Events.UnityAction SwitchAUtoMOde = () =>
        {
            SwitchToCMode(this.focusingChar, !this.focusingChar.getRunner().playerMode);
            refresh();
        };//点角色icon是设置focusingChar，点icon旁边的C按钮才是进入控制
        autoBUtton.onClick.RemoveAllListeners();
        autoBUtton.onClick.AddListener(SwitchAUtoMOde);

		if (this.focusingChar != null)
		{
			controlleringChar.gameObject.SetActive(true);

            CharacterResourceInfo characterResourceInfo = CharsManager.getCharacterResourceInfo(this.focusingChar._CharacterDataInfo.resource_num);
            
            controlleringChar.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(this.focusingChar._CharacterDataInfo.resource_num),characterResourceInfo._zokusei);
			controllerHPbar.gameObject.SetActive(true);
			controllerEXbar.gameObject.SetActive(true);
            controllerResistbar.gameObject.SetActive(true);
        }else{
			controlleringChar.gameObject.SetActive(false);
			controllerHPbar.gameObject.SetActive(false);
			controllerEXbar.gameObject.SetActive(false);
            controllerResistbar.gameObject.SetActive(false);
        }

        SideCharIcon button;
        if (team1sideicons_on)
        {
            foreach (Data_Center team_member in _CharSetManager.TeamMembers[Team.player1])
            {
                datacenterCharIconDic.TryGetValue(team_member, out button);
                if (button)
                {
                    if (team_member == this.focusingChar)
                    {
                        button.gameObject.SetActive(false);
                        //控制中的角色不再需要在侧边icon显示
                        //button.transform.SetParent(null);//要讨论
                    }
                    else
                    {
                        button.gameObject.SetActive(true);
                        button.transform.SetParent(Team1Container);
                    }
                }
            }
        }

        if (team2sideicons_on)
        {
            foreach (Data_Center team in _CharSetManager.TeamMembers[Team.player2])
            {
                datacenterCharIconDic.TryGetValue(team, out button);
                if (button)
                {
                    if (team == this.focusingChar)
                    {
                        button.gameObject.SetActive(false);
                        //控制中的角色不再需要在侧边icon显示
                        //button.transform.SetParent(null);//要讨论
                    }
                    else
                    {
                        button.gameObject.SetActive(true);
                        button.transform.SetParent(Team2Container);
                    }
                }
            }
        }
	}
    
	public void SwitchToCMode(Data_Center _char,bool playerControll)//要转成控制模式的是哪个角色，如果括号里是null，意味着走向AI模式    
    {
        //先把之前在控制的给取消掉
        if (this.focusingChar != null)
            this.focusingChar.getRunner().playerMode = false;

        this.focusingChar = _char;
        if (this.focusingChar == null)
        {
            AIStateRunner._focusing = null;
            _mobileInputsManager.turnOffButtons();
            refresh();
        }
        else
        {
            AIStateRunner._focusing = this.focusingChar.getRunner();
            this.focusingChar.getRunner().playerMode = playerControll;
            _mobileInputsManager.focusCharInputs(this.focusingChar.getRunner().getInputManager(),_char.Zokusei);
            _mobileInputsManager.turnOnButtons();
            _CameraManager.Assign_Camera(Camera_Mode_Num.GodPlayerMode, new List<Transform>() { focusingChar.transform });
            refresh();
        }
    }

    public void SwitchToGodWatch()
    {
        if (this.focusingChar != null)
        {
            this.focusingChar.getRunner().playerMode = false;
        }

        this.focusingChar = null;
        AIStateRunner._focusing = null;

        _mobileInputsManager.focusCharInputs(null,zokusei.Null);
        _mobileInputsManager.turnOffButtons();
        _CameraManager.Assign_Camera(Camera_Mode_Num.GodWatchCamera);
        refresh();            
    }
        
    public void CurrentControllerHPEXShow(Data_Center _focusing)
    {
		if (controllerHPbar && _focusing && _focusing.getBOHealth() && _focusing._playerBattleInfo.MaxHP > 0)
		{
			controllerHPbar.value = (float)_focusing.getBOHealth()._health / (float)_focusing._playerBattleInfo.MaxHP;
            controllerEXbar.value = (float)_focusing.getBOHealth().CriticalGauge / 100f; //咱这个游戏满ex就是100，这个改的话也没什么意思
            controllerResistbar.value = (float)_focusing.getBOHealth().Resistance / 10f;
        }
    }

    public void Clear()// 这个我们还没有添加在合理的地方。
    {
        datacenterCharIconDic.Clear();
        datacenterHPBarDic.Clear();
        datacenterHitComboDic.Clear();
        datacenterResistBarDic.Clear();
    }

    public void instantiateCharsIconsAndFloatHPBar(List<Data_Center> Chars,RectTransform Container)//这个环节应该能够同时把HP bar也适配好。
	{
        if (Chars == null || Container == null)
        {
            Debug.Log("战斗中屏幕列表读取出错");
            return;
        }

        hpBarPrefab.gameObject.SetActive(false);
        resistBarPrefab.gameObject.SetActive(false);
        button_prefab.gameObject.SetActive(false);
        HitCombo.gameObject.SetActive(false);

        SideCharIcon _SideCharIcon;
		Slider floatHpBar;
        Slider resistBar;
        TextMeshProUGUI hitCombo;
        foreach (Data_Center a_char in Chars)
		{
			floatHpBar = Instantiate(hpBarPrefab);
            floatHpBar.name = a_char.name + "HPbar";

            resistBar = Instantiate(resistBarPrefab);
            resistBar.name = a_char.name + "ResistBar";

            hitCombo = Instantiate(HitCombo);
            hitCombo.name = a_char.name + "HitCombo";

            _SideCharIcon = Instantiate(button_prefab);
            _SideCharIcon.name = a_char.name + " ICon";
            if (_SideCharIcon.theHpBar == null)
            {
                Debug.Log("角色血条适配错误");
            }
            _SideCharIcon.iniHPShow(a_char);

            _SideCharIcon.focusingCharIcon.iconButton.onClick.RemoveAllListeners();
            UnityEngine.Events.UnityAction action1 = () =>
            {
                this.SetFocusingChar(a_char); 
            };//点角色icon是设置focusingChar，点icon旁边的C按钮才是进入控制
            _SideCharIcon.focusingCharIcon.iconButton.onClick.AddListener(action1);

            CharacterResourceInfo characterResourceInfo = CharsManager.getCharacterResourceInfo(a_char._CharacterDataInfo.resource_num);
            _SideCharIcon.focusingCharIcon.changeIcon(monsterIconsDic.Instance.getMonsterIconSyn(a_char._CharacterDataInfo.resource_num),characterResourceInfo._zokusei);

            _SideCharIcon.gameObject.SetActive(true);
            _SideCharIcon.transform.SetParent(Container.transform);
            _SideCharIcon.transform.localScale = Vector3.one;
			datacenterCharIconDic.Add(new KeyValuePair<Data_Center, SideCharIcon>(a_char, _SideCharIcon));

			floatHpBar.gameObject.SetActive(false);
            floatHpBar.transform.SetSiblingIndex(0);
            resistBar.gameObject.SetActive(false);
            resistBar.transform.SetSiblingIndex(1);
            datacenterHPBarDic.Add(new KeyValuePair<Data_Center,Slider>(a_char,floatHpBar));
            datacenterResistBarDic.Add(new KeyValuePair<Data_Center, Slider>(a_char, resistBar));
            datacenterHitComboDic.Add(new KeyValuePair<Data_Center, TextMeshProUGUI>(a_char, hitCombo));

            this._mobileInputsManager.zokuseiButtonRegister(a_char.Zokusei);
		}
	}
	
    //这个在进入瞬间由当前的auto与否决定是否直接切换控制或auto
    public void SetFocusingChar(Data_Center _Data_Center)
	{
        bool currentControlMode = false;
        if (this.focusingChar != _Data_Center) 
        {
            if (this.focusingChar != null)
            {
                currentControlMode = this.focusingChar.getRunner().playerMode;
                this.focusingChar.getRunner().playerMode = false;
            }
            this.focusingChar = _Data_Center;
        }

        if (this.focusingChar != null)
        {
            AIStateRunner._focusing = this.focusingChar.getRunner();
            SwitchToCMode(this.focusingChar, currentControlMode);//继承当前模式：AI？玩家控制？
        }
        else
        {
            AIStateRunner._focusing = null;
        }

        refresh();
    }//这个method涉及到很多地方。。。其实还牵扯到一个控制角色的问题。
       
    public Data_Center GetFocusingChar()
	{
		return this.focusingChar;
	}

    public void removeAllUIElement(Transform t)
	{
        Destroy(t.gameObject.GetComponent<Text>());
        Destroy(t.gameObject.GetComponent<Image>());
        Destroy(t.gameObject.GetComponent<Button>());
    }

	void Awake()
	{
	}
		
	// Use this for initialization
	void Start()
	{
        button_prefab.gameObject.SetActive(false);
		controlleringChar.gameObject.SetActive(false);
        controllerHPbar.gameObject.SetActive(false);
        controllerEXbar.gameObject.SetActive(false);
        controllerResistbar.gameObject.SetActive(false);
    }    
}
