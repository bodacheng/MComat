using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input
{
    public Inputs_defined input_num;
    public string unity_axis_name;
    public bool input_state;

    public bool button_down;

    public Input()
    {
    }

    public Input(Inputs_defined input_num,string unity_axis_name)
    {
        this.input_num = input_num;
        this.unity_axis_name = unity_axis_name;
        input_state = false;
        button_down = false;
    }

    public virtual void UpdateInputState()
    {
    }

    public virtual bool CheckInputState()
    {
        return input_state;
    }
}

public class ButtonTouchedTypeInput : Input
{
    readonly int frame_limit;
    int frame_counter;

    public ButtonTouchedTypeInput(Inputs_defined input_num, string unity_axis_name) : base(input_num, unity_axis_name)
    {
        frame_limit = -1;
    }

    public ButtonTouchedTypeInput(Inputs_defined input_num, string unity_axis_name, int frame_limit)
    {
        this.input_num = input_num;
        this.unity_axis_name = unity_axis_name;
        this.frame_limit = frame_limit;
        frame_limit = (int)Mathf.Clamp(frame_limit, 0, Mathf.Infinity);
        input_state = false;
        button_down = false;
    }

    public override void UpdateInputState()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (frame_limit == -1)
            {
                input_state = ETCInput.GetButtonDown(unity_axis_name);
            }              
            else
            {
                if (ETCInput.GetButtonDown(unity_axis_name))
                {
                    frame_counter = 0;
                    input_state = false;
                }
                if (ETCInput.GetButton(unity_axis_name))
                {
                    frame_counter++;
                }
                if (ETCInput.GetButtonUp(unity_axis_name))
                {
                    input_state |= frame_counter <= frame_limit;
                    frame_counter = 0;
                }
            }
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (frame_limit == -1)
                input_state = UnityEngine.Input.GetButtonDown(unity_axis_name);
            else
            {
                if (UnityEngine.Input.GetButtonDown(unity_axis_name))
                {
                    frame_counter = 0;
                    this.input_state = false;
                }
                if (UnityEngine.Input.GetButton(unity_axis_name))
                {
                    frame_counter++;
                }
                if (UnityEngine.Input.GetButtonUp(unity_axis_name))
                {
                    this.input_state |= frame_counter <= frame_limit;
                    frame_counter = 0;
                }
            }
        }

        this.button_down = this.input_state ? true : false;
    }

    public override bool CheckInputState()
    {
        if (input_state)
        {
            input_state = false;
            return true;
        }else{
            return false;
        }
    }
}

public class ButtonDownTypeInput:Input
{
    public ButtonDownTypeInput(Inputs_defined input_num, string unity_axis_name) : base(input_num, unity_axis_name)
    {
        //this.frame_limit = -1;
    }

    public override void UpdateInputState()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (unity_axis_name == "Any")
                input_state = UnityEngine.Input.GetButton("Attack") || 
                                UnityEngine.Input.GetButton("Fire1") || 
                                    UnityEngine.Input.GetButton("Fire2") || 
                                        UnityEngine.Input.GetButton("Rush") ||
                                            UnityEngine.Input.GetButton("Defend");
            //if (frame_limit == -1)
            //{
            //    this.input_state = ETCInput.GetButton(unity_axis_name);
            //}
            //else
            //{
            //    if (ETCInput.GetButtonDown(unity_axis_name))
            //    {
            //        frame_counter = 0;
            //        this.input_state = false;
            //    }
            //    if (ETCInput.GetButton(unity_axis_name))
            //    {
            //        frame_counter++;
            //    }
            //    if (ETCInput.GetButtonUp(unity_axis_name))
            //    {
            //        this.input_state = false;
            //        frame_counter = 0;
            //    }
            //    if (frame_counter > frame_limit)
            //    {
            //        this.input_state = true;
            //    }
            //}
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            
            if (unity_axis_name == "Any")
                input_state = UnityEngine.Input.GetButton("Attack") || 
                                UnityEngine.Input.GetButton("Fire1") || 
                                    UnityEngine.Input.GetButton("Fire2") || 
                                        UnityEngine.Input.GetButton("Rush") ||
                                            UnityEngine.Input.GetButton("Defend");
            else
                input_state = UnityEngine.Input.GetButton(unity_axis_name);//电脑测试的话就把这个打开。
            
            //if (frame_limit == -1)
            //{
            //    this.input_state = Input.GetButton(unity_axis_name);
            //}else{
            //    if (Input.GetButtonDown(unity_axis_name))
            //    {
            //        frame_counter = 0;
            //        this.input_state = false;
            //    }
            //    if (Input.GetButton(unity_axis_name))
            //    {
            //        frame_counter++;
            //    }
            //    if (Input.GetButtonUp(unity_axis_name))
            //    {
            //        this.input_state = false;
            //        frame_counter = 0;
            //    }
            //    if (frame_counter > frame_limit)
            //    {
            //        this.input_state = true;
            //    }
            //}
        }
        button_down = this.input_state;
    }

    public override bool CheckInputState()
    {
        return input_state;
    }
}

public class ButtonOffTypeInput : Input
{
    public ButtonOffTypeInput(Inputs_defined input_num, string unity_axis_name) : base(input_num, unity_axis_name)
    {
    }

    public override void UpdateInputState()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            this.input_state = !UnityEngine.Input.GetButton(unity_axis_name);
        }
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //this.input_state = !ETCInput.GetButton(unity_axis_name);
        }

        this.button_down = false;
    }

    public override bool CheckInputState()
    {
        return input_state;
    }
}
