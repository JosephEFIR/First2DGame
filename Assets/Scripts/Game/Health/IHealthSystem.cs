namespace Assets.Scripts.Interfaces
{
    public interface IHealthSystem
    {
        public void AddHealth(int value);
        public void GetDamage(int value);
        public void Death();
    }
}