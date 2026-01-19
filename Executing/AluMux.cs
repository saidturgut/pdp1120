namespace pdp11_emulator.Executing;
using Signaling;
using Computing;
using Components;

public partial class DataPath
{
    private Alu Alu = new();
    
    public void AluAction(TriStateBus cpuBus, TriStateBus aluBus)
    {
        if(signals.AluAction is null)
            return;
        
        AluAction action = signals.AluAction.Value;
        
        AluOutput output = Alu.Compute(new AluInput
        {
            Operation = action.AluOperation,
            
            A = cpuBus.Get(),
            
            B = action.RegisterOperand != Register.NONE
                ? Access(action.RegisterOperand).Get() : (ushort)2,
            
            C = (Access(Register.PSW).Get() & (ushort)AluFlag.Carry) != 0,
        });

        aluBus.Set(output.Result);

        Access(Register.PSW).Set((ushort)
            ((Access(Register.PSW).Get() & (ushort)~action.FlagMask) | (output.Flags & (ushort)action.FlagMask)));
    }
}