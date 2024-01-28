public class Status
{
    public float defaulMaxHP;
    public float additionalMaxHP;
    public float currentNowHP;
    public float CurrentMaxHP { get { return defaulMaxHP + additionalMaxHP; } }


    public float defaultSpeed;
    public float additionalSpeed;
    public float CurrentSpeed { get { return defaultSpeed + additionalSpeed; } }

    public float defaultAttackForce;
    public float additionalAttackForce;
    public float CurrentAttackForce { get { return defaultAttackForce + additionalAttackForce; } }

    public float defaultDefenseForce;
    public float additionalDefenseForce;
    public float CurrentDefenseForce { get { return defaultAttackForce + additionalAttackForce; } }


    public float defaultCriticalProbability;
    public float additionalCriticalProbability;
    public float CurrentCriticalProbability { get { return defaultCriticalProbability + additionalCriticalProbability; } }

    public float defaultCriticalForce;
    public float additionalCriticalForce;
    public float CurrentCriticalForce { get { return defaultCriticalForce + additionalCriticalForce; } }


    public Status()
    {
        defaultSpeed = 10;
        defaulMaxHP = 10;
        defaultAttackForce = 10;
        currentNowHP = CurrentMaxHP;
    }
}
