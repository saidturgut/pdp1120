namespace pdp11_emulator.Core.Signaling;
using Decoding;
using Cycles;

public class MicroUnit : MicroUnitROM
{
    public Decoder Decoder = new();
    
    public MicroCycle currentCycle;

    public void Emit()
    {
    }

    public void Decode(ushort IR)
        => decoded = Decoder.Decode(IR);
}
