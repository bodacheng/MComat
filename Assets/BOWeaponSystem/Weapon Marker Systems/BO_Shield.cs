using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EZObjectPools;

public class BO_Shield : MonoBehaviour {

	//there are animation events on Enable shield collider and disable shield collider. Info them out!
	[Space(10)]
	[Tooltip("盾牌单次寿命")]
	public int _HP = 50;

    [Tooltip("An option for more accurate shield detection on Humanoid targets. If in the same frame both a Shield and an Enemy were hit (which may happen at REALY high speeds), ticking this option True will provide additional check to see what was closer to the Attack orign - the shield or the Enemy. It's a good idea to have it turned ON by default, but it's not mandatory. The only situations when this option is not adviced is when your shield wielder is enormously big. Like, a building-big. (though it's not a rule and it may still work fine).")]
    public bool _AdvancedShieldDetection = true;

	[Tooltip("Before we start, if you plan on using this Shield with a character or object equiped with the BS_Main_Health system, please, assign it here. Otherwise the hit detection may not work properly")]
    public BO_Health _ParentHealth; //it's referenced in other scripts.

	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. It is a point behind the center of the shield (from the safe side, behind the shield's collider (so it's simply a point near the center of the Shield's Wielder, if it's humantoid, for scale). It's used to calculate if the attack was coming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield and place it accordingly around the shield. Then put it's reference into this variable. [INFO 2:] If you're experiencing that the shild is being hit though the attack was clearly coming from the back (like if the shield is quite big or the attack swipe is large), don't be affraid to pull this spot a bit further behind the shield wielder")]
	public Transform _ShieldBackSpot;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. It is a point around the very center of the shield (can be a tiny bit in front of the shield's collider (so from the Attacker side). It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldCenterSpot;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot1;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot2;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot3;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot4;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot5;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot6;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot7;
	[Tooltip("This fancy named GameObject is used in Advanced Shield Detection. These edge spots should be placed around the edges of your shields - will it be a simple rectangular box or any custom shape, these spots should entwine the collider. It's used to calculate if the attack was comming from the front or the back of the shield. [INFO:] Simply create an empty GameObject, set it as a child of your shield (or even better - the character that has this shield!) and place it accordingly around the shield or the character. Then put it's reference into this variable. If your Shield is not a square (so it has any custom shape), don't be affraid to place these Spots around it's edges in any different pattern - it will still work!")]
	public Transform _ShieldEdgeSpot8;

	[Space(10)]
	[Tooltip("With an Animation Event from the BS_Weapon_Animation_Events system you can call a function to turn on and off the Shields colliders. You can use the DisableShieldCollider() and EnableShieldCollider() Animation Events of the earlier mentioned system! Read more about it in the ReadMe File.")]
	public Collider _shieldCollider;

	[Tooltip("Disable shield when dying?")]
	public bool DisableShieldOnDeath;

    [Tooltip("属性")]
    public zokusei zokusei;

    [Tooltip("盾牌伤害类型")]
    public damageType damage_type = damageType.normal_shield;

    private int _hpCounter = 0;
    private EZObjectPool _hitSparks,shieldBreakSpark;
    private string personalEffectPath;

    void Awake()
    {
        switch (zokusei)
        {
            case zokusei.darkMagic:
                personalEffectPath = "darkMagic";
                break;
            case zokusei.blueMagic:
                personalEffectPath = "blueMagic";
                break;
            case zokusei.greenMagic:
                personalEffectPath = "greenMagic";
                break;
            case zokusei.lightMagic:
                personalEffectPath = "lightMagic";
                break;
            case zokusei.redMagic:
                personalEffectPath = "redMagic";
                break;
            default:
                personalEffectPath = "defaultEffects";
                break;
        }
    }

    public void plusHP(int plus)
    {
        _hpCounter += plus;
    }

    void Update()
    {
        if (_hpCounter < 0)
        {
            shieldBreak();
        }    
    }

    private GameObject shieldbreaking;
    private void shieldBreak()
    {
        if (this._ShieldCenterSpot != null)
        {
            if (shieldBreakSpark == null)
            {
                shieldBreakSpark = defaultPools.Instance.iniEffectsPool("onEnableShieldSpark", personalEffectPath, 3);
            }
            if (shieldBreakSpark != null)
            {
                shieldBreakSpark.TryGetNextObject(this._ShieldCenterSpot.position, Quaternion.identity, out shieldbreaking);
                shieldbreaking.transform.LookAt(_ShieldCenterSpot.position - _ShieldBackSpot.position);
            }
        }
        if (_ParentHealth != null)
        {
            _ParentHealth.ApplyDamage(new v_Damage(0, damageType.heavy_damage, Vector3.zero, this._ShieldCenterSpot.position, _ParentHealth));
        }
    }

    public void iniShield(TeamConfig _TeamConfig,BO_Health bO_Health)
    {
        if (_TeamConfig !=null)
            this.gameObject.layer = _TeamConfig.myShieldLayer;
        this._ParentHealth = bO_Health;
        bO_Health.setShield(this);
    }

    private GameObject _missSparksShield;
    public void passHitPointsFromWeaponToShiled(List<Vector3> _ShiledHitPositions)
    {
        if (_hitSparks == null)
            _hitSparks = defaultPools.Instance.iniEffectsPool("shield_hit", personalEffectPath, 3);
        
        if (_hitSparks != null)
        {
            for (int i3 = 0; i3 < _ShiledHitPositions.Count; i3++)
            {
                _missSparksShield = _hitSparks.TryGetNextObject(_ShiledHitPositions[i3], Quaternion.LookRotation(_ShiledHitPositions[i3] - _ShieldBackSpot.position));
                //_missSparksShield.transform.LookAt(2 * _missSparksShield.transform.position - _ShieldBackSpot.position);
            }
        }
    }

	void OnEnable()
	{
        this._hpCounter = _HP;
    }
}
