namespace pdp11_emulator.Executing;
using Addressing;
using Signaling;
using Arbitrating;

public partial class DataPath
{
    private readonly Mmu Mmu = new();
    
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
    
    public void UniBusDrive(UniBus uniBus, TrapUnit trapUnit, bool fetch)
    {
        if(Signals.UniBusDriving is UniBusDriving.NONE)
            return;

        MmuOutput mmuOutput = Mmu.Address(Access(Register.MAR).Get(), Psw.CMOD, Signals.UniBusDriving, fetch);

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
}