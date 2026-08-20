using System.Diagnostics;
namespace src.Modelo;

public class GeneradorGraph
{
    public string GeneradorMapaDot(Ciudad ciudad)
    {
        string dot="graph Ciudad {\n";
        dot=dot+"layout=neato;\n";
        dot=dot+$"label=\"{ciudad.Nombre}\";\n";
        dot=dot+"labelloc=\"t\";\n";
        dot=dot+"fontsize=20;\n";
        dot=dot+"node [shape=square, fixedsize=true, width=0.9, label=\"\"];\n\n";

        for(int i = 1; i <= ciudad.CantidadColumnas; i++)
        {
            dot=dot+ $"Col{i} [pos=\"{i},0!\", shape=plaintext, label=\"{i}\"];\n";
        }

        for(int i = 1; i <= ciudad.CantidadFilas; i++)
        {
            dot=dot+ $"Fil{i} [pos=\"0,{-i}!\", shape=plaintext, label=\"{i}\"];\n";
        }

        Filas? fila=ciudad.PrimerFila!;
        while(fila != null)
        {
            Celda? celda=fila.PrimerCelda!;
            while (celda != null)
            {
                string color=Color(celda);
                dot=dot+$"C{celda.Fila}_{celda.Columna} [pos=\"{celda.Columna},{-celda.Fila}!\", style=filled, fillcolor=\"{color}\"];\n";
                celda=celda.Derecha;
            }

            fila=fila.SiguienteFila;
        }

        dot=dot+ $"Ley1 [pos=\"1.3,{-ciudad.CantidadFilas-1}!\", style=filled, fillcolor=\"black\", label=\"\"];\n";
        dot=dot+ $"LeyTexto1 [pos=\"3,{-ciudad.CantidadFilas-1}!\", shape=plaintext, label=\"Intransitable\"];\n";
        dot=dot+ $"Ley2 [pos=\"1.3,{-ciudad.CantidadFilas-2}!\", style=filled, fillcolor=\"green\", label=\"\"];\n";
        dot=dot+ $"LeyTexto2 [pos=\"3,{-ciudad.CantidadFilas-2}!\", shape=plaintext, label=\"Entrada\"];\n";
        dot=dot+ $"Ley3 [pos=\"1.3,{-ciudad.CantidadFilas-3}!\", style=filled, fillcolor=\"white\", label=\"\"];\n";
        dot=dot+ $"LeyTexto3 [pos=\"3,{-ciudad.CantidadFilas-3}!\", shape=plaintext, label=\"Transitable\"];\n";
        dot=dot+ $"Ley4 [pos=\"1.3,{-ciudad.CantidadFilas-4}!\", style=filled, fillcolor=\"red\", label=\"\"];\n";
        dot=dot+ $"LeyTexto4 [pos=\"3,{-ciudad.CantidadFilas-4}!\", shape=plaintext, label=\"Unidad Militar\"];\n";
        dot=dot+ $"Ley5 [pos=\"1.3,{-ciudad.CantidadFilas-5}!\", style=filled, fillcolor=\"blue\", label=\"\"];\n";
        dot=dot+ $"LeyTexto5 [pos=\"3,{-ciudad.CantidadFilas-5}!\", shape=plaintext, label=\"Unidad Civil\"];\n";
        dot=dot+ $"Ley6 [pos=\"1.3,{-ciudad.CantidadFilas-6}!\", style=filled, fillcolor=\"gray\", label=\"\"];\n";
        dot=dot+ $"LeyTexto6 [pos=\"3,{-ciudad.CantidadFilas-6}!\", shape=plaintext, label=\"Unidad Recurso\"];\n\n";
        dot=dot+"}\n";
        return dot;
    }

    public string Color(Celda celda)
    {
        string color="white";

        if (celda.TipoCelda == Celda.EstadoCelda.Civil)
        {
            color="blue";
        } else if (celda.TipoCelda == Celda.EstadoCelda.Entrada)
        {
            color="green";
        } else if (celda.TipoCelda == Celda.EstadoCelda.Instransitable)
        {
            color="black";
        } else if (celda.TipoCelda == Celda.EstadoCelda.Militar)
        {
            color="red";
        } else if (celda.TipoCelda == Celda.EstadoCelda.Recurso)
        {
            color="gray";
        }
        else if(celda.TipoCelda == Celda.EstadoCelda.Transitable)
        {
            if (celda.Camino == true)
            {
                color="yellow";
            }
        }

        return color;
    }

