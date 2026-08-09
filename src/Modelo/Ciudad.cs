namespace src.Modelo
{
    public class Ciudad
    {
        public string Nombre { get; set; }
        public int CantidadFilas { get; set; }
        public int CantidadColumnas { get; set; }
        public  Filas? PrimerFila { get; set; }
        public  Columnas? PrimerColumna { get; set; }
        
         Celda? ultimaCelda = null;
         Filas? filaActual = null;
         Columnas? columnaActual = null;

        public int CantidadCiviles { get; set; }
        public int CantidadRecursos { get; set; }

        public Ciudad(string nombre,int cantidadFilas, int cantidadColumnas)
        {
            Nombre= nombre;
            CantidadFilas= cantidadFilas;
            CantidadColumnas= cantidadColumnas;
            PrimerFila= null;
            PrimerColumna= null;
            CantidadCiviles= 0;
            CantidadRecursos= 0;
        }

        public void AgregarFilas(int cantidadFilas)
        {
            Filas? ultimaFila= null;
            for (int i=1; i<=cantidadFilas;i++)
            {
                Filas nuevaFila= new Filas(i);
                
                if (PrimerFila== null)
                {
                    PrimerFila= nuevaFila;
                }
                else
                {
                    ultimaFila!.SiguienteFila= nuevaFila;  
                }
                ultimaFila= nuevaFila;
            }
        }

        public void AgregarColumnas(int cantidadColumnas)
        {
            Columnas? ultimaColumna = null;
            for (int i=1; i<= cantidadColumnas;i++)
            {
                Columnas nuevaColumna= new Columnas(i);
                
                if (PrimerColumna== null)
                {
                    PrimerColumna= nuevaColumna;
                }
                else
                {
                    ultimaColumna!.SiguienteColumna= nuevaColumna;  
                }
                ultimaColumna= nuevaColumna;
            }
        }

        public void AgregarCelda(int fila, int columna, string tipo)
        {
            if (filaActual== null)
            {
                filaActual= PrimerFila;
            }
            if (columnaActual== null)
            {
                columnaActual= PrimerColumna;
            }
            Celda.EstadoCelda tipoCelda;
            switch (tipo)
            {
                case "*":
                    tipoCelda= Celda.EstadoCelda.Instransitable;
                    break;
                case " ":
                    tipoCelda= Celda.EstadoCelda.Transitable;
                    break;
                case "E":
                    tipoCelda= Celda.EstadoCelda.Entrada;
                    break;
                case "C":
                    tipoCelda= Celda.EstadoCelda.Civil;
                    CantidadCiviles++;
                    break;
                case "R":
                    tipoCelda= Celda.EstadoCelda.Recurso;
                    CantidadRecursos++;
                    break;
                default:
                    throw new ArgumentException("Tipo de celda inválido");
            }
            //CREAMOS LA CELDA CON LOS PARAMETROS DE FILA, COLUMNA Y TIPO DE CELDA
            Celda nuevaCelda= new Celda(fila, columna, tipoCelda);
            // Agregar la celda a la fila correspondiente
            
            //Cambio de fila para estar en la fila correcta
            while (filaActual!= null && filaActual.NumeroFila != fila)
            {
                filaActual= filaActual.SiguienteFila;
                columnaActual= PrimerColumna; // Reiniciar la columna al cambiar de fila
                ultimaCelda= null; // Reiniciar la última celda al cambiar de fila
            }
            //Excepcion por precaucion en caso de que no se encuentre la fila
            if (filaActual== null)
            {
                throw new ArgumentException("Fila no encontrada");
            }
            //Primera celda de la fila
            if (filaActual.PrimerCelda== null)
            {
                filaActual.PrimerCelda= nuevaCelda;
            }
            //Agregar la celda al final de la fila
            else
            {
                ultimaCelda!.Derecha= nuevaCelda;
                nuevaCelda.Izquierda= ultimaCelda;
            }
            ultimaCelda=nuevaCelda;

            // Agregar la celda a la columna correspondiente
            //Cambio de columna para estar en la columna correcta
            while (columnaActual!= null && columnaActual.NumeroColumna != columna)
            {
                columnaActual= columnaActual.SiguienteColumna;
            }
            // Excepción por precaución en caso de que no se encuentre la columna
            if (columnaActual== null)
            {
                throw new ArgumentException("Columna no encontrada");
            }
            // Primera celda de la columna
            if (columnaActual.PrimerCelda== null)
            {
                columnaActual.PrimerCelda= nuevaCelda;
            }
            // Agregar la celda al final de la columna
            else
            {
                columnaActual.UltimaCeldaC!.Abajo= nuevaCelda;
                nuevaCelda.Arriba= columnaActual.UltimaCeldaC;  
            }
                columnaActual.UltimaCeldaC= nuevaCelda;
            }

            public Celda BuscarCelda(int fila, int columna)
            {
                Filas? filaActualR= PrimerFila;
                while (filaActualR!= null && filaActualR.NumeroFila != fila)
                {
                    filaActualR= filaActualR.SiguienteFila;
                }
                if (filaActualR== null)
                {
                    throw new ArgumentException("Fila no encontrada");
                }

                Celda? celdaActualR= filaActualR.PrimerCelda;
                while (celdaActualR!= null && celdaActualR.Columna != columna)
                {
                    celdaActualR= celdaActualR.Derecha;
                }
                if (celdaActualR== null)
                {
                    throw new ArgumentException("Columna no encontrada");
                }
                return celdaActualR;
            }

            public void AsignarUnidadMilitar(int fila, int columna, int capacidadCombate)
            {
                Celda celda= BuscarCelda(fila, columna);
                celda.TipoCelda = Celda.EstadoCelda.Militar;
                celda.CapacidadCombate= capacidadCombate;
            }
        }
    }
     
               