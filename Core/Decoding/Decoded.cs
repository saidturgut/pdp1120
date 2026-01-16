namespace pdp11_emulator.Core.Decoding;
using Signaling.Cycles;
using Signaling;

public struct Decoded()
{
    public RegisterAction[] Registers = [];
    
    public List<MicroCycle> MicroCycles 
        = [MicroCycle.FETCH_MAR, MicroCycle.PC_INC, MicroCycle.FETCH_MDR, MicroCycle.DECODE];
}