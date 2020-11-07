namespace muShell.Events
{
    public delegate void HealthSystemEvent(Health.HealthSystem Sender, float Health);
    public delegate void LevelSystemEvent(Level.LevelSystem Sender, int Exp, int Level);

}