using src.Modelo;

Control control=new Control();
Buscar buscar=new Buscar();
GeneradorGraph generador= new GeneradorGraph();
control.CargarConfiguracion();

int opcion;
do
{
   Console.WriteLine("==================SISTEMA CONTROL==================");
   Console.WriteLine("Menú de opciones:");
    Console.WriteLine("[1] Elección Ciudad");
    Console.WriteLine("[2] Salir");
    Console.Write("Opción: ");
    int.TryParse(Console.ReadLine(), out opcion);
    Console.Clear();
    switch (opcion)
    {
        case 1:
            Ciudad elegida=ListaCiudades();
            elegida.LimpiarVisitados();
            Console.Clear();
            TipoMision misionElegida=ElegirMision(elegida);
            Console.Clear();
            if (misionElegida != TipoMision.Imposible)
            {
                //Listado y eleccion de robot
                Robot? robotElegido= ListaRobots(misionElegida);
                Console.Clear();
                if (robotElegido != null)
                {
                    //MOSTRAR MAPA (PRUEBA)
                    generador.GenerarMapa(elegida);
                    Console.WriteLine("---------Visualiza el mapa--------");
                    Console.WriteLine("Enter para continuar");
                    Console.ReadKey();
                    Console.Clear();

                    //Ejecucion Mision
                    Console.WriteLine("------------ENTRADA------------");
                    Celda entrada=CeldaObjetivo(elegida,Celda.EstadoCelda.Entrada,elegida.CantidadEntradas)!;
                    Console.WriteLine("Enter para continuar");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("------------OBJETIVO------------");
                    Celda objetivo;
                    if (misionElegida == TipoMision.Rescate)
                    {
                        Console.WriteLine("-RESCATAR");
                        objetivo = CeldaObjetivo(elegida, Celda.EstadoCelda.Civil, elegida.CantidadCiviles)!;
                    }
                    else
                    {
                        Console.WriteLine("-EXTRAER");
                        objetivo = CeldaObjetivo(elegida, Celda.EstadoCelda.Recurso, elegida.CantidadRecursos)!;
                    }
                    Console.WriteLine("Ejecutar Misión, Enter");
                    Console.ReadKey();
                    Console.Clear();

                    //Buscar camino
                    Console.WriteLine("--------------CAMINO--------------");
                    elegida.LimpiarVisitados();
                    
                    //Vida del robot inicio
                    int InicialC= 0;
                    if(robotElegido is Fighter f)
                    {
                        InicialC=f.CapacidadCombate;
                    }

                    //Camino Inverso
                    Pila? caminoI=buscar.DFS(entrada, objetivo, robotElegido);
                    if(caminoI != null)
                    {
                        Pila Camino=buscar.OrdenarCamino(caminoI);
                        int Inicial=-1;
                        int Final=-1;
                        if(robotElegido is Fighter fighter)
                        {
                            Inicial=InicialC;
                            Final=fighter.CapacidadCombate;
                            fighter.CapacidadCombate = InicialC;
                        }
                        Console.WriteLine($"[DEBUG] Inicial={Inicial}, Final={Final}, EsFighter={robotElegido is Fighter}");
                        string dot=generador.GeneradorMapaDot(elegida);
                        generador.GenerarCamino(dot,Camino,elegida,misionElegida,objetivo,robotElegido,Inicial,Final);
                    }
                    Console.WriteLine("VISUALIZA EL CAMINO EN PANTALLA");
                    Console.WriteLine("Enter para continuar");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            break;
        case 2:
            //SALIR
            Console.Clear();
            break;
        default:
            Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
            Console.ReadKey();
            Console.Clear();
            break;
            
    }
}while(opcion!=2);

Ciudad ListaCiudades()
{
    ListaCiudad ciudadeslista=control.Ciudades;
    NodoCiudad? nodo=control.Ciudades.PrimerCiudad!;
    int listado=1;
    Console.WriteLine("==================CIUDADES DISPONIBLES==================");
    while (nodo != null)
    {
        Console.WriteLine($"[{listado++}] {nodo.Ciudad!.Nombre}");
        nodo=nodo.SiguienteCiudad;
    }
    Console.Write("Opción: ");
    int.TryParse(Console.ReadLine(), out int opcionC);

    while(opcionC<=0 || opcionC > listado-1 )
    {
        Console.WriteLine("Ingrese opción válida");
        int.TryParse(Console.ReadLine(), out opcionC);
    }
    
    NodoCiudad? nodoCiudadElegida=control.Ciudades.PrimerCiudad;
    for (int i=1;i<opcionC;i++)
    {
        nodoCiudadElegida=nodoCiudadElegida!.SiguienteCiudad;
    }
    Ciudad? ciudadElegida=nodoCiudadElegida!.Ciudad;

    return ciudadElegida!;
}

TipoMision ElegirMision(Ciudad ciudad)
{
    int contadorRescate=ciudad.CantidadCiviles;
    int contadorExtraccion=ciudad.CantidadRecursos;
    TipoMision tipoMision= TipoMision.Imposible;

    if (contadorRescate > 0 && contadorExtraccion==0)
    {
        Console.WriteLine("==================MISION DISPONIBLE==================");
        Console.WriteLine("*RESCATE");
        tipoMision= TipoMision.Rescate;
        Console.ReadKey();
    }else if (contadorExtraccion > 0 && contadorRescate==0)
    {
        Console.WriteLine("==================MISION DISPONIBLE==================");
        Console.WriteLine("*EXTRACCIÓN");
        tipoMision= TipoMision.Extraccion;
        Console.ReadKey();
    }else if(contadorRescate > 0 && contadorExtraccion > 0)
    {
        Console.WriteLine("==================OPCIONES DE MISION==================");
        Console.WriteLine("[1] Rescate");
        Console.WriteLine("[2] Extracción");
        Console.WriteLine("Opcion:");
        int.TryParse(Console.ReadLine(), out int opcionM);

        while(opcionM<=0 || opcionM > 2)
        {
            Console.WriteLine("Opcion:");
            int.TryParse(Console.ReadLine(), out opcionM);
        }
        switch (opcionM)
        {
            case 1:
            tipoMision= TipoMision.Rescate;
            break;

            case 2:
            tipoMision= TipoMision.Extraccion;
            break;
        }
    }
    else
    {
        Console.WriteLine("*******************MISION IMPOSIBLE*******************");
        tipoMision= TipoMision.Imposible;
        Console.ReadKey();
    }

    return tipoMision;
}

Robot? ListaRobots(TipoMision tipoMision)
{
    ListaRobot robots=control.Robots;
    NodoRobot? nodoRobot=control.Robots.PrimerRobot;
    Robot? robotElegido=null;
    int rescue=0;
    int fight=0;
    if (robots.PrimerRobot != null)
    {
        if (tipoMision == TipoMision.Rescate)
        {
            //RESCATE
            Console.WriteLine("==================OPCIONES DE ROBOT==================");
            while (nodoRobot != null)
            {
                if (nodoRobot.Robot!.TipoRobot == "ChapinRescue")
                {
                Console.WriteLine($"-{nodoRobot.Robot!.NombreRobot}");
                robotElegido=nodoRobot.Robot;
                rescue++;
                }
                nodoRobot=nodoRobot.SiguienteRobot;
            }
                if(rescue>1)
                {
                    Console.WriteLine("Opcion:");
                    string opcionUser = Console.ReadLine()!;
                    robotElegido=robots.BuscarRobotPorNombre(opcionUser);
                    while (robotElegido == null)
                    {
                        Console.WriteLine("Ingrese nombre correctamente");
                        opcionUser = Console.ReadLine()!;
                        robotElegido=robots.BuscarRobotPorNombre(opcionUser);
                    }
                    return robotElegido;
                }
                else if (rescue == 1)
                {
                    return robotElegido;
                }
                else
                {
                    Console.WriteLine("NO HAY ROBOT DISPONIBLE MISION IMPOSIBLE");
                    Console.ReadKey();
                }
        }
        else
        {
            //EXTRACCIÓN
            Console.WriteLine("==================OPCIONES DE ROBOT==================");
             while (nodoRobot != null)
            {
                if (nodoRobot.Robot!.TipoRobot == "ChapinFighter")
                {
                    Fighter f =(Fighter)nodoRobot.Robot;
                    Console.WriteLine($"-{f.NombreRobot} (Capacidad: {f.CapacidadCombate})");
                    robotElegido = nodoRobot.Robot;
                    fight++;
                }
                nodoRobot=nodoRobot.SiguienteRobot;
            }
                if(fight>1)
                {
                    Console.WriteLine("Opcion:");
                    string opcionUser = Console.ReadLine()!;
                    robotElegido=robots.BuscarRobotPorNombre(opcionUser);
                    while (robotElegido == null)
                    {
                        Console.WriteLine("Ingrese nombre correctamente");
                        opcionUser = Console.ReadLine()!;
                        robotElegido=robots.BuscarRobotPorNombre(opcionUser);
                    }
                    return robotElegido;
                }
                else if (fight == 1)
                {
                    return robotElegido;
                }
                else
                {
                    Console.WriteLine("NO HAY ROBOT DISPONIBLE MISION IMPOSIBLE");
                    Console.ReadKey();
                }
        }
    }
    else
    {
        Console.WriteLine("*******************MISION IMPOSIBLE*******************");
        Console.ReadKey();
    }
    return robotElegido;
}

Celda? CoordenadasCelda(Ciudad ciudad,Celda.EstadoCelda tipo)
{
    Console.WriteLine("Ingrese la celda");
    Celda? celdaE=null;
    bool invalido=true;

    while (invalido)
    {
        Console.WriteLine("Fila:");
        int.TryParse(Console.ReadLine(), out int filaE);
        Console.WriteLine("Columna:");
        int.TryParse(Console.ReadLine(), out int columnaE);

         celdaE=ciudad.BuscarCelda(filaE,columnaE);

        try
        {
            if (celdaE.TipoCelda == tipo)
            {
                invalido=false;
                return celdaE;
            }
            else
            {
                Console.WriteLine("Celda no encontrada tipo incorrecto, intente de nuevo");
            }
        }
        catch (ArgumentException)
        {
                Console.WriteLine("Fuera de alcance o dato erroneo, intente de nuevo");
        }
        
    }
    return celdaE;
}

Celda? CeldaObjetivo(Ciudad ciudad, Celda.EstadoCelda tipo, int CantidadD)
{
    if (CantidadD == 1)
    {
        return ciudad.BuscarCeldaTipo(tipo);
    }
    else
    {
        return CoordenadasCelda(ciudad,tipo);
    }
}

public enum TipoMision
{
    Rescate,
    Extraccion,
    Imposible
}

