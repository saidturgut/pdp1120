namespace pdp11_emulator.Arbitrating;
using Signaling;

public struct DataRequest
{
    public byte Requester;
    public uint Address;
    public ushort Data;
    public UniBusDriving Operation;
}

public struct InterruptRequest
{
    public TrapVector Vector;
    public byte Priority;
}

public enum Requester
{
    NONE,
    CPU, 
}
