namespace src.Modelo
{
    public class Paso
    {
        public Celda? celdaC { get; set;}
        public int CapacidadC { get ; set;}

        public Paso(Celda celda, int capacidad)
        {
            celdaC=celda;
            CapacidadC=capacidad;
        }
    }
}