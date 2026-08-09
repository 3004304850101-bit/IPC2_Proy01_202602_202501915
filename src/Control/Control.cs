namespace src.Modelo
{
    public class Control
    {
        public ListaCiudad Ciudades {get; set;}
        public ListaRobot Robots {get; set;}

        public Control()
        {
            Ciudades= new ListaCiudad();
            Robots= new ListaRobot();
        }

        public void CargarCiudad(Ciudad ciudadNueva)
        {
            Ciudad? existente= Ciudades.BuscarCiudadPorNombre(ciudadNueva.Nombre);
            if(existente== null)
            {
                Ciudades.AgregarCiudad(ciudadNueva);
            }
            else
            {
                Ciudades.ActualizarCiudad(ciudadNueva.Nombre,ciudadNueva);
            }
        }

        public void CargarRobot(Robot RobotNuevo)
        {
            Robot? existenteR= Robots.BuscarRobotPorNombre(RobotNuevo.NombreRobot);
            if(existenteR== null)
            {
                Robots.AgregarRobot(RobotNuevo);
            }
            else
            {
                Robots.ActualizarRobot(RobotNuevo.NombreRobot,RobotNuevo);
            }
        }

        public void CargarConfiguracion()
        {
            string directorioBase= AppContext.BaseDirectory;
            string raizProyecto= Directory.GetParent(directorioBase)!.Parent!.Parent!.Parent!.Parent!.FullName;
            string rutaConfiguracion = Path.Combine(raizProyecto, "configuracion");


            foreach(string archivo in Directory.EnumerateFiles(rutaConfiguracion,"*.xml"))
            {
                Console.WriteLine(archivo);
                LectorXML lector= new LectorXML(archivo);

                ListaCiudad ciudad= lector.CrearCiudades();
                ListaRobot robot= lector.CrearRobots();

                NodoCiudad? nodoCiudad=ciudad.PrimerCiudad!;
                NodoRobot? nodoRobot=robot.PrimerRobot;
                while (nodoCiudad != null)
                {
                    CargarCiudad(nodoCiudad.Ciudad!);
                    nodoCiudad=nodoCiudad.SiguienteCiudad;
                }

                while(nodoRobot!= null)
                {
                    CargarRobot(nodoRobot.Robot!);
                    nodoRobot=nodoRobot.SiguienteRobot;
                }


            }
        }
    }
    
}
