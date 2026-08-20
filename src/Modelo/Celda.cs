namespace src.Modelo
{
    public class Celda
    {
        public enum EstadoCelda
        {
            Instransitable,
            Transitable,
            Entrada,
            Civil,
            Militar,
            Recurso,

        }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public EstadoCelda TipoCelda { get; set; }
        public bool Visitado { get; set; }
        public bool Camino { get; set; }
        public int CapacidadCombate { get; set; }

        public Celda? Arriba { get; set; }
        public Celda? Abajo { get; set; }
        public Celda? Izquierda { get; set; }
        public Celda? Derecha { get; set; }

        public Celda(int fila, int columna, EstadoCelda tipoCelda)
        {
            Fila = fila;
            Columna = columna;
            TipoCelda = tipoCelda;
            Visitado=false;
            Camino=false;
            CapacidadCombate = 0;
        }
    }
}