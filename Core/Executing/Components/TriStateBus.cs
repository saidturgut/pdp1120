namespace pdp11_emulator.Core.Executing.Components;

public class TriStateBus
{
    private ushort value;

    public void Clear()
        => value = 0;
    
    public void Set(ushort input) 
        => value = input;

    public ushort Get() 
        => value;
}
