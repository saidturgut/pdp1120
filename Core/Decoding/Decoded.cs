namespace pdp11_emulator.Core.Decoding;
using Signaling.Cycles;

public class Decoded
{
    public RegisterAction driver;
    
    public List<MicroCycle> MicroCycles 
        = [MicroCycle.FETCH_MAR, MicroCycle.FETCH_INC, MicroCycle.FETCH_MDR];
}