using Soul;

public class Controlled_State : Behavior {

    string clip_name;

    public Controlled_State(string clip_name)
	{
		this.clip_name = clip_name;
    }

	public override void Pre_process_before_enter()
	{
		base.Pre_process_before_enter ();
	}

	public override bool Force_enter_condition()
	{
		return true;
	}

	public override void AI_State_enter()
	{
        base.AI_State_enter();
        this.personality_Events.CloseAllPersonalityEffects();
		_Rigidbody.useGravity = false;
		Animation_Manger.AnimationTrigger(clip_name);
    }

	public override bool Capacity_Exit_Condition()//如果受伤动画播放完的话状态就退出，如果时间过了规定的晕眩时间，也退出。
	{
        return false;
	}
}
