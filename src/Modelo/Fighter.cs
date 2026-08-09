using System.Security.Cryptography.X509Certificates;
namespace src.Modelo 
{
    public class Fighter : Robot
    {
        public int CapacidadCombate { get; set; }
        public Fighter(string nombreRobot, int capacidadCombate) : base("ChapinFighter",nombreRobot)
        {
            CapacidadCombate = capacidadCombate;
        }
    }
}