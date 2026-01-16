namespace pdp11_emulator;
using Core.Executing.Components;

public class Pdp11
{
    private readonly UniBus UniBus = new ();
    
    // REQUESTERS
    private readonly Cpu Cpu = new ();
    
    // RESPONDERS
    private readonly Ram Ram = new ();
    
    private bool HALT;
    
    public void Power() => Clock();

    private void Clock()
    {
        Cpu.Init();
        
        while (!HALT)
        {
            Tick();
            
            Thread.Sleep(25);
        }
    }
    
    private void Tick()
    {
        // REQUESTERS
        Cpu.Tick(UniBus);
        
        UniBus.Arbitrate();

        // RESPONDERS
        Ram.Respond(UniBus);
    }
}