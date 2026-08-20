namespace src.Modelo
{
    public class Buscar
    {
        public Pila? pilaCamino { get; set; }


        public bool EsValida(Celda? candidata, Robot robotE, Celda objetivo)
        {
            if (candidata==null) return false;
            if(candidata.Visitado) return false;
            if(candidata.TipoCelda==Celda.EstadoCelda.Instransitable) return false;
            if (candidata.TipoCelda == Celda.EstadoCelda.Recurso)
            {
                if (candidata == objetivo)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (candidata.TipoCelda == Celda.EstadoCelda.Militar)
            {
                if(robotE is Fighter f)
                {
                    if(f.CapacidadCombate > candidata.CapacidadCombate)
                    {
                        return true;
                    }
                        return false;
                }
                return false;
            }
            return true;
        }

        public Pila? DFS(Celda entrada, Celda objetivo, Robot robot)
        {
            pilaCamino = new Pila();
            //INGRESAR ENTRADA
            entrada.Visitado=true;
            Paso pasoE=new Paso(entrada,0);
            pilaCamino.Apilar(pasoE);

            while (!pilaCamino.EstaVacia())
            {
            Celda actualC=pilaCamino.Cima()!.celdaC!;
            //CAMINO ENCONTRADA HASTA OBJETIVO
            if (actualC == objetivo)
            {
                return pilaCamino;
            }

            //SEGUIR BUSCANDO
            else
            {
                Paso? pasoNuevo;
                //DERECHA
                if (EsValida(actualC.Derecha,robot, objetivo))
                {
                     Celda Candidata=actualC.Derecha!;
                     Candidata.Visitado=true;
                    if (Candidata.TipoCelda == Celda.EstadoCelda.Militar)
                    {
                        if(robot is Fighter f)
                        {
                            f.CapacidadCombate=f.CapacidadCombate-Candidata.CapacidadCombate;
                        }
                    }
                    pasoNuevo= new Paso(Candidata,Candidata.CapacidadCombate);
                    pilaCamino.Apilar(pasoNuevo);

                }
                //ABAJO
                else if (EsValida(actualC.Abajo, robot, objetivo))
                {
                    Celda Candidata=actualC.Abajo!;
                    Candidata.Visitado=true;
                    if (Candidata.TipoCelda == Celda.EstadoCelda.Militar)
                    {
                        if(robot is Fighter f)
                        {
                            f.CapacidadCombate=f.CapacidadCombate-Candidata.CapacidadCombate;
                        }
                    }
                    pasoNuevo= new Paso(Candidata,Candidata.CapacidadCombate);
                    pilaCamino.Apilar(pasoNuevo);
                }
                //IZQUIERA
                else if (EsValida(actualC.Izquierda, robot, objetivo))
                {
                    Celda Candidata=actualC.Izquierda!;
                     Candidata.Visitado=true;
                    if (Candidata.TipoCelda == Celda.EstadoCelda.Militar)
                    {
                        if(robot is Fighter f)
                        {
                            f.CapacidadCombate=f.CapacidadCombate-Candidata.CapacidadCombate;
                        }
                    }
                    pasoNuevo= new Paso(Candidata,Candidata.CapacidadCombate);
                    pilaCamino.Apilar(pasoNuevo);
                    
                }
                //ARRIBA
                else if (EsValida(actualC.Arriba, robot, objetivo))
                {
                    Celda Candidata=actualC.Arriba!;
                     Candidata.Visitado=true;
                    if (Candidata.TipoCelda == Celda.EstadoCelda.Militar)
                    {
                        if(robot is Fighter f)
                        {
                            f.CapacidadCombate=f.CapacidadCombate-Candidata.CapacidadCombate;
                        }
                    }
                    pasoNuevo= new Paso(Candidata,Candidata.CapacidadCombate);
                    pilaCamino.Apilar(pasoNuevo);
                }
                //RETROCEDER
                else
                {
                    Paso retroceso=pilaCamino.Desapilar();
                    if(robot is Fighter f)
                    {
                        f.CapacidadCombate=f.CapacidadCombate+retroceso.CapacidadC;
                    }
                }

            }
            }
            Console.WriteLine("CAMINO NO ENCONTRADO");
            return null;
        }

        public Pila OrdenarCamino(Pila camino)
        {
            Pila aux=new Pila();
            while (!camino.EstaVacia())
            {
                
                Paso auxiliar=camino.Desapilar();
                auxiliar.celdaC!.Camino=true;
                aux.Apilar(auxiliar);
            }

            return aux;
        }
    }
}