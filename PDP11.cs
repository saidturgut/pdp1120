namespace pdp11_emulator;
using Core.Executing.Components;

public class PDP11
{
    private readonly TriStateBus UNIBUS = new ();
    
    // MASTERS
    private readonly CPU CPU = new ();
    
    // SLAVES
    private readonly RAM RAM = new ();
    
    private bool HALT = false;
    
    public void Power() => Clock();

    private void Clock()
    {
        CPU.Init();
        
        while (!HALT)
        {
            Tick();
        }
    }
    
    private void Tick()
    {
        CPU.Tick(UNIBUS);
    }
}