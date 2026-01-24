namespace pdp11_emulator.Executing;
using Components;
using Signaling;

public partial class DataPath
{
    private MmuOutput PhysicalAddress(ushort virtualAddress, SegmentData data, UniBusDriving operation)
    {
        uint offset = (uint)(virtualAddress & 0x1FFF);
        
        return new MmuOutput(data.BaseAddress + offset, Validate(data, operation, offset));
    }

    private TrapVector Validate(SegmentData data, UniBusDriving operation, uint offset)
    {
        if (!data.Valid)
            return TrapVector.INVALID_SEGMENT;

        if (!data.Writable && operation is UniBusDriving.WRITE_WORD or UniBusDriving.WRITE_BYTE)
            return TrapVector.WRITE_VIOLATION;

        uint size = operation switch
        {
            UniBusDriving.READ_WORD or UniBusDriving.WRITE_WORD => 2,
            UniBusDriving.READ_BYTE or UniBusDriving.WRITE_BYTE => 1
        };
        
        if (!data.ExpandDown)
        {
            if (offset + size - 1 >= data.LengthByte)
                return TrapVector.LENGTH_VIOLATION;
        }
        else
        {
            if(offset < data.LengthByte)
                return TrapVector.LENGTH_VIOLATION;
        }

        return TrapVector.NONE;
    }
}

public struct MmuOutput(uint physicalAddress, TrapVector trap)
{
    public readonly uint PhysicalAddress = physicalAddress;
    public readonly TrapVector Trap = trap;
}
