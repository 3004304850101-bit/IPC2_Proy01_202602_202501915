namespace src.Modelo
{
    public abstract class Robot
    {
        public String TipoRobot { get; set; }
        public string NombreRobot {get; set;}
        public Robot(String tipoRobot, string nombreRobot)
        { 
            TipoRobot = tipoRobot;   
            NombreRobot=nombreRobot;
        }
    }
}