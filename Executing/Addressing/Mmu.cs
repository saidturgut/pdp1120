namespace pdp11_emulator.Executing.Addressing;
using Components;
using Signaling;
using Utility;
using Arbitrating;

public struct MmuOutput(uint physicalAddress, TrapVector trap)
{
    public uint PhysicalAddress;
    public TrapVector Trap;
}

public class Mmu
{
    private bool ENABLED;

    private readonly SegmentSet KernelSegmentSet = new ();
    private readonly SegmentSet UserSegmentSet = new ();

    public void Init(bool enable)
    {
        ENABLED = enable;
        KernelSegmentSet.Init();
        UserSegmentSet.Init();
    }
    
    public MmuOutput Address(ushort virtualAddress, Mode mode, UniBusDriving operation, bool fetch)
    {
        if (!ENABLED) return new MmuOutput(0, TrapVector.NONE);
        
        return Translate(virtualAddress, mode is Mode.KERNEL ? KernelSegmentSet : UserSegmentSet, operation, fetch);
    }

    private MmuOutput Translate(ushort virtualAddress, SegmentSet segmentSet, UniBusDriving operation, bool fetch)
    {
        uint offset = (uint)(virtualAddress & 0x1FFF);

        byte segmentIndex = (byte)((virtualAddress >> 13) & 0b111);
        
        Segment segment = (fetch ? segmentSet.InstructionSegment : segmentSet.DataSegment)[segmentIndex];
        segment.Update();
        
        return new MmuOutput(segment.baseAddress + offset, Validate(segment, operation, offset));
    }

    private TrapVector Validate(Segment segment, UniBusDriving operation, uint offset)
    {
        if (!segment.valid)
            return TrapVector.INVALID_SEGMENT;

        if (!segment.writable && operation is UniBusDriving.WRITE_WORD or UniBusDriving.WRITE_BYTE)
            return TrapVector.WRITE_VIOLATION;

        uint size = operation switch
        {
            UniBusDriving.READ_WORD or UniBusDriving.WRITE_WORD => 2,
            UniBusDriving.READ_BYTE or UniBusDriving.WRITE_BYTE => 1
        };
        
        if (!segment.expandDown)
        {
            if (offset + size - 1 >= segment.length)
                return TrapVector.LENGTH_VIOLATION;
        }
        else
        {
            if(offset < segment.length)
                return TrapVector.LENGTH_VIOLATION;
        }

        return TrapVector.NONE;
    }

    public void Commit(bool abort)
    {
        if(!ENABLED)  return;
        
        KernelSegmentSet.Commit(abort);
        UserSegmentSet.Commit(abort);
    }
}