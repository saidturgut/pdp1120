namespace pdp11_emulator.Arbitrating;
using Signaling;

public partial class UniBus
{    
    public UniBusDriving Operation {get; private set;}
    
    private uint address;
    private ushort data;
    
    public bool respondPermit { get; private set; }

    public void Clear()
    {
        respondPermit = false;
    }
    
    public uint GetAddress()
        => address;

    public void SetData(ushort input)
        => data = input;

    public ushort GetData() 
        => data;
}