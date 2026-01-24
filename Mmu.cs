using pdp11_emulator.Arbitrating;

namespace pdp11_emulator;
using Executing.Components;
using Signaling;

public class Mmu
{
    private bool ENABLED;
    
    private readonly RegisterObject[] MmuRegisters = new RegisterObject[64];
    
    public void Init(bool enable)
    {
        ENABLED = enable;
        for (int i = 0; i < 64; i++) MmuRegisters[i] = new();
    }

    public SegmentData ReturnSegment(byte segmentIndex)
        => new(MmuRegisters[segmentIndex].Get(), MmuRegisters[segmentIndex + 8].Get());
    
    public void Respond(UniBus uniBus)
    {
        uint address = uniBus.GetAddress();
        
        if(!uniBus.respondPermit || address < 0xFF00 || address > 0xFF7F)
            return;

        uint registerIndex = (ushort)((address - 0xF4A0) >> 1);
        
        switch (uniBus.Operation)
        {
            case UniBusDriving.READ_WORD:
            case UniBusDriving.READ_BYTE: 
                uniBus.SetData(Read(registerIndex)); break;
            case UniBusDriving.WRITE_WORD:
            case UniBusDriving.WRITE_BYTE:
                Write(registerIndex, uniBus.GetData()); break;
        } 
    }

    private ushort Read(uint address)
        => MmuRegisters[address].Get();

    private void Write(uint address, ushort data)
        => MmuRegisters[address].Set(data);
    
    public void Commit(bool abort)
    {
        if(!ENABLED)  return;
        
        for (int i = 0; i < 64; i++) MmuRegisters[i].Commit(abort);
    }
}

public struct SegmentData(ushort par,  ushort pdr)
{
    public readonly uint BaseAddress = (uint)(par << 6);
    public readonly bool Valid = (pdr & 0x1000) != 0;
    public readonly bool Writable = (pdr & 0x4000) != 0;
    public readonly bool ExpandDown = (pdr & 0x2000) != 0;
    public readonly ushort LengthByte  = (ushort)((pdr >> 6) & 0x3F);
}