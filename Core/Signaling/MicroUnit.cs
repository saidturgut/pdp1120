namespace pdp11_emulator.Core.Signaling;
using Decoding;
using Cycles;

public class MicroUnit : MicroUnitRom
{
    private readonly Decoder Decoder = new();
    
    private ushort currentCycle;
    
    private bool INTERRUPT;
    private bool TRAPS;

    public SignalSet Emit(ushort ir)
    {
        if (INTERRUPT)
        {
            return new SignalSet();
        }

        if (decoded.MicroCycles[currentCycle] is MicroCycle.DECODE)
        {
            decoded = Decoder.Decode(ir);
            return new SignalSet();
        }

        return MicroCycles[(int)decoded.MicroCycles[currentCycle]]();
    }
    
    public void Advance()
    {
        if (INTERRUPT)
        {
            return;
        }

        if (currentCycle == decoded.MicroCycles.Count - 1)
        {
            currentCycle = 0;
        }
        else
        {
            currentCycle++;
        }
    }
}