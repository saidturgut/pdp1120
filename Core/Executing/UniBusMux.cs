namespace pdp11_emulator.Core.Executing;
using Signaling.Cycles;
using Signaling;
using Components;

public partial class DataPath
{
    public void UniBusLatch(UniBus uniBus)
    {
        if(!signals.UniBusLatch)
            return;

        if (uniBus.requesters[(ushort)requesterIndex] != null)
        {
            STALL = true;
            return;
        }

        STALL = false;
        Access(RegisterAction.MDR).Set(uniBus.GetData());
    }
    
    public void UniBusDrive(UniBus uniBus)
    {
        if(signals.UniBusDrive is UniBusAction.NONE)
            return;

        Request request = new Request
        {
            Requester = (byte)requesterIndex,
            Address = Access(RegisterAction.MAR).Get(),
        };

        if (signals.UniBusDrive == UniBusAction.WRITE)
            request.Data = Access(RegisterAction.TMP).Get();
        
        uniBus.Request(request);
    }
}