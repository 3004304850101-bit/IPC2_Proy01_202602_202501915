namespace src.Modelo
{
    public class Columnas
    {
         public int NumeroColumna { get; set; }
        public Celda? PrimerCelda { get; set; }
        public Celda? UltimaCeldaC { get; set; }
        public Columnas? SiguienteColumna { get; set; }
        public Columnas(int numeroColumna)
        {
            NumeroColumna = numeroColumna;
        }
    }
}