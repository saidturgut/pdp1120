using System.Transactions;

namespace pdp11_emulator;
using Executing.Components;
using Arbitrating.Memory;
using Arbitrating;
using Signaling;

public class Pdp11
{
    private readonly UniBus UniBus = new ();
    private readonly TrapUnit TrapUnit = new();

    private readonly Kd11 Kd11 = new ();
    
    private readonly Mmu Mmu = new();
    private readonly Rom Rom = new ();
    private readonly Ram Ram = new ();
    
    private bool HALT;

    private int interrupter;
    
    public void Power() => Clock();

    private void Clock()
    {
        Rom.Boot(Ram);
        Mmu.Init(true);
        
        Kd11.Init();
        
        while (!HALT)
        {
            //interrupter++;
            
            Tick();
            
            Thread.Sleep(100);
        }
    }
    
    private void Tick()
    {
        UniBus.Clear();
        
        // TERMINAL REQUESTS INTERRUPT
        // DISK REQUESTS INTERRUPT

        if (interrupter == 20)// FOR DEBUGGING
        {
            UniBus.RequestInterrupt(new InterruptRequest()
            {
                Vector = TrapVector.IOT,
                Priority = 2,
            });
            
            UniBus.RequestInterrupt(new InterruptRequest()
            {
                Vector = TrapVector.BUS_ERROR,
                Priority = 6,
            });
        }

        // REQUESTERS
        Kd11.Tick(UniBus, Mmu, TrapUnit);
        
        UniBus.ArbitrateData();

        // RESPONDERS
        Mmu.Respond(UniBus);
        Ram.Respond(UniBus, TrapUnit);
        
        HALT = Kd11.HALT;

        if (Kd11.COMMIT) Commit();
    }

    private void Commit()
    {
        Mmu.Commit(TrapUnit.ABORT);
        Ram.Commit(TrapUnit.ABORT);

        Kd11.COMMIT = false;
    }
}