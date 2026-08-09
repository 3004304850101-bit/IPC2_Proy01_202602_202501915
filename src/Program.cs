// Ajusta la ruta según donde tengas el archivo de prueba
using src.Modelo;

Console.WriteLine("Inicio");
Control control=new Control();
control.CargarConfiguracion();

NodoCiudad? nodo = control.Ciudades.PrimerCiudad;
while (nodo != null)
{
    Console.WriteLine($"Ciudad: {nodo.Ciudad!.Nombre}, {nodo.Ciudad.CantidadFilas}x{nodo.Ciudad.CantidadColumnas}, Civiles: {nodo.Ciudad.CantidadCiviles}, Recursos: {nodo.Ciudad.CantidadRecursos}");
    nodo = nodo.SiguienteCiudad;
}

NodoRobot? nodoR = control.Robots.PrimerRobot;
while (nodoR != null)
{
    Console.WriteLine($"Robot: {nodoR.Robot!.NombreRobot} ({nodoR.Robot.TipoRobot})");
    nodoR = nodoR.SiguienteRobot;
}

Console.WriteLine("Fin");