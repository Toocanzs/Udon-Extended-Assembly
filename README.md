# Udon Extended Assembly
 
This project adds the ability to construct and fill the heap from UASM alone through the use of a preprocessor.

**How to use:**

Drop the folder `ExtendedUASM` into `\Assets\Udon\Editor`

Then right click, Create->VRChat->Udon->Extended Udon Assembly Program Asset

Add that new file to the "Program Source" slot on a UdonBehaviour

Paste your UASM code in the Assembly Code box.

**What does Udon Extended Assembly add?**

By adding comments to the top of the uasm source code, you can construct types at compile time.

Example:
```
#.INIT_START
#PUSH SystemSingle 1
#PUSH SystemSingle 5
#CONSTRUCT UnityEngineVector2 2
#SETHEAP "test"
#PUSH SystemString "This should work"
#SETHEAP "str"
#.INIT_END
.data_start
	test: %UnityEngineVector2, null
	str: %SystemString, null
.data_end

.code_start
    .export _start
    
    _start:
        PUSH, test
        EXTERN, "UnityEngineDebug.__Log__SystemObject__SystemVoid"
	PUSH, str
        EXTERN, "UnityEngineDebug.__Log__SystemObject__SystemVoid"
        JUMP, 0xFFFFFF
.code_end
```

Above you can see comments which look very similar to udon assembly. Don't worry there's only 4 instructions.

**PUSH**

- Usage: `#PUSH TypeName LiteralValue`. Example: `#PUSH SystemString "This is a string"` `#PUSH SystemSingle 123.456`
- Supported literals that can be pushed include SystemSingle, SystemDouble, SystemInt64, SystemInt32, SystemInt16, SystemUInt64, SystemUInt32, SystemUInt16, SystemString, SystemBoolean, SystemChar, SystemByte, SystemSByte, and SystemType
- Strings should look like 'PUSH SystemString "This is a string"'

**PUSHENUM**

- Usage: `#PUSHENUM TypeName EnumValueName`. Example: `#PUSHENUM UnityEngineFFTWindow Hamming`
- Attempts to find the enum value `EnumValueName` in the type `TypeName`

**CONSTRUCT**
- Usage: `#CONSTRUCT TypeName ArgCount`. Example: `#CONSTRUCT UnityEngineVector2 2`
- Pops `ArgCount` arguments off the stack and attempts to construct the type `TypeName` with those arguments. It will attempt to call the constructor that the arguments fit into. The result is automatically pushed back onto the stack.

**SETHEAP**
- Usage: `#SETHEAP "variableName"`. Example: `#SETHEAP "test"`
- Finds the variable named `variableName` in the assmebly and sets the value of that variable on the heap to the first object on the stack which is popped off.



Place any of these instructions in between `#.INIT_START` and `#.INIT_END` at the top of the file.
