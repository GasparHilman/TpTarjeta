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
                tarifa = 0;
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
                if (tarjeta.credito == 0)
                {

                    tarjeta.saldo -= tarifa;
                    tarjeta.viajesHoy++;

                }
                else
                {

                    if (tarjeta.credito >= tarifa)
                    {
                        tarjeta.credito -= tarifa;

                    }
                    else
                    {
                        tarjeta.saldo -= tarifa - tarjeta.credito;
                        tarjeta.credito = 0;
                    }
                }

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