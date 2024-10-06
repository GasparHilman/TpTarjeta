using System;
using System.Collections.Generic;
using System.Linq;

namespace Space
{
    public class Colectivo
    {

        public int tarifa;
        public int precio = 940;
        public string linea; 
        public string TipoTarjeta;


        public Colectivo(string linea1)
        {
            this.linea = linea1;
        }

        public bool Descontar(Tarjeta tarjeta)
        {

            if (tarjeta is GratuitoBoleto)
            {   
                if(tarjeta.historial.Count != 0){
                    if (tarjeta.historial.LastOrDefault().UltimoViaje.Day != DateTime.Now.Day)
                    {

                        tarjeta.viajesHoy = 0;
                    }
                }
                if (tarjeta.viajesHoy < 2)
                {

                    tarifa = 0;

                }
                else
                {

                    tarifa = precio;
                }

                TipoTarjeta = "Boleto Gratuito";
            }
            else
            {

                if (tarjeta is MedioBoleto)
                {
                    tarifa = precio / 2;
                    TipoTarjeta = "Medio Boleto";
                }
                else
                {
                    tarifa = precio;
                    TipoTarjeta = "Medio Boleto";
                }
            }

            if (tarjeta.saldo - tarifa >= tarjeta.limite_neg)
            {
                tarjeta.saldo -= tarifa;
                return true;
            }
            else
            {
                Console.WriteLine("No se pudo pagar. Saldo insuficiente.");
                return false;
            }
        }

        public Boleto PagarCon(Tarjeta tarjeta)
        {
            if (Descontar(tarjeta))
            {
                tarjeta.historial.Add(new Boleto(tarifa, linea, tarjeta.saldo, TipoTarjeta, tarjeta.id));
                return new Boleto(tarifa, linea, tarjeta.saldo, TipoTarjeta, tarjeta.id);
            }
            else
            {
                Console.WriteLine("No se pudo emitir el boleto.");
                return null;
            }
        }
}
}