    public string GenerardorCaminoDot(string dot,Pila camino, Ciudad ciudad,TipoMision mision, Celda objetivo, Robot robot, int CapacidadI ,int CapacidadF)
    {
        string info= $"Tipo de misión: {mision}\\nObjetivo: {objetivo.Fila},{objetivo.Columna}\\nRobot utilizado: {robot.NombreRobot} ({robot.TipoRobot})";
        if (CapacidadI >= 0)
        {
            info=info+$"\\nCapacidad inicial: {CapacidadI}, Capacidad final: {CapacidadF}\\n\\n\\n\\n";
        }
        string CaminoL="";
        Celda? anterior=null;

        while (!camino.EstaVacia())
        {
            Paso imprimir=camino.Desapilar();

            if(anterior != null)
            {
                CaminoL=CaminoL+$"C{anterior.Fila}_{anterior.Columna} -- C{imprimir.celdaC!.Fila}_{imprimir.celdaC.Columna} [color=orange, penwidth=3];\n";
            }

            anterior=imprimir.celdaC;
        }
        
        string NuevaInfo= $"Info [pos=\"1.5,{-ciudad.CantidadFilas-7.5}!\", shape=plaintext, label=\"{info}\"];\n\n";
        NuevaInfo=NuevaInfo+CaminoL;
        int Nuevocierre= dot.LastIndexOf('}');
        return dot.Insert(Nuevocierre, NuevaInfo);
    }

    public void GenerarMapa(Ciudad ciudad)
    {
         string directorioBase= AppContext.BaseDirectory;
         string raizProyecto= Directory.GetParent(directorioBase)!.Parent!.Parent!.Parent!.FullName;
         string rutaSalida = Path.Combine(raizProyecto, "Reportes");

         string fecha=DateTime.Now.ToString("yyyyMMdd_HHmmss");
         string nombre=$"{ciudad.Nombre}_{fecha}";

         string rutaDot= Path.Combine(rutaSalida,$"mapa.dot");
         string rutaImagen= Path.Combine(rutaSalida,$"mapa.png");

         string contenidoDot=GeneradorMapaDot(ciudad);
         File.WriteAllText(rutaDot,contenidoDot);

         ProcessStartInfo InfoDot= new ProcessStartInfo();
         InfoDot.FileName= "dot";
         InfoDot.Arguments= $"-Tpng \"{rutaDot}\" -o \"{rutaImagen}\"";
         InfoDot.UseShellExecute=false;
         Process.Start(InfoDot)!.WaitForExit();

         ProcessStartInfo abrirInfo= new ProcessStartInfo();
         abrirInfo.FileName=rutaImagen;
         abrirInfo.UseShellExecute= true;
        Process.Start(abrirInfo);
    }

    public void GenerarCamino(string dot,Pila camino, Ciudad ciudad,TipoMision mision, Celda objetivo, Robot robot, int CapacidadI, int CapacidadF)
    {
         string directorioBase= AppContext.BaseDirectory;
         string raizProyecto= Directory.GetParent(directorioBase)!.Parent!.Parent!.Parent!.FullName;
         string rutaSalida = Path.Combine(raizProyecto, "Reportes");

         string fecha=DateTime.Now.ToString("yyyyMMdd_HHmmss");
         string nombre=$"{ciudad.Nombre}.Camino_{fecha}";

         string rutaDot= Path.Combine(rutaSalida,$"camino.dot");
         string rutaImagen= Path.Combine(rutaSalida,$"camino.png");

         string contenidoDot=GenerardorCaminoDot(dot,camino,ciudad,mision,objetivo,robot,CapacidadI,CapacidadF);
         File.WriteAllText(rutaDot,contenidoDot);

         ProcessStartInfo InfoDot= new ProcessStartInfo();
         InfoDot.FileName= "dot";
         InfoDot.Arguments= $"-Tpng \"{rutaDot}\" -o \"{rutaImagen}\"";
         InfoDot.UseShellExecute=false;
         Process.Start(InfoDot)!.WaitForExit();

         ProcessStartInfo abrirInfo= new ProcessStartInfo();
         abrirInfo.FileName=rutaImagen;
         abrirInfo.UseShellExecute= true;
        Process.Start(abrirInfo);
    }
}