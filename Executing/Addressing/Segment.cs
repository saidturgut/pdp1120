namespace pdp11_emulator.Executing.Addressing;
using Components;

public class SegmentSet
{
    public readonly Segment[] InstructionSegment = new Segment[8];
    public readonly Segment[] DataSegment = new Segment[8];

    public void Init()
    {
        for (int i = 0; i < 8; i++)
        {
            InstructionSegment[i] = new Segment();
            DataSegment[i] = new Segment();
        }
    }

    public void Commit(bool abort)
    {
        for (int i = 0; i < 8; i++)
        {
            InstructionSegment[i].Commit(abort);
            DataSegment[i].Commit(abort);
        }
    }
}

public class Segment
{
    private RegisterObject Par;
    private RegisterObject Pdr;
    
    public uint baseAddress;
    public bool valid;
    public bool writable;
    public bool expandDown;
    public ushort length;
    
    public void Update()
    {
        baseAddress = (uint)(Par.Get() << 6);
        
        ushort pdr = Pdr.Get();
        valid = (pdr & 0x1000) != 0;
        writable = (pdr & 0x4000) != 0;
        expandDown = (pdr & 0x2000) != 0;

        length = (ushort)((pdr >> 6) & 0x3F);
    }
    
    public void Commit(bool abort)
    {
        if (!abort)
        {
            Pdr.Commit();
            Par.Commit();
        }
        
        Pdr.Init();
        Par.Init();
    }
}

public class SegmentStatus
{
}

