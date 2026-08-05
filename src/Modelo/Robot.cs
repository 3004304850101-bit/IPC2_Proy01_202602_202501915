namespace src.Modelo
{
    public abstract class Robot
    {
        public String TipoRobot { get; set; }
        public Robot(String tipoRobot)
        { 
            TipoRobot = tipoRobot;   
        }
    }
}