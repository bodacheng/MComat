using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BO_Weapon_Animation_Events))]
[RequireComponent(typeof(ResistanceManager))]
[RequireComponent(typeof(BO_Ani_E))]
[RequireComponent(typeof(Pusher))]
[RequireComponent(typeof(SkillCancelFlag))]
public class OutsideDataLink : MonoBehaviour
{
    public Data_Center _C;
}
