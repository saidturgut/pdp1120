namespace pdp11_emulator.Executing.Computing;
using Signaling;

public class Alu : AluRom
{
    public AluOutput Compute(AluInput input)
    {
        AluOutput output = Operations[(ushort)input.Operation](input);
        
        if((output.Result & 0x8000) != 0) 
            output.Flags |= (ushort)AluFlag.Negative;
        if(output.Result == 0) 
            output.Flags |= (ushort)AluFlag.Zero;

        return output;
    }
}
