using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LAYERS
{
    FLOOR = 9,
    SPAWNER,
    ANIMALS,
    MATERIALS,
    
};

public enum ANIMAL_STATES
{
    IDLE = 0,
    WALK,
    RUN,
    JUMP,
    EAT,
    RESET,
};

public enum ANIMAL_TYPES
{
    CHICKEN = 0,
    PIG,
    ALPACA,
    HORSE,
    DOG,
    AMBIENT,
    COUNT
};

public enum RESOURCES_TYPES
{
    STRAW = 0,
    WOOD,
    BRICKS
};