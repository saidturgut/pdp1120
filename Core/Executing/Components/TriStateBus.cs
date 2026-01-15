namespace pdp11_emulator.Core.Executing.Components;

public class TriStateBus
{
    private byte value;
    private bool driven;

    public void Clear()
    {
        value = 0;
        driven = false;
    }
    
    public void Set(byte input)
    {
        if (driven)
            throw new Exception("BUS CONTENTION");
        
        driven = true;
        value = input;
    }

    public byte Get() => value;
}