using System.Security.Cryptography.X509Certificates;
namespace src.Modelo 
{
    public class Fighter : Robot
    {
        public int CapacidadCombate { get; set; }
        public Fighter(String tipoRobot, int capacidadCombate) : base(tipoRobot)
        {
            CapacidadCombate = capacidadCombate;
        }
    }
}