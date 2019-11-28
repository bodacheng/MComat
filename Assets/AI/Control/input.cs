public class Input
{
    public Inputs_defined input_num;
    public bool input_state;

    public Input()
    {
    }

    public Input(Inputs_defined input_num)
    {
        this.input_num = input_num;
        input_state = false;
    }

    public virtual bool CheckInputState()
    {
        return input_state;
    }
}