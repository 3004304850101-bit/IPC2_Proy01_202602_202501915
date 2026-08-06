namespace src.Modelo
{
    public class NodoRobot
    {
        public Robot? Robot { get; set; }
        public NodoRobot? SiguienteRobot { get; set; }
        public NodoRobot(Robot? robot)
        {
            Robot=robot;
        }
    }

    public class ListaRobot
    {
        public NodoRobot? PrimerRobot { get; set; }
        public NodoRobot? UltimoRobot { get; set; }

        public void AgregarRobot(Robot robot)
        {
            NodoRobot nuevoNodo = new NodoRobot(robot);
            if (PrimerRobot == null)
            {
                PrimerRobot = nuevoNodo;
            }
            else
            {
                UltimoRobot!.SiguienteRobot = nuevoNodo;
            }
            UltimoRobot=nuevoNodo;
        }
    }
}