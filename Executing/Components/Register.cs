namespace pdp11_emulator.Executing.Components;

public class RegisterObject
{
    private ushort committed;
    private ushort value;
    
    public void Init(ushort input)
    {
        value = input;
        committed = value;
    }
    
    public void Set(ushort input)
        => value = input;

    public ushort Get() 
        => value;
    
    public void Commit(bool abort)
    {
        if (!abort)
        {
            committed = value;
        }
        
        value = committed;
    }
}