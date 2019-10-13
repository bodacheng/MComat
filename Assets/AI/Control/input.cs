using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input
{
    public inputs_defined input_num;
    public string unity_axis_name;
    public bool input_state;

    public bool button_down;

    public input()
    {
    }

    public input(inputs_defined input_num,string unity_axis_name)
    {
        this.input_num = input_num;
        this.unity_axis_name = unity_axis_name;
        this.input_state = false;
        this.button_down = false;
    }

    public virtual void updateInputState()
    {
    }

    public virtual bool checkInputState()
    {
        return input_state;
    }
}

public class buttonTouchedTypeInput : input
{
    int frame_limit,frame_counter;

    public buttonTouchedTypeInput(inputs_defined input_num, string unity_axis_name) : base(input_num, unity_axis_name)
    {
        frame_limit = -1;
    }

    public buttonTouchedTypeInput(inputs_defined input_num, string unity_axis_name, int frame_limit)
    {
        this.input_num = input_num;
        this.unity_axis_name = unity_axis_name;
        this.frame_limit = frame_limit;
        frame_limit = (int)Mathf.Clamp(frame_limit, 0, Mathf.Infinity);
        this.input_state = false;
        this.button_down = false;
    }

    public override void updateInputState()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (frame_limit == -1)
            {
                this.input_state = ETCInput.GetButtonDown(unity_axis_name);
            }              
            else
            {
                if (ETCInput.GetButtonDown(unity_axis_name))
                {
                    frame_counter = 0;
                    this.input_state = false;
                }
                if (ETCInput.GetButton(unity_axis_name))
                {
                    frame_counter++;
                }
                if (ETCInput.GetButtonUp(unity_axis_name))
                {
                    if (frame_counter <= frame_limit)
                    {
                        this.input_state = true;
                    }
                    frame_counter = 0;
                }
            }
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (frame_limit == -1)
                this.input_state = Input.GetButtonDown(unity_axis_name);
            else
            {
                if (Input.GetButtonDown(unity_axis_name))
                {
                    frame_counter = 0;
                    this.input_state = false;
                }
                if (Input.GetButton(unity_axis_name))
                {
                    frame_counter++;
                }
                if (Input.GetButtonUp(unity_axis_name))
                {
                    if (frame_counter <= frame_limit)
                    {
                        this.input_state = true;
                    }
                    frame_counter = 0;
                }
            }
        }

        if (this.input_state)
            this.button_down = true;
        else
            this.button_down = false;
    }

    public override bool checkInputState()
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

public class buttonDownTypeInput:input
{
    public buttonDownTypeInput(inputs_defined input_num, string unity_axis_name) : base(input_num, unity_axis_name)
    {
        //this.frame_limit = -1;
    }

    public override void updateInputState()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
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
            this.input_state = Input.GetButton(unity_axis_name);//电脑测试的话就把这个打开。

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

        if (this.input_state)
            this.button_down = true;
        else
            this.button_down = false;
    }

    public override bool checkInputState()
    {
        return input_state;
    }
}

public class buttonOffTypeInput : input
{
    public buttonOffTypeInput(inputs_defined input_num, string unity_axis_name) : base(input_num, unity_axis_name)
    {
    }

    public override void updateInputState()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor
            || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
        {
            this.input_state = !Input.GetButton(unity_axis_name);
        }
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //this.input_state = !ETCInput.GetButton(unity_axis_name);
        }

        this.button_down = false;
    }

    public override bool checkInputState()
    {
        return input_state;
    }
}
