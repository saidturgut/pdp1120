namespace pdp11_emulator.Executing;
using Components;
using Signaling;
using Arbitrating;

public partial class DataPath
{
    private const Requester requesterType = Requester.CPU;
    
    public bool STALL { get; private set; }

    public void UniBusLatch(UniBus uniBus)
    {
        if(!Signals.UniBusLatching)
            return;
        
        if (uniBus.DataRequests[(ushort)requesterType] != null)
        {
            STALL = true;
            return;
        }

        STALL = false;
        
        Access(Register.MDR).Set(uniBus.GetData());
    }
    
    public void UniBusDrive(UniBus uniBus, Mmu mmu, TrapUnit trapUnit, bool fetch)
    {
        if(Signals.UniBusDriving is UniBusDriving.NONE)
            return;

        // VIRTUAL ADDRESS TO PHYSICAL ADDRESS
        ushort virtualAddress = Access(Register.MAR).Get();
        
        SegmentData segmentData = mmu.ReturnSegment(CalculateSegment(fetch, Psw.CMOD, virtualAddress));
        
        MmuOutput mmuOutput = PhysicalAddress(virtualAddress, segmentData, Signals.UniBusDriving);

        if (mmuOutput.Trap != TrapVector.NONE)
        {
            trapUnit.Request(mmuOutput.Trap); 
            return;
        }
        
        uniBus.RequestData(new DataRequest
        {
            Requester = (byte)requesterType,
            
            Address = mmuOutput.PhysicalAddress,
           
            Data = Access(Register.TMP).Get(),
            Operation = Signals.UniBusDriving,
        });
    }

    private byte CalculateSegment(bool fetch, Mode mode, ushort virtualAddress)
    {
        byte segmentIndex = 0;
        if (!fetch) segmentIndex++;
        if (mode == Mode.KERNEL) segmentIndex += 2;
        segmentIndex *= 16;
        segmentIndex += (byte)((virtualAddress >> 13) & 0b111);
        return segmentIndex;
    }
}