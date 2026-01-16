namespace pdp11_emulator.Core.Signaling;

public struct SignalSet()
{
    public bool UniBusLatch = false; // MDR
    public RegisterAction CpuBusDriver = RegisterAction.NONE;
    public AluAction? AluAction = null;
    public RegisterAction CpuBusLatcher = RegisterAction.NONE;
    public UniBusAction UniBusDrive = UniBusAction.NONE; // MAR
}

public struct AluAction(AluOperation operation, 
    RegisterAction registerOperand , ushort constOperand)
{
    public AluOperation AluOperation = operation;
    public RegisterAction RegisterOperand = registerOperand;
    public ushort ConstOperand = constOperand;
}

public enum UniBusAction
{
    NONE, READ, WRITE,
}

public enum AluOperation
{
    NONE, ADD, SUB
}

public enum RegisterAction
{
    R0 = 0, R1 = 1, R2 = 2, R3 = 3, R4 = 4, R5 = 5, R6 = 6, R7 = 7,
    MDR = 8, IR = 9, MAR = 10, TMP = 11,
    NONE = 12,
}